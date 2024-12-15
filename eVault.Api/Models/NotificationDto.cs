using eVault.Domain.Models;

namespace eVault.Api.Models
{
    public class NotificationDto : BaseEntity
    {
        public string NotificationText { get; set; }
    }
}
