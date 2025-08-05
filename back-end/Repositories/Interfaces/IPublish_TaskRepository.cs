using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IPublish_TaskRepository
    {
        Task<IEnumerable<Publish_Task>> GetAllAsync();
        Task<Publish_Task?> GetByIdAsync(int id);
        Task AddAsync(Publish_Task publish_task);
        Task UpdateAsync(Publish_Task publish_task);
        Task DeleteAsync(Publish_Task publish_task);
        Task SaveAsync();
    }
}