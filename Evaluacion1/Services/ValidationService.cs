using Evaluacion1.Models;
using Evaluacion1.Services.Interfaces;

namespace Evaluacion1.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidarEstudiante(Estudiante estudiante, out Dictionary<string, string> errores)
        {
            errores = new Dictionary<string, string>();

            // Validar que campos requeridos tengan valores
            if (string.IsNullOrWhiteSpace(estudiante.Nombre))
                errores.Add("Nombre", "El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(estudiante.Apellido1))
                errores.Add("Apellido1", "El primer apellido es obligatorio");

            if (string.IsNullOrWhiteSpace(estudiante.Apellido2))
                errores.Add("Apellido2", "El segundo apellido es obligatorio");

            // Validar cédula
            if (!ValidarCedula(estudiante.Cedula))
                errores.Add("Cedula", "El formato de la cédula no es válido");

            return errores.Count == 0;
        }

        public bool ValidarCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return false;

            return cedula.All(char.IsDigit) && cedula.Length >= 9 && cedula.Length <= 11;
        }
    }
}
