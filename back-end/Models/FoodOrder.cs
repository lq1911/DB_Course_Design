using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class FoodOrder
    {
        // 出餐订单类
        // 主码：OrderID
        // 外码：CustomerID，CartID，StoreID，SellerID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required]
        public DateTime PaymentTime { get; set; }

        [StringLength(255)]
        public string? Remarks { get; set; }

        // 订单评分相关字段
        [Range(1, 5)]  // 限制评分范围为1-5
        [Column(TypeName = "decimal(2,1)")] 
        public decimal? Rating { get; set; } 

        [StringLength(500)]
        public string? RatingComment { get; set; }  // 评价内容

        public DateTime? RatingTime { get; set; }  // 评价时间

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        [Required]
        public int CartID { get; set; }
        [ForeignKey("CartID")]
        public ShoppingCart Cart { get; set; } = null!;

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        [Required]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; } = null!;

        // 一对多导航属性
        // 优惠券
        public ICollection<Coupon>? Coupons { get; set; }

        // 售后申请
        public ICollection<AfterSaleApplication>? AfterSaleApplications { get; set; }
    }
}
