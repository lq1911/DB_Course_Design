using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Customer_Coupon
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Key, Column(Order = 1)]
        public int CouponID { get; set; }
        [ForeignKey("CouponID")]
        public Coupon Coupon { get; set; }

        [Required]
        public int CouponQuantity { get; set; }
    }
}
