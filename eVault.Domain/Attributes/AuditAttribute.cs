using eVault.Domain.Enums;

namespace eVault.Domain.Attributes
{
    public class AuditAttribute : Attribute
    {
        public AuditEntityType EntityType { get; set; }
        public AuditAttribute(AuditEntityType entityType)
        {
            EntityType = entityType;
        }
    }
}
