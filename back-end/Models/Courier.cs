using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    // 骑手类
    // 主码：UserID
    // 外码：UserID

    public class Courier
    {
        [Key]
        public int UserID { get; set; }
        [NotMapped]
        public User User { get; set; } = null!;

        [Required]
        public DateTime CourierRegistrationTime { get; set; }

        [Required]
        [StringLength(20)]
        public string VehicleType { get; set; } = null!;

        public int ReputationPoints { get; set; } = 0;
        public int TotalDeliveries { get; set; } = 0;
        public int AvgDeliveryTime { get; set; } = 0;

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        public int MonthlySalary { get; set; } = 0;

        // 新增属性：表示骑手当前是否在线
        [Required]
        public CourierIsOnline IsOnline { get; set; } = CourierIsOnline.Offline; // 默认为离线

        [Column(TypeName = "decimal(10,6)")]
        public decimal? CourierLongitude { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal? CourierLatitude { get; set; }

        public DateTime? LastOnlineTime { get; set; }

        // 一对多导航属性
        // 配送任务
        public ICollection<DeliveryTask>? DeliveryTasks { get; set; }
    }
}
