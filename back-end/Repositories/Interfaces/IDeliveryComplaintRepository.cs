using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IDeliveryComplaintRepository
    {
        Task<IEnumerable<DeliveryComplaint>> GetAllAsync();
        Task<DeliveryComplaint?> GetByIdAsync(int id);
        Task AddAsync(DeliveryComplaint complaint);
        Task UpdateAsync(DeliveryComplaint complaint);
        Task DeleteAsync(DeliveryComplaint complaint);
        Task SaveAsync();
    }
}