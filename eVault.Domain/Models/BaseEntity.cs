using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eVault.Domain.Models
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        [NotMapped]
        public bool IsNew => Id == Guid.Empty;

        public bool IsActive { get; set; }
    }

    public interface IBaseEntity
    {
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
