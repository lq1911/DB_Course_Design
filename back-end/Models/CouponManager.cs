using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class CouponManager
    {
        // 优惠券管理类
        // 主码：CouponManagerID
        // 外码：SellerID，StoreID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouponManagerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CouponName { get; set; } = string.Empty;

        [Required]
        public CouponType CouponType { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumSpend { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? DiscountRate { get; set; }

        [Required]
        public int TotalQuantity { get; set; }

        public int UsedQuantity { get; set; } = 0;

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; } = null!;

        // 一对多导航属性
        // 优惠券
        public ICollection<Coupon>? Coupons { get; set; }

        // 计算属性：优惠券状态
        [NotMapped]
        public string Status
        {
            get
            {
                var now = DateTime.Now;
                if (now < ValidFrom) return "upcoming";
                if (now > ValidTo) return "expired";
                return "active";
            }
        }

        // 计算属性：是否过期
        [NotMapped]
        public bool IsExpired => DateTime.Now > ValidTo;

        // 计算属性：是否未开始
        [NotMapped]
        public bool IsUpcoming => DateTime.Now < ValidFrom;
    }
}
