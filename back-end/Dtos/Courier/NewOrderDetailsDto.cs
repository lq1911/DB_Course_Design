namespace BackEnd.Dtos.Courier
{
    public class NewOrderDetailsDto
    {
        public string Id { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
        public string RestaurantAddress { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string Distance { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public string MapImageUrl { get; set; } = string.Empty;
    }
}