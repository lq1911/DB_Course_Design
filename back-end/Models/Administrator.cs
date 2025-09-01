using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Administrator
    {
        // 管理员类
        // 主码：UserID
        // 外码：UserID

        [Key]
        public int UserID { get; set; }
        [NotMapped]
        public User User { get; set; } = null!;

        [Required]
        public DateTime AdminRegistrationTime { get; set; }

        [Required]
        [StringLength(50)]
        public string ManagedEntities { get; set; } = null!;

        [Column(TypeName = "decimal(3,2)")]
        public decimal IssueHandlingScore { get; set; } = 0.00m;

        // 多对多关系
        // 审核评论
        public ICollection<Review_Comment> ReviewComments { get; set; } = new List<Review_Comment>();

        // 监督违规店铺
        public ICollection<Supervise_> Supervise_s { get; set; } = new List<Supervise_>();

        // 处理售后请求
        public ICollection<Evaluate_AfterSale> EvaluateAfterSales { get; set; } = new List<Evaluate_AfterSale>();

        // 处理配送投诉
        public ICollection<Evaluate_Complaint> EvaluateComplaints { get; set; } = new List<Evaluate_Complaint>();

        // 便捷属性
        [NotMapped]
        public IEnumerable<Comment> Comments => ReviewComments.Select(rc => rc.Comment);

        [NotMapped]
        public IEnumerable<StoreViolationPenalty> Penalties => Supervise_s.Select(s => s.Penalty);

        [NotMapped]
        public IEnumerable<AfterSaleApplication> Applications => EvaluateAfterSales.Select(eas => eas.Application);

        [NotMapped]
        public IEnumerable<DeliveryComplaint> DeliveryComplaints => EvaluateComplaints.Select(ec => ec.Complaint);
    }
}
