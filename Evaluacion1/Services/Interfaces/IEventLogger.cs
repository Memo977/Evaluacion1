namespace Evaluacion1.Services.Interfaces
{
    public interface IEventLogger
    {
        void LogConsulta(string entidad);
        void LogAgregar(string entidad, int id);
        void LogActualizar(string entidad, int id);
        void LogEliminar(string entidad, int id);
    }
}
