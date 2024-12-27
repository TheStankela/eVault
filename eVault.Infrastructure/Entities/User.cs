using eVault.Domain.Attributes;
using eVault.Domain.Enums;
using eVault.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace eVault.Infrastructure.Entities
{
    [Audit(AuditEntityType.User)]
    public class User : IdentityUser, IBaseEntity
    {
        //IBaseEntity
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
