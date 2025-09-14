using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        // Task<IEnumerable<Dish>> GetBySellerIdAsync(int sellerId);
        Task<Dish?> GetByIdAsync(int id);
        Task AddAsync(Dish dish);
        Task UpdateAsync(Dish dish);
        Task DeleteAsync(Dish dish);
        Task SaveAsync();
    }
}