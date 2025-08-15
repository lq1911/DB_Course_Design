namespace BackEnd.DTOs.Courier
{
    public class CourierProfileDto
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string RegisterDate { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int CreditScore { get; set; }
    }
}