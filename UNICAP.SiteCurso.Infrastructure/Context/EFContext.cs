using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Infrastructure.Context
{
    public class EFContext : DbContext, IEFContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }

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
