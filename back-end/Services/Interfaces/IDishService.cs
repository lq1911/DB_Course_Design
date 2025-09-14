using BackEnd.Dtos.Dish;

namespace BackEnd.Services.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetAllDishesAsync();
        Task<IEnumerable<DishDto>> GetDishesBySellerIdAsync(int sellerId);
        Task<DishDto> CreateDishAsync(CreateDishDto dto);
        Task<DishDto?> UpdateDishAsync(int dishId, UpdateDishDto dto);
        Task<(bool Success, string? Message, DishDto? Data)> ToggleSoldOutAsync(int dishId, int isSoldOut, int sellerId);
        Task<DishDto?> GetDishByIdAsync(int dishId);
    }
}