using Microsoft.AspNetCore.Identity;

namespace eVault.Infrastructure.Entities
{
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
        public Guid CreatedById { get  ; set  ; }

        public User? CreatedBy { get  ; set  ; }

        public DateTime CreatedOn { get  ; set  ; }

        public Guid? UpdatedById { get  ; set  ; }

        public User? UpdatedBy { get  ; set  ; }

        public DateTime? UpdatedOn { get  ; set  ; }

        public bool IsActive { get  ; set  ; }
    }
}
