namespace Evaluacion1.Helpers
{
    public class StringHelper
    {
        // Trunca un texto a un número máximo de caracteres
        public static string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength) + "...";
        }
    }
}
