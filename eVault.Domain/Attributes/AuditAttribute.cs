using eVault.Domain.Enums;

namespace eVault.Domain.Attributes
{
    public class AuditAttribute : Attribute
    {
        public eAuditEntityType EntityType { get; set; }
        public AuditAttribute(eAuditEntityType entityType)
        {
            EntityType = entityType;
        }
    }
}
