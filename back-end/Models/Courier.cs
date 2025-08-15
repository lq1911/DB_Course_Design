using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models
{
    // ������
    // ���룺UserID
    // ���룺UserID

    public class Courier
    {
        [Key, ForeignKey("User")]
        public int UserID { get; set; }
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

       
        public ICollection<DeliveryTask>? DeliveryTasks { get; set; }

        // 新增属性：表示骑手当前是否在线
         [Required]
        public bool IsOnline { get; set; } = false; // 默认为离线

        // 新增属性：记录最近一次开工的时间
        public DateTime? LastOnlineTime { get; set; } // 可为空，因为离线时没有开工时间
    }
}
