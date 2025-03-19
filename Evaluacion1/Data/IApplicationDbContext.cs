using Evaluacion1.Models;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion1.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Estudiante> Estudiantes { get; }
        DbSet<Matricula> Matriculas { get; }
        DbSet<EventLog> EventLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();
    }
}
