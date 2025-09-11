using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Dtos.User
{
    public class UserInfoResponseDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        public string DefaultAddress { get; set; } = string.Empty;
    }

    public class SubmitOrderRequestDto
    {
        [Required]
        public string PaymentTime { get; set; } = null!;  // 支付时间

        [Required]
        public int CustomerId { get; set; }        // 用户编号

        [Required]
        public int CartId { get; set; }            // 购物车编号

        [Required]
        public int StoreId { get; set; }           // 店铺编号

        [Required]
        [Column(TypeName = "decimal(5,2)")] // 精确定义数据库类型，最大值为 999.99
        public decimal DeliveryFee { get; set; } = 0.00m; // 默认为 0
    }

    public class UsedCouponDto
    {
        [Required]
        public int CouponId { get; set; }
    }

    public class GetUserIdRequestDto
    {
        public string Account { get; set; } = string.Empty;  // 用户手机号或邮箱
    }

    public class GetUserIdResponseDto
    {
        public int Id { get; set; } // 用户编号
    }
}