using eVault.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace eVault.Infrastructure.Entities
{
    public class User : IdentityUser, IBaseEntity
    {
        //IBaseEntity
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
