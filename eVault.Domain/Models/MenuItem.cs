namespace eVault.Domain.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public MenuItem? Parent { get; set; }

        public string Label { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public string TabIndex { get; set; }

        public List<MenuItem>? Items { get; set; }

        public string? Badge { get; set; }

        public string? Tooltip { get; set; }

        public string? Title { get; set; }

        public bool IsActive { get; set; }
    }
}
