using eVault.Domain.Models;

namespace eVault.Infrastructure.Entities
{
    public class Notification : BaseEntity
    {
        public string NotificationText { get; set; }
    }
}
