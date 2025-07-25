using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store?> GetByIdAsync(int id);
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(Store store);
        Task SaveAsync();
    }
}