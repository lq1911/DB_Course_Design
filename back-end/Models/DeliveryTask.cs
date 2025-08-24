using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class DeliveryTask
    {
        // 配送任务类
        // 主码：TaskID
        // 外码：CustomerID，StoreID，SellerID，CourierID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }

        [Required]
        public DateTime EstimatedArrivalTime { get; set; }

        [Required]
        public DateTime EstimatedDeliveryTime { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal? CourierLongitude { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal? CourierLatitude { get; set; }

        // 发布任务时间
        [Required]
        public DateTime PublishTime { get; set; }

        // 接单时间
        [Required]
        public DateTime AcceptTime { get; set; }

        //8.13
        // 1. 任务的当前状态
        [Required]
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending; // 默认为配送中

        // 2. 任务实际完成的时间
        public DateTime? CompletionTime { get; set; } // 可为空，因为未完成时没有完成时间

        // --- 新增的属性 --- 8.14
        [Required]
        [Column(TypeName = "decimal(5,2)")] // 假设配送费最多为 999.99
        public decimal DeliveryFee { get; set; } = 0.00m; // 配送费

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        [Required]
        public int CourierID { get; set; }
        [ForeignKey("CourierID")]
        public Courier Courier { get; set; } = null!;

        // 一对多导航属性
        // 配送投诉
        public ICollection<DeliveryComplaint>? DeliveryComplaints { get; set; }
    }
}
