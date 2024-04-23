using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.Interfaces;

namespace UNICAP.SiteCurso.Infrastructure.Context
{
    public class EFContext : DbContext, IEFContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return base.Database.BeginTransactionAsync(cancellationToken);
        }

        public override EntityEntry Update(object obj)
        {
            return base.Update(obj);
        }

        public void Migrate()
        {
            base.Database.Migrate();
        }
    }
}
