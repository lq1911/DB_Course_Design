using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

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
        public DateTime OrderTime { get; set; } = DateTime.Now;

        public DateTime? PaymentTime { get; set; } // 改为可空，支付后才有值

        [StringLength(255)]
        public string? Remarks { get; set; }

        [Required]
        public FoodOrderState FoodOrderState { get; set; } = FoodOrderState.Pending;

        // 新增配送任务的导航属性
        public DeliveryTask? DeliveryTask { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        public int? CartID { get; set; }
        [ForeignKey("CartID")]
        public ShoppingCart? Cart { get; set; } = null!;

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        // 一对多导航属性
        // 优惠券
        public ICollection<Coupon>? Coupons { get; set; }

        // 售后申请
        public ICollection<AfterSaleApplication>? AfterSaleApplications { get; set; }

        // 评论
        public ICollection<Comment>? Comments { get; set; }
    }
}
