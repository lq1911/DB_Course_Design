using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums

namespace BackEnd.Models
{
    public class Seller
    {
        // 商家类
        // 主码：UserID
        // 外码：UserID

        [Key, ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public DateTime SellerRegistrationTime { get; set; }

        public int ReputationPoints { get; set; } = 0;

        [Required]
        public SellerState BanStatus { get; set; } = SellerState.Normal;

        // 商家和店铺一对一
        [Required]
        public Store? Store { get; set; }

        // 一对多导航属性
        // 配送任务
        public ICollection<DeliveryTask>? DeliveryTasks { get; set; }

        // 出餐订单
        public ICollection<FoodOrder>? FoodOrders { get; set; }

        // 优惠券类
        public ICollection<CouponManager>? CouponsManager { get; set; }

        // 售后申请
        public ICollection<AfterSaleApplication>? AfterSaleApplications { get; set; }

        // 违规店铺处罚
        public ICollection<StoreViolationPenalty>? StoreViolationPenalties { get; set; }
    }
}
