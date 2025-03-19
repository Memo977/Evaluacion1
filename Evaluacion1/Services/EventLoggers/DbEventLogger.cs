using Evaluacion1.Data;
using Evaluacion1.Models;
using Evaluacion1.Services.Interfaces;
using System;

namespace Evaluacion1.Services.EventLoggers
{
    public class DbEventLogger : IEventLogger
    {
        private readonly IApplicationDbContext _context;

        public DbEventLogger(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void LogConsulta(string entidad)
        {
            LogToDatabase($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Consulta - {entidad}.");
        }

        public void LogAgregar(string entidad, int id)
        {
            LogToDatabase($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Agregar - {entidad}: {id}.");
        }

        public void LogActualizar(string entidad, int id)
        {
            LogToDatabase($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Actualizar - {entidad}: {id}.");
        }

        public void LogEliminar(string entidad, int id)
        {
            LogToDatabase($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Eliminar - {entidad}: {id}.");
        }

        private void LogToDatabase(string message)
        {
            try
            {
                _context.EventLogs.Add(new EventLog
                {
                    Timestamp = DateTime.Now,
                    Message = message
                });
                _context.SaveChangesAsync().GetAwaiter().GetResult(); // Usa el método async de forma síncrona
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
            }
        }
    }
}