using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IEvaluate_AfterSaleRepository
    {
        Task<IEnumerable<Evaluate_AfterSale>> GetAllAsync();
        Task<Evaluate_AfterSale?> GetByIdAsync(int adminId, int applicationId);
        Task AddAsync(Evaluate_AfterSale evaluateAfterSale);
        Task UpdateAsync(Evaluate_AfterSale evaluateAfterSale);
        Task DeleteAsync(Evaluate_AfterSale evaluateAfterSale);
        Task SaveAsync();
    }
}