using System.ComponentModel.DataAnnotations.Schema;

namespace People.Service.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        [Column("PasswordHash")]  // Map to your DB column
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
