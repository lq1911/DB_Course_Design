using BackEnd.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class User
    {
        // �û���
        // ���룺UserID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Password { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; }

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

        // �û��Ľ�ɫ����
        [Required]
        public UserIdentity Role { get; set; } = UserIdentity.Customer;

        // �û�������
        public Customer? Customer { get; set; }
        public Courier? Courier { get; set; }
        public Administrator? Administrator { get; set; }
        public Seller? Seller { get; set; }
    }
}
