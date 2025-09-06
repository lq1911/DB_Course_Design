namespace BackEnd.Dtos.ViolationPenalty
{
    public class GetViolationPenaltyInfo
    {
        public string PunishmentId { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string MerchantPunishment { get; set; } = null!;
        public string StorePunishment { get; set; } = null!;
        public string PunishmentTime { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string ProcessingNote { get; set; } = string.Empty;
    }

    public class SetViolationPenaltyInfo
    {
        public string PunishmentId { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string MerchantPunishment { get; set; } = null!;
        public string StorePunishment { get; set; } = null!;
        public string PunishmentTime { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string ProcessingNote { get; set; } = string.Empty;
    }

    public class SetViolationPenaltyInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetViolationPenaltyInfo? Data { get; set; }
    }
}
