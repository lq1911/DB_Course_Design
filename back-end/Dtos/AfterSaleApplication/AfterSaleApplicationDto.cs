namespace BackEnd.Dtos.AfterSaleApplication
{
    public class GetAfterSaleApplicationInfo
    {
        public string ApplicationId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string ApplicationTime { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Punishment { get; set; } = "-";
        public string PunishmentReason { get; set; } = string.Empty;
        public string ProcessingNote { get; set; } = string.Empty;
    }

    public class SetAfterSaleApplicationInfo
    {
        public string ApplicationId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Punishment { get; set; } = "-";
        public string PunishmentReason { get; set; } = string.Empty;
        public string ProcessingNote { get; set; } = string.Empty;
    }

    public class SetAfterSaleApplicationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAfterSaleApplicationInfo? Data { get; set; }
    }
}
