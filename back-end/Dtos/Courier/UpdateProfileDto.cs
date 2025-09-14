using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.Courier
{
    public class UpdateProfileDto
    {
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(15, ErrorMessage = "姓名长度不能超过15个字符")] // 匹配 User.Username 的 MaxLength
        public string Name { get; set; } = null!;

        [StringLength(2, ErrorMessage = "性别代码长度不能超过2个字符")] // 匹配 User.Gender 的 MaxLength
        public string? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(1000)] // 匹配 User.Avatar 的 MaxLength
        public string? Avatar { get; set; }

        [Required(ErrorMessage = "车辆类型不能为空")]
        [StringLength(20)] // 匹配 Courier.VehicleType 的 MaxLength
        public string VehicleType { get; set; } = null!;
    }
}