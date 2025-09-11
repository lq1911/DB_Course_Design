namespace BackEnd.Dtos.Courier
{
    // 这个类严格匹配前端 Complaint 接口中的 'punishment' 对象
    public class PunishmentDto
    {
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Duration { get; set; } // 可为空，因为它是可选的
    }

    // 这个类严格匹配前端的 Complaint 接口
    public class ComplaintDto
    {
        public string ComplaintID { get; set; } = null!;
        public string DeliveryTaskID { get; set; } = null!;
        public string ComplaintTime { get; set; } = null!;
        public string ComplaintReason { get; set; } = null!;
        public PunishmentDto? Punishment { get; set; } // 可为空，因为它是可选的
    }
}