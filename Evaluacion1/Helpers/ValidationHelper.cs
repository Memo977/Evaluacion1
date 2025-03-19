namespace Evaluacion1.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidarCedula(string cedula)
        {
            // Valida formato de cédula
            if (string.IsNullOrWhiteSpace(cedula))
                return false;

            return cedula.All(char.IsDigit) && cedula.Length >= 9 && cedula.Length <= 11;
        }
    }
}
