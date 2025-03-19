namespace Evaluacion1.Helpers
{
    public class MessageHelper
    {
        // Genera un mensaje de confirmación para operaciones CRUD
        public static string GenerarMensajeConfirmacion(string operacion, string entidad, bool exito)
        {
            if (exito)
                return $"La operación de {operacion} de {entidad} se ha realizado con éxito.";
            else
                return $"Ha ocurrido un error al realizar la operación de {operacion} de {entidad}.";
        }
    }
}
