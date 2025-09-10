using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [Required]
        [Column(TypeName = "decimal(5,2)")] // 精确定义数据库类型，最大值为 999.99
        [JsonPropertyName("deliveryFee")]
        public decimal DeliveryFee { get; set; } = 0.00m; // 默认为 0

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
        public long PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;
    }

    public class ResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}