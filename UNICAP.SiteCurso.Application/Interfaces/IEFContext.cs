using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace UNICAP.SiteCurso.Application.Interfaces
{
    public interface IEFContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        EntityEntry Update(object obj);
        IDbContextTransaction BeginTransaction();
        void Migrate();
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
