namespace eVault.Domain.Models
{
    public interface IAuditableEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
