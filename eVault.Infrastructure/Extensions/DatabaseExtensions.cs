using eVault.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVault.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static IQueryable<T> Active<T>(this IQueryable<T> query) where T : IBaseEntity => query.Where(_ => _.IsActive);

        public static void AddBaseEntityForeignKeys<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IBaseEntity
        {
            builder.HasOne(e => e.CreatedBy)
                   .WithMany()
                   .HasForeignKey(e => e.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UpdatedBy)
                   .WithMany()
                   .HasForeignKey(e => e.UpdatedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
