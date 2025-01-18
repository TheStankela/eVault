using eVault.Domain.Attributes;
using eVault.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace eVault.Infrastructure.Entities
{
    [Audit(AuditEntityType.User)]
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public bool IsActive { get; set; }

        public Guid CreatedById { get; set; }

        public User? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedById { get; set; }

        public User? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
