namespace BackEnd.Dtos.Cart
{
    public class ShoppingCartItemDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int DishId { get; set; }
        public int CartId { get; set; }
        public CartItemDishRefDto? Dish { get; set; }
    }

    public class CartItemDishRefDto
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int IsSoldOut { get; set; }
    }
}