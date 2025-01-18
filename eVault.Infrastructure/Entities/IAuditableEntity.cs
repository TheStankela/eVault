namespace eVault.Infrastructure.Entities
{
    public interface IAuditableEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
