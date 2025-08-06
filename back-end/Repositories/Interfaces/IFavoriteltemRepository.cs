using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IFavoriteItemRepository
    {
        Task<IEnumerable<FavoriteItem>> GetAllAsync();
        Task<FavoriteItem?> GetByIdAsync(int id);
        Task AddAsync(FavoriteItem item);
        Task UpdateAsync(FavoriteItem item);
        Task DeleteAsync(FavoriteItem item);
        Task SaveAsync();
    }
}