using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IAfterSaleApplicationRepository
    {
        Task<IEnumerable<AfterSaleApplication>> GetAllAsync();
        Task<AfterSaleApplication?> GetByIdAsync(int id);
        Task<IEnumerable<AfterSaleApplication>> GetByOrderIdAsync(int orderId);
        Task AddAsync(AfterSaleApplication application);
        Task UpdateAsync(AfterSaleApplication application);
        Task DeleteAsync(AfterSaleApplication application);
        Task SaveAsync();
    }
}