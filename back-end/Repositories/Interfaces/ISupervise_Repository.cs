using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface ISupervise_Repository
    {
        Task<IEnumerable<Supervise_>> GetAllAsync();
        Task<Supervise_?> GetByIdAsync(int adminId, int penaltyId);
        Task AddAsync(Supervise_ supervise_);
        Task UpdateAsync(Supervise_ supervise_);
        Task DeleteAsync(Supervise_ supervise_);
        Task SaveAsync();
    }
}