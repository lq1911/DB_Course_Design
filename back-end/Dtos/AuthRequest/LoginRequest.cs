using BackEnd.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.AuthRequest
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "手机号不能为空")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        public required string PhoneNum { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "角色不能为空")]
        public required string Role { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public UserInfo? User { get; set; }
    }
}
