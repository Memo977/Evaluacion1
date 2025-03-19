using Evaluacion1.Services.Interfaces;

namespace Evaluacion1.Services.EventLoggers
{
    public abstract class EventLogger : IEventLogger
    {
        protected abstract void WriteLog(string mensaje);

        protected string FormatMessage(string tipo, string entidad, int? id = null)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm}] {tipo} - {entidad}{(id.HasValue ? $": {id}" : "")}.";
        }

        public void LogConsulta(string entidad)
        {
            WriteLog(FormatMessage("Consulta", entidad));
        }

        public void LogAgregar(string entidad, int id)
        {
            WriteLog(FormatMessage("Agregar", entidad, id));
        }

        public void LogActualizar(string entidad, int id)
        {
            WriteLog(FormatMessage("Actualizar", entidad, id));
        }

        public void LogEliminar(string entidad, int id)
        {
            WriteLog(FormatMessage("Eliminar", entidad, id));
        }
    }
}
