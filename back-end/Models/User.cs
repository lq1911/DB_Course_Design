using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class User
    {
        //主键
        //这个属性的值由数据库自动生成，不要在代码中给这个字段赋值
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//
        public int UserID { get; set; }

        [Required]
        [StringLength(15)]
        public string Username { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; } // 密码哈希

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(6)]
        public string? Gender { get; set; }

        [StringLength(30)]
        public string? FullName { get; set; }

        [StringLength(255)]
        public string? Avatar { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public DateTime AccountCreationTime { get; set; }

        [Required]
        public ProfilePublicType IsProfilePublic { get; set; } // 使用枚举类型
    }
    public enum ProfilePublicType
    {
        Private = 0,
        Public = 1,
        FriendsOnly = 2
    }
}
