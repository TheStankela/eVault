using eVault.Domain.Interfaces.Service;
using eVault.Infrastructure.Entities;
using eVault.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eVault.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IUserStoreFactory _userStoreFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserStoreFactory userStoreFactory)
            : base(options)
        {
            _userStoreFactory = userStoreFactory;
        }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<MenuItemUserRole> MenuItemUserRoles { get; set; }

        public virtual DbSet<Connection> Connections { get; set; }

        public virtual DbSet<AuditEntry> Audit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>().AddBaseEntityForeignKeys();
            modelBuilder.Entity<User>().AddBaseEntityForeignKeys();
            modelBuilder.Entity<Role>().AddBaseEntityForeignKeys();

            modelBuilder.Entity<Connection>(entity =>
            {
                entity.Property(e => e.ConnectionId)
                    .IsRequired()
                    .HasMaxLength(22); 
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var _userStore = _userStoreFactory.GetUserStore();

            foreach (var item in this.ChangeTracker.Entries()
            .Where(e => e.Entity is IBaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as IBaseEntity)
            )
            {
                if (item?.CreatedOn <= DateTime.MinValue)
                {
                    item.CreatedOn = DateTime.UtcNow;
                    item.CreatedById = _userStore.CurrentUserId;
                }
                else
                {
                    item.UpdatedOn = DateTime.UtcNow;
                    item.UpdatedById = _userStore.CurrentUserId;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
