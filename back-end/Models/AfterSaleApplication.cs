using BackEnd.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class AfterSaleApplication
    {
        // 售后申请类
        // 主码：ApplicationID
        // 外码：CustomerID，OrderID，SellerID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationID { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime ApplicationTime { get; set; }

        // 新增售后申请状态
        [Required]
        public AfterSaleState AfterSaleState { get; set; } = AfterSaleState.Pending;

        // 处理措施
        [StringLength(255)]
        public string? ProcessingResult { get; set; } = "-";

        // 处理原因
        [StringLength(255)]
        public string? ProcessingReason { get; set; }

        // 处理备注
        [StringLength(255)]
        public string? ProcessingRemark { get; set; }

        [Required]
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public FoodOrder Order { get; set; } = null!;

        // 多对多关系
        // 可以由多个管理员负责
        public ICollection<Evaluate_AfterSale> EvaluateAfterSales { get; set; } = new List<Evaluate_AfterSale>();
    }
}
