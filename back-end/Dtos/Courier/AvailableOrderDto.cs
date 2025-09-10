namespace BackEnd.DTOs.Courier
{
    public class AvailableOrderDto
    {
        public string Id { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Restaurant { get; set; } = null!;
        public string PickupAddress { get; set; } = null!;
        public string DeliveryAddress { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public string Fee { get; set; } = null!;
        public string Distance { get; set; } = null!;
        public string Time { get; set; } = null!;
    }
}