using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<IEnumerable<Administrator>> GetAllAsync();
        Task<Administrator?> GetByIdAsync(int id);
        // 根据管理员ID，获取所有相关的售后申请实体
        Task<IEnumerable<AfterSaleApplication>> GetAfterSaleApplicationsByAdminIdAsync(int adminId);
        Task AddAsync(Administrator administrator);
        Task UpdateAsync(Administrator administrator);
        Task DeleteAsync(Administrator administrator);
        Task SaveAsync();
    }
}