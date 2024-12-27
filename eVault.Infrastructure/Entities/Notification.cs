using eVault.Domain.Attributes;
using eVault.Domain.Enums;
using eVault.Domain.Models;

namespace eVault.Infrastructure.Entities
{
    [Audit(AuditEntityType.Notification)]
    public class Notification : BaseEntity, IAuditableEntity
    {
        public string NotificationText { get; set; }
    }
}
