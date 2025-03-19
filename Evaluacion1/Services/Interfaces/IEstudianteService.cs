using Evaluacion1.Models;

namespace Evaluacion1.Services.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante> GetByIdAsync(int id);
        Task<Estudiante> CreateAsync(Estudiante estudiante);
        Task<Estudiante> UpdateAsync(Estudiante estudiante);
        Task<bool> DeleteAsync(int id);
        Task<bool> CanDeleteAsync(int id);
    }
}
