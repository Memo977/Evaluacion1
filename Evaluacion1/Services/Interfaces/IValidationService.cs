using Evaluacion1.Models;

namespace Evaluacion1.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidarEstudiante(Estudiante estudiante, out Dictionary<string, string> errores);
        bool ValidarCedula(string cedula);
    }
}
