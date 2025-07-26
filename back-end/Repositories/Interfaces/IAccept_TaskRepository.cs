using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IAccept_TaskRepository
    {
        Task<IEnumerable<Accept_Task>> GetAllAsync();
        Task<Accept_Task?> GetByIdAsync(int courierId, int deliveryTaskId);
        Task AddAsync(Accept_Task acceptTask);
        Task UpdateAsync(Accept_Task acceptTask);
        Task DeleteAsync(Accept_Task acceptTask);
        Task SaveAsync();
    }
}