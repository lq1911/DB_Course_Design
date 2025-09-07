using BackEnd.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Store
    {
        // 店铺类
        // 主码：StoreID
        // 外码：SellerID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string StoreAddress { get; set; } = null!;

        // 开业时间
        [Required]
        public TimeSpan OpenTime { get; set; } = TimeSpan.FromHours(9);  // 09:00

        [Required]
        public TimeSpan CloseTime { get; set; } = TimeSpan.FromHours(22); // 22:00

        [NotMapped]  // 表示该属性不会映射到数据库表中
        public bool IsOpen
        {
            get
            {
                var now = DateTime.Now.TimeOfDay;

                if (OpenTime <= CloseTime)
                    return now >= OpenTime && now <= CloseTime;
                else
                    return now >= OpenTime || now <= CloseTime;
            }
        }

        [NotMapped]
        public string BusinessHoursDisplay =>
            $"{OpenTime:hh\\:mm} - {CloseTime:hh\\:mm}";

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }

        [StringLength(500)]
        public string? StoreFeatures { get; set; }

        [Required]
        public DateTime StoreCreationTime { get; set; }

        // 新增店铺状态
        [Required]
        public StoreState StoreState { get; set; } = StoreState.IsOperation;

        // 店铺种类
        [Required]
        public string StoreCategory { get; set; } = null!;

        // 新增店铺图片
        public string? StoreImage { get; set; }

        [Required]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; } = null!;

        // 一对多导航属性
        // 出餐订单
        public ICollection<FoodOrder>? FoodOrders { get; set; }

        // 优惠券类
        public ICollection<CouponManager>? CouponManagers { get; set; }

        // 菜单
        public ICollection<Menu>? Menus { get; set; }

        // 收藏夹项
        public ICollection<FavoriteItem>? FavoriteItems { get; set; }

        // 违规店铺处罚
        public ICollection<StoreViolationPenalty>? StoreViolationPenalties { get; set; }

        // 评论
        public ICollection<Comment>? Comments { get; set; }

        // 配送任务
        public ICollection<DeliveryTask>? DeliveryTasks { get; set; }
    }
}
