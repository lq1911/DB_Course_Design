using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Dtos.UserHomepage
{
    public class HomeRecmDto
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }
    }

    public class HomeSearchDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public Array UserAddress { get; set; } = null!;

        [Required]
        public string SearchName { get; set; } = null!;
    }

    public class HomeSearchGetDto
    {
        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = null!;

        [Required]
        public int MonthlySales { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreAddress { get; set; } = null!;
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

    public class UserOrderHistoryGetDto
    {
        [Required]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = null!;

        [Required]
        public int OrderID { get; set; }

        [Required]
        public DateTime PaymentTime { get; set; }

        [Required]
        public int CartID { get; set; }

        [Required]
        public string? StoreImage { get; set; } // Address of Image

        // public TYPE OrderedFood { get; set; }
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
}