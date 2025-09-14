namespace BackEnd.Dtos.Dish
{
    public class DishDto
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int IsSoldOut { get; set; }
    }

    public class CreateDishDto
    {
        public string DishName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int IsSoldOut { get; set; } = 0;
    }

    public class UpdateDishDto
    {
        public string? DishName { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? IsSoldOut { get; set; }
    }

    public class ToggleSoldOutDto
    {
        public int IsSoldOut { get; set; }
    }
}