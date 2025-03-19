using Evaluacion1.Services.Interfaces;

namespace Evaluacion1.Services.EventLoggers
{
    public class ConsoleEventLogger : IEventLogger
    {
        public void LogConsulta(string entidad)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Consulta - {entidad}.");
        }

        public void LogAgregar(string entidad, int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Agregar - {entidad}: {id}.");
        }

        public void LogActualizar(string entidad, int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Actualizar - {entidad}: {id}.");
        }

        public void LogEliminar(string entidad, int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Eliminar - {entidad}: {id}.");
        }
    }
}