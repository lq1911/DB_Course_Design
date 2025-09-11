using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.DeliveryComplaint
{
    public class CreateComplaintDto
    {
        public int? OrderId { get; set; }   // 新增：订单编号
        public int? DeliveryTaskId { get; set; }   // 仍然支持任务编号

        [Required(ErrorMessage = "投诉原因不能为空")]
        [StringLength(255, ErrorMessage = "投诉原因不能超过255个字符")]
        public string ComplaintReason { get; set; } = null!;
    }

    public class CreateComplaintResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public int? ComplaintId { get; set; }
    }
}
