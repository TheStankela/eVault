using System.Reflection.Emit;
using eVault.Domain.Interfaces.Service;
using eVault.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eVault.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IUserStoreFactory _userStoreFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserStoreFactory userStoreFactory)
            : base(options)
        {
            _userStoreFactory = userStoreFactory;
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AuditEntry> Audit { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Domain.Models.IBaseEntity>()
            .HasQueryFilter(e => !e.IsActive);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var _userStore = _userStoreFactory.GetUserStore();

            foreach (var item in this.ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Models.IBaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as Domain.Models.IBaseEntity)
            )
            {
                if (item?.CreatedOn <= DateTime.MinValue)
                {
                    item.CreatedOn = DateTime.UtcNow;
                    item.CreatedBy = _userStore.CurrentUserId;
                }
                else
                {
                    item.UpdatedOn = DateTime.UtcNow;
                    item.UpdatedBy = _userStore.CurrentUserId;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
