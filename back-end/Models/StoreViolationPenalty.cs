using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class StoreViolationPenalty
    {
        // 违规店铺处罚类
        // 主码：PenaltyID
        // 外码：StoreID，SellerID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PenaltyID { get; set; }

        [Required]
        [StringLength(255)]
        public string PenaltyReason { get; set; } = null!;

        [Required]
        public DateTime PenaltyTime { get; set; }

        [StringLength(50)]
        public string? SellerPenalty { get; set; }

        [StringLength(50)]
        public string? StorePenalty { get; set; }

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        // 多对多关系
        // 可以由多个管理员负责
        public ICollection<Supervise_> Supervise_s { get; set; } = new List<Supervise_>();
    }
}
