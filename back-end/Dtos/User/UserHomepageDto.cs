using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Dtos.User
{

    public class HomeRecmDto
    {
        [Required]
        public IEnumerable<ShowStoreDto> RecomStore { get; set; } = Array.Empty<ShowStoreDto>();
    }

    public class HomeSearchDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [FromQuery(Name = "address")]

        public string Address { get; set; } = null!;

        [Required]
        [FromQuery(Name = "keyword")]
        public string Keyword { get; set; } = null!;
    }

    public class HomeSearchGetDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public int MonthlySales { get; set; }

    }

    public class UserIdDto
    {
        [Required]
        public int UserId { get; set; }
    }

    public class UserInfoDto
    {
        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = null!;

        [Required]
        public long PhoneNumber { get; set; }

        [MaxLength(255)]
        public string? Avatar { get; set; }
    }
    // 订单中的单个菜品DTO
    public class DishDto
    {
        public int DishID { get; set; }
        public string DishName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? DishImage { get; set; }
    }
    public class HistoryOrderDto
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public string PaymentTime { get; set; } = string.Empty;

        [Required]
        public int CartID { get; set; }

        [Required]
        public int StoreID { get; set; }

        [Required]
        public string StoreImage { get; set; } = string.Empty;

        [Required]
        public string StoreName { get; set; } = string.Empty;

        [Required]
        public List<string> DishImage { get; set; } = new List<string>();

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public FoodOrderState OrderStatus { get; set; }
    }

    public class HistoryOrderGetDto
    {
        [Required]
        public List<HistoryOrderDto> Orders { get; set; } = new List<HistoryOrderDto>();
    }
    // 用户信息响应DTO
    public class UserInfoResponse
    {
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Image { get; set; }
    }

    public class CouponDto
    {
        [Key]
        public int CouponID { get; set; }

        public CouponState CouponState { get; set; } = CouponState.Unused;

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
        public DateTime ValidTo { get; set; }
    }
    public class ShowStoreDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }
    }
     public class StoresResponseDto
    {
        public List<ShowStoreDto> AllStores { get; set; } = new List<ShowStoreDto>();
    }
}