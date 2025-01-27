using System.ComponentModel.DataAnnotations;

namespace eVault.Infrastructure.Entities
{
    public class MenuItem
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public MenuItem Parent { get; set; }

        [MaxLength(30)]
        public string Label { get; set; }

        [MaxLength(30)]
        public string Icon { get; set; }

        [MaxLength(30)]
        public string Url { get; set; }

        [MaxLength(30)]
        public string TabIndex { get; set; }

        public List<MenuItem>? Items { get; set; }

        [MaxLength(30)]
        public string? Badge { get; set; }

        [MaxLength(100)]
        public string? Tooltip { get; set; }

        [MaxLength(30)]
        public string? Title { get; set; }

        public bool IsActive { get; set; }
    }
}
