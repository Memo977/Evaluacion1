using Evaluacion1.Services.Interfaces;
using Evaluacion1.Data;
using Evaluacion1.Models;
using Microsoft.EntityFrameworkCore;
using Evaluacion1.Models.Enum;

namespace Evaluacion1.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IApplicationDbContext _context;
        private readonly IEventLogger _logger;

        public EstudianteService(IApplicationDbContext context, IEventLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            _logger.LogConsulta("Estudiantes");
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            _logger.LogConsulta("Estudiantes");
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task<Estudiante> CreateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            _logger.LogAgregar("Estudiante", estudiante.Id);
            return estudiante;
        }

        public async Task<Estudiante> UpdateAsync(Estudiante estudiante)
        {
            var existingEstudiante = await _context.Estudiantes.FindAsync(estudiante.Id);

            if (existingEstudiante == null)
                return null;

            // Actualizar solo propiedades que pueden modificarse
            existingEstudiante.Cedula = estudiante.Cedula;
            existingEstudiante.Nombre = estudiante.Nombre;
            existingEstudiante.Apellido1 = estudiante.Apellido1;
            existingEstudiante.Apellido2 = estudiante.Apellido2;
            existingEstudiante.Sexo = estudiante.Sexo;

            // No actualizamos el estado aquí, eso se gestiona por el servicio de matrícula

            await _context.SaveChangesAsync();

            _logger.LogActualizar("Estudiante", estudiante.Id);
            return existingEstudiante;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return false;

            // Verificar si el estudiante está matriculado
            if (estudiante.Estado == EstadoEstudiante.Matriculado)
                return false;

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            _logger.LogEliminar("Estudiante", id);
            return true;
        }

        public async Task<bool> CanDeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return false;

            return estudiante.Estado == EstadoEstudiante.NoMatriculado;
        }
    }
}