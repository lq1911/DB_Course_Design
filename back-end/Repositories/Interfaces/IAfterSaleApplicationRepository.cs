using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IAfterSaleApplicationRepository
    {
        Task<IEnumerable<AfterSaleApplication>> GetAllAsync();
        Task<AfterSaleApplication?> GetByIdAsync(int id);
        Task AddAsync(AfterSaleApplication application);
        Task UpdateAsync(AfterSaleApplication application);
        Task DeleteAsync(AfterSaleApplication application);
        Task SaveAsync();
    }
}