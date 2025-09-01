using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Evaluate_Complaint
    {
        // 管理员与配送投诉之间的评估投诉关系
        // 主码：AdminID，ComplaintID

        [Key, Column(Order = 0)]
        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public Administrator Admin { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int ComplaintID { get; set; }
        [ForeignKey("ComplaintID")]
        public DeliveryComplaint Complaint { get; set; } = null!;
    }
}
