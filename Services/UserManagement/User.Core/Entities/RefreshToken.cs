using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace User.Core.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Token { get; set; }
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime DateExpiresUtc { get; set; }
        public bool IsExpired => DateTime.UtcNow >= DateExpiresUtc;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
