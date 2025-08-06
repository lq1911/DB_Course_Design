using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class Customer
    {
        // 消费者类
        // 主码：UserID
        // 外码：USerID

        [Key, ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        [StringLength(100)]
        public string? DefaultAddress { get; set; }

        public int ReputationPoints { get; set; } = 0;

        public MembershipStatus IsMember { get; set; } = MembershipStatus.NotMember;

        // 一对多导航属性
        // 配送任务
        public ICollection<DeliveryTask>? DeliveryTasks { get; set; }

        // 出餐订单
        public ICollection<FoodOrder>? FoodOrders { get; set; }

        // 优惠券
        public ICollection<Coupon>? Coupons { get; set; }

        // 收藏夹
        public ICollection<FavoritesFolder>? FavoritesFolders { get; set; }

        // 评论
        public ICollection<Comment>? Comments { get; set; }
    }
}
