using System.ComponentModel.DataAnnotations;

namespace eVault.Domain.Models
{
    public partial class Connection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string ConnectionId { get; set; }
        public DateTime ConnectedAt { get; set; }
    }
}
