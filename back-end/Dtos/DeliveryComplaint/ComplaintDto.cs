namespace BackEnd.Dtos.DeliveryComplaint
{
    public class GetComplaintInfo
    {
        public string ComplaintId { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public string ApplicationTime { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Punishment { get; set; } = "-";
        public string PunishmentReason { get; set; } = string.Empty;
        public string ProcessingNote { get; set; } = string.Empty;
    }

    public class SetComplaintInfo
    {
        public string ComplaintId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Punishment { get; set; } = "-";
        public string PunishmentReason { get; set; } = string.Empty;
        public string ProcessingNote { get; set; } = string.Empty;
    }

    public class SetComplaintInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetComplaintInfo? Data { get; set; }
    }
}
