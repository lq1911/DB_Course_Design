using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.AuthRequest
{
    public class RegisterRequest
    {
        public required string Nickname { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "手机号不能为空")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public required string Birthday { get; set; }
        public required string VerificationCode { get; set; }
        public required string Role { get; set; }
        public string? AvatarUrl { get; set; }
        public int IsPublic { get; set; }

        public RiderInfoDto? RiderInfo { get; set; }
        public AdminInfoDto? AdminInfo { get; set; }
        public StoreInfoDto? StoreInfo { get; set; }
    }

    public class RiderInfoDto
    {
        public required string VehicleType { get; set; }
        public required string Name { get; set; }
    }

    public class AdminInfoDto
    {
        public required string ManagementObject { get; set; }
        public required string Name { get; set; }

    }

    public class StoreInfoDto
    {
        public required string SellerName { get; set; }
        public required string StoreName { get; set; }
        public required string Address { get; set; }
        public required string OpenTime { get; set; }
        public required string CloseTime { get; set; }
        public required string EstablishmentDate { get; set; }
        public required IFormFile BusinessLicense { get; set; }
        public required string Category { get; set; }
    }

    public class RegisterResult
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; } = "";
    }
}
