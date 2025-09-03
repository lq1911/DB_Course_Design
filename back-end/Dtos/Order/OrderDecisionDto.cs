namespace BackEnd.Dtos.Order
{
    public class OrderDecisionDto
    {
        public int OrderId { get; set; }
        public string Decision { get; set; } = null!; // "accepted" 或 "rejected"
        public string DecidedAt { get; set; } = null!;
        public string? Reason { get; set; }
    }
}