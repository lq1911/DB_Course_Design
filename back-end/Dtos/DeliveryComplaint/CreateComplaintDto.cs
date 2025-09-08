using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.DeliveryComplaint
{
    public class CreateComplaintDto
    {
        [Required(ErrorMessage = "配送任务ID不能为空")]
        public string DeliveryTaskId { get; set; } = null!;

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
