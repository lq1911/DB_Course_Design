using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouponID { get; set; }

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

        public int? ApplicableStoreID { get; set; }
        [ForeignKey("ApplicableStoreID")]
        public Store Store { get; set; }

        public int? OrderID { get; set; }
        [ForeignKey("OrderID")]
        public FoodOrder? Order { get; set; }

        [Required]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; } = null!;

        // 添加缺少的属性
        public int? CouponManagerID { get; set; }
        [ForeignKey("CouponManagerID")]
        public CouponManager? CouponManager { get; set; }

        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        [Required]
        public BackEnd.Models.Enums.CouponState CouponState { get; set; } = BackEnd.Models.Enums.CouponState.Unused;
    }

}
