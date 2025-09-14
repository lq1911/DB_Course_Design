using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IStoreViolationPenaltyRepository
    {
        Task<IEnumerable<StoreViolationPenalty>> GetAllAsync();
        Task<StoreViolationPenalty?> GetByIdAsync(int id);
        Task<IEnumerable<StoreViolationPenalty>> GetBySellerIdAsync(int sellerId);
        Task AddAsync(StoreViolationPenalty storeviolationpenalty);
        Task UpdateAsync(StoreViolationPenalty storeviolationpenalty);
        Task DeleteAsync(StoreViolationPenalty storeviolationpenalty);
        Task SaveAsync();
    }
}