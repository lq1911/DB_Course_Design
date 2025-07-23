using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class User
    { 
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Password { get; set; } = null!;

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [MaxLength(2)]
        public string? Gender { get; set; }

        [MaxLength(6)]
        public string? FullName { get; set; }

        [MaxLength(255)]
        public string? Avatar { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public DateTime AccountCreationTime { get; set; }

        public int IsProfilePublic { get; set; } = 0;
    }
}
