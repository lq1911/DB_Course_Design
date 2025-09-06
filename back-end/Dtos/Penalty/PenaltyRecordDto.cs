using System;

namespace BackEnd.Dtos.Penalty
{
    public class PenaltyRecordDto
    {
        public string Id { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string Time { get; set; } = null!;
        public string? MerchantAction { get; set; }
        public string? PlatformAction { get; set; }
    }
}