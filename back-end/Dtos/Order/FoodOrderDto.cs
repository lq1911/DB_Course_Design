namespace BackEnd.Dtos.Order
{
    public class FoodOrderDto
    {
        public int OrderId { get; set; }
        public string PaymentTime { get; set; } = null!;
        public string? Remarks { get; set; }
        public int CustomerId { get; set; }
        public int CartId { get; set; }
        public int StoreId { get; set; }
        public int SellerId { get; set; }
    }
}