using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IDeliveryTaskRepository
    {
        Task<IEnumerable<DeliveryTask>> GetAllAsync();
        Task<DeliveryTask?> GetByIdAsync(int id);
        Task AddAsync(DeliveryTask task);
        Task UpdateAsync(DeliveryTask task);
        Task DeleteAsync(DeliveryTask task);
        Task SaveAsync();
    }
}