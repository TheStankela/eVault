using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eVault.Domain.Enums;

namespace eVault.Infrastructure.Entities
{
    public class AuditEntry
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ObjectId { get; set; }

        public AuditEntityType EntityType { get; set; }

        [NotMapped]
        public List<AuditEntryChanges> AuditChanges { get; set; } = new();

        public string Changes { get; set; }

        public DatabaseOperation Operation { get; set; }

        public Guid? CreatedById { get; set; }

        public User? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }

    public class AuditEntryChanges
    {
        public string? PropertyName { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }
    }
}
