using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IFoodOrderRepository
    {
        Task<IEnumerable<FoodOrder>> GetAllAsync(int userId);
        Task<IEnumerable<FoodOrder>> GetAllAsync();
        Task<FoodOrder?> GetByIdAsync(int id);
        Task AddAsync(FoodOrder foodorder);
        Task UpdateAsync(FoodOrder foodorder);
        Task DeleteAsync(FoodOrder foodorder);
        Task SaveAsync();
    }
}