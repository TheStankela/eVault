using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eVault.Infrastructure.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public Guid CreatedById { get; set; }

        public User? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedById { get; set; }

        public User? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        [NotMapped]
        public bool IsNew => Id == Guid.Empty;

        public bool IsActive { get; set; }
    }

    public interface IBaseEntity
    {
        public Guid CreatedById { get; set; }
        public User? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
