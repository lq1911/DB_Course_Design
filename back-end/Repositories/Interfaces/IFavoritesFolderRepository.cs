using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IFavoritesFolderRepository
    {
        Task<IEnumerable<FavoritesFolder>> GetAllAsync();
        Task<FavoritesFolder?> GetByIdAsync(int id);
        Task AddAsync(FavoritesFolder favoritesfolder);
        Task UpdateAsync(FavoritesFolder favoritesfolder);
        Task DeleteAsync(FavoritesFolder favoritesfolder);
        Task SaveAsync();
    }
}