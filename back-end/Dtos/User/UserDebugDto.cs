using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public DateTime PaymentTime { get; set; }  // 支付时间

        [Required]
        public int CustomerId { get; set; }        // 用户编号

        [Required]
        public int CartId { get; set; }            // 购物车编号

        [Required]
        public int StoreId { get; set; }           // 店铺编号
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