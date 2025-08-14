using BackEnd.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Coupon
    {
        // 优惠券类
        // 主码：CouponID
        // 外码：CouponManagerID，CustomerID，OrderID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouponID { get; set; }

        public CouponState CouponState { get; set; } = CouponState.Unused;

        [Required]
        public int CouponManagerID { get; set; }
        [ForeignKey("CouponManagerID")]
        public CouponManager CouponManager { get; set; } = null!;

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        public int? OrderID { get; set; }
        [ForeignKey("OrderID")]
        public FoodOrder? Order { get; set; }

        [NotMapped]
        public bool IsExpired => CouponManager.ValidTo < DateTime.Now;

    }
}
