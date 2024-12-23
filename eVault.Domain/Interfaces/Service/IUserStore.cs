namespace eVault.Domain.Interfaces.Service
{
    public interface IUserStore
    {
        public Guid CurrentUserId { get; set; }
    }
}
