using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Application.Interfaces
{
    public interface IEFContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Article> Articles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        EntityEntry Update(object obj);
        IDbContextTransaction BeginTransaction();
        void Migrate();
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
