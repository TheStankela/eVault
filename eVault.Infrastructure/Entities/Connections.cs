using System.ComponentModel.DataAnnotations;

namespace eVault.Infrastructure.Entities
{
    public partial class Connection
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public string ConnectionId { get; set; }

        public DateTime ConnectedAt { get; set; }
    }
}
