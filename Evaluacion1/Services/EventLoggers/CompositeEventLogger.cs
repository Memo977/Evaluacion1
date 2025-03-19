using Evaluacion1.Services.Interfaces;

namespace Evaluacion1.Services.EventLoggers
{
    public class CompositeEventLogger : IEventLogger
    {
        private readonly IEnumerable<IEventLogger> _loggers;

        public CompositeEventLogger(IEnumerable<IEventLogger> loggers)
        {
            _loggers = loggers;
        }

        public void LogConsulta(string entidad)
        {
            foreach (var logger in _loggers)
            {
                logger.LogConsulta(entidad);
            }
        }

        public void LogAgregar(string entidad, int id)
        {
            foreach (var logger in _loggers)
            {
                logger.LogAgregar(entidad, id);
            }
        }

        public void LogActualizar(string entidad, int id)
        {
            foreach (var logger in _loggers)
            {
                logger.LogActualizar(entidad, id);
            }
        }

        public void LogEliminar(string entidad, int id)
        {
            foreach (var logger in _loggers)
            {
                logger.LogEliminar(entidad, id);
            }
        }
    }
}
