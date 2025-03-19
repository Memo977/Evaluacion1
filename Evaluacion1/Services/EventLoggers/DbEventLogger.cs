using Evaluacion1.Data;
using Evaluacion1.Models;
using Evaluacion1.Services.Interfaces;
using System;

namespace Evaluacion1.Services.EventLoggers
{
    public class DbEventLogger : EventLogger
    {
        private readonly IApplicationDbContext _context;

        public DbEventLogger(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override void WriteLog(string mensaje)
        {
            try
            {
                _context.EventLogs.Add(new EventLog
                {
                    Timestamp = DateTime.Now,
                    Message = mensaje
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