using eVault.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace eVault.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in this.ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Models.IBaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as Domain.Models.IBaseEntity)
            )
            {
                if (item.CreatedOn <= DateTime.MinValue)
                {
                    item.CreatedOn = DateTime.UtcNow;
                    item.CreatedBy = Guid.NewGuid(); //temp
                }
                else
                {
                    item.UpdatedOn = DateTime.UtcNow;
                    item.UpdatedBy = Guid.NewGuid(); //temp
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
