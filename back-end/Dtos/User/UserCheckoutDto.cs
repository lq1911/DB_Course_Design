using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Dtos.User
{
    public class UserCouponDto
    {
        [Required]
        public int CouponID { get; set; }

        [Required]
        public CouponState CouponState { get; set; }

        [Required]
        public int? OrderID { get; set; }

        [Required]
        public int CouponManagerID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumSpend { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        public string ValidTo { get; set; }
    }

    public class CartRequestDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int StoreId { get; set; }
    }

    public class CartResponseDto
    {
        [Required]
        public int CartId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; } = 0.00m;

        [Required]
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();

    }

    public class ShoppingCartItemDto
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int DishId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; } = 0.00m;
    }

    public class UpdateCartItemDto
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int DishId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class RemoveCartItemDto
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int DishId { get; set; }
    }
}