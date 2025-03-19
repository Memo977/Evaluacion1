using Evaluacion1.Models;

namespace Evaluacion1.Services.Interfaces
{
    public interface IMatriculaService
    {
        Task<IEnumerable<Matricula>> GetAllAsync();
        Task<Matricula> GetByIdAsync(int id);
        Task<Matricula> CreateAsync(int estudianteId);
        Task<bool> CanMatricularAsync(int estudianteId);
        Task<bool> DeleteAsync(int id);
    }
}
