using Evaluacion1.Services.Interfaces;

namespace Evaluacion1.Services.EventLoggers
{
    public class ConsoleEventLogger : EventLogger
    {
        protected override void WriteLog(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}