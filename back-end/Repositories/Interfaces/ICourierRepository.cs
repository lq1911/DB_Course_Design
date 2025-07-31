using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface ICourierRepository
    {
        Task<IEnumerable<Courier>> GetAllAsync();
        Task<Courier?> GetByIdAsync(int id);
        Task AddAsync(Courier courier);
        Task UpdateAsync(Courier courier);
        Task DeleteAsync(Courier courier);
        Task SaveAsync();
    }
}