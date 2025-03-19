using Evaluacion1.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Evaluacion1.Services.EventLoggers
{
    public class FileEventLogger : IEventLogger
    {
        private readonly string _logFilePath;

        public FileEventLogger(IWebHostEnvironment webHostEnvironment)
        {
            // Usar el directorio raíz del proyecto en lugar del directorio bin
            string projectRootPath = webHostEnvironment.ContentRootPath;
            Console.WriteLine($"Directorio raíz del proyecto: {projectRootPath}");

            // Crear carpeta logs en el directorio del proyecto
            string logsDirectory = Path.Combine(projectRootPath, "logs");
            Console.WriteLine($"Intentando crear directorio: {logsDirectory}");

            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
                Console.WriteLine("Directorio creado exitosamente");
            }
            else
            {
                Console.WriteLine("El directorio ya existe");
            }

            // Definir ruta del archivo de log
            _logFilePath = Path.Combine(logsDirectory, $"events_{DateTime.Now:yyyyMMdd}.log");
            Console.WriteLine($"Ruta del archivo: {_logFilePath}");

            // Crear archivo de prueba
            File.WriteAllText(_logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm}] Inicialización del log\r\n");
            Console.WriteLine("Archivo creado exitosamente");
        }

        // Resto del código permanece igual
        private void WriteToFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }

        public void LogConsulta(string entidad)
        {
            WriteToFile($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Consulta - {entidad}.");
        }

        public void LogAgregar(string entidad, int id)
        {
            WriteToFile($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Agregar - {entidad}: {id}.");
        }

        public void LogActualizar(string entidad, int id)
        {
            WriteToFile($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Actualizar - {entidad}: {id}.");
        }

        public void LogEliminar(string entidad, int id)
        {
            WriteToFile($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Eliminar - {entidad}: {id}.");
        }
    }
}