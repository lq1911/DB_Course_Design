using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class DeliveryComplaint
    {
        // 配送投诉类
        // 主码：ComplaintID
        // 外码：CourierID，CustomerID，DeliveryTaskID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintID { get; set; }

        [Required]
        [StringLength(255)]
        public string ComplaintReason { get; set; } = null!;

        [Required]
        public DateTime ComplaintTime { get; set; }

        [Required]
        public int CourierID { get; set; }
        [ForeignKey("CourierID")]
        public Courier Courier { get; set; } = null!;

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        [Required]
        public int DeliveryTaskID { get; set; }
        [ForeignKey("DeliveryTaskID")]
        public DeliveryTask DeliveryTask { get; set; } = null!;

        // 多对多关系
        // 可以由多个管理员负责
        public ICollection<Evaluate_Complaint> EvaluateComplaints { get; set; } = new List<Evaluate_Complaint>();
    }
}
