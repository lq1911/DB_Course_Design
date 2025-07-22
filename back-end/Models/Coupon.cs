using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Order Order { get; set; }

        [Required]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; }
    }

}
