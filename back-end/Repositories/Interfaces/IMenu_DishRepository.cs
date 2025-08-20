using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IMenu_DishRepository
    {
        Task<IEnumerable<Menu_Dish>> GetAllAsync();
        Task<Menu_Dish?> GetByIdAsync(int menuId, int dishId);
        Task AddAsync(Menu_Dish menu_dish);
        Task UpdateAsync(Menu_Dish menu_dish);
        Task DeleteAsync(Menu_Dish menu_dish);
        Task SaveAsync();
    }
}