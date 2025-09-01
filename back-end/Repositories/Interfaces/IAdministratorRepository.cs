using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<IEnumerable<Administrator>> GetAllAsync();
        Task<Administrator?> GetByIdAsync(int id);
        Task AddAsync(Administrator administrator);
        Task UpdateAsync(Administrator administrator);
        Task DeleteAsync(Administrator administrator);
        Task SaveAsync();
    }
}