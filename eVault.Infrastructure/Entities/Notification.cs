using eVault.Domain.Attributes;
using eVault.Domain.Enums;

namespace eVault.Infrastructure.Entities
{
    [Audit(eAuditEntityType.Notification)]
    public class Notification : BaseEntity, IAuditableEntity
    {
        public string NotificationText { get; set; }
    }
}
