using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumSpend { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        // 一对多导航属性
        // 优惠券
        public ICollection<Coupon>? Coupons { get; set; }
    }
}
