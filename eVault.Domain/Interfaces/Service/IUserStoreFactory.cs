namespace eVault.Domain.Interfaces.Service
{
    public interface IUserStoreFactory
    {
        IUserStore GetUserStore();
    }
}
