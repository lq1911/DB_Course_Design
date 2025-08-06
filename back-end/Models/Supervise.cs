using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Supervise
    {
        // 管理员与违规店铺处罚之间的监督关系
        // 主码：AdminID，PenaltyID

        [Key, Column(Order = 0)]
        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public Administrator Admin { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int PenaltyID { get; set; }
        [ForeignKey("PenaltyID")]
        public StoreViolationPenalty Penalty { get; set; } = null!;
    }
}
