using eVault.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace eVault.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IPrincipal _principal;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPrincipal principal) : base(options)
        {
            _principal = principal;
        }
        public DbSet<Notification> Notifications { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in this.ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Models.BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as Domain.Models.BaseEntity)
            )
            {
                if (item.CreatedOn <= DateTime.MinValue)
                {
                    item.CreatedOn = DateTime.UtcNow;
                    item.CreatedBy = Guid.NewGuid();
                }
                else
                {
                    item.UpdatedOn = DateTime.UtcNow;
                    item.UpdatedBy = Guid.NewGuid();
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
