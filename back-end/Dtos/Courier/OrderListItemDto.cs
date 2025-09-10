// BackEnd/DTOs/Courier/OrderListItemDto.cs
namespace BackEnd.Dtos.Courier
{
    public class OrderListItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Restaurant { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Fee { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
    }
}