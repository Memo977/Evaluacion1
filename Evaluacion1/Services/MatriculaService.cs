using Evaluacion1.Data;
using Evaluacion1.Models;
using Evaluacion1.Models.Enum;
using Evaluacion1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Evaluacion1.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IApplicationDbContext _context;
        private readonly IEventLogger _logger;

        public MatriculaService(IApplicationDbContext context, IEventLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            _logger.LogConsulta("Matriculas");
            return await _context.Matriculas
                .Include(m => m.Estudiante)
                .ToListAsync();
        }

        public async Task<Matricula> GetByIdAsync(int id)
        {
            _logger.LogConsulta("Matriculas");
            return await _context.Matriculas
                .Include(m => m.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> CanMatricularAsync(int estudianteId)
        {
            var estudiante = await _context.Estudiantes.FindAsync(estudianteId);
            if (estudiante == null)
                return false;

            // Solo se puede matricular si no está matriculado
            return estudiante.Estado == EstadoEstudiante.NoMatriculado;
        }

        public async Task<Matricula> CreateAsync(int estudianteId)
        {
            // Validar si el estudiante puede ser matriculado
            if (!await CanMatricularAsync(estudianteId))
                return null;

            // Obtener estudiante
            var estudiante = await _context.Estudiantes.FindAsync(estudianteId);

            // Crear la matrícula
            var matricula = new Matricula
            {
                EstudianteId = estudianteId,
                Fecha = DateTime.Now
            };

            // Actualizar el estado del estudiante
            estudiante.Estado = EstadoEstudiante.Matriculado;

            // Guardar cambios
            using var transaction = _context.BeginTransaction();
            try
            {
                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync();

                _logger.LogAgregar("Matricula", matricula.Id);
                _logger.LogActualizar("Estudiante", estudianteId);

                await transaction.CommitAsync();
                return matricula;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (matricula == null)
                return false;

            // Actualizar el estado del estudiante
            matricula.Estudiante.Estado = EstadoEstudiante.NoMatriculado;

            // Eliminar la matrícula
            using var transaction = _context.BeginTransaction();
            try
            {
                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();

                _logger.LogEliminar("Matricula", id);
                _logger.LogActualizar("Estudiante", matricula.EstudianteId);

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}