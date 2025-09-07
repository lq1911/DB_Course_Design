using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.User
{
    public class CreateOrderDto
    {
        [Required]
        public int CartId { get; set; }         // 购物车 ID

        [Required]
        public int CustomerId { get; set; }     // 顾客 ID

        [Required]
        public int StoreId { get; set; }        // 店铺 ID

        [Required]
        public DateTime PaymentTime { get; set; }  // 支付时间

        public string? Remarks { get; set; }    // 订单备注（可空）
    }

    public class UpdateAccountDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public long PhoneNumber { get; set; } = 0;

        public string? Image { get; set; }
    }

    public class SaveAddressDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;
    }

    public class ResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}