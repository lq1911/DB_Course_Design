using BackEnd.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class User
    {
        // 用户类
        // 主码：UserID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(60)]
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

        public ProfilePrivacyLevel IsProfilePublic { get; set; } = ProfilePrivacyLevel.Private;

        // 用户的角色名称
        [Required]
        public UserIdentity Role { get; set; } = UserIdentity.Customer;

        // 用户的身份
        public Customer? Customer { get; set; }
        public Courier? Courier { get; set; }
        public Administrator? Administrator { get; set; }
        public Seller? Seller { get; set; }
    }
}
