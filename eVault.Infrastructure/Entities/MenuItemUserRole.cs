namespace eVault.Infrastructure.Entities
{
    public class MenuItemUserRole
    {
        public Guid Id { get; set; }

        public Guid MenuItemId { get; set; }

        public MenuItem? MenuItem { get; set; }

        public Guid UserRoleId { get; set; }

        public Role UserRole { get; set; }

        public Guid IsActive { get; set; }
    }
}
