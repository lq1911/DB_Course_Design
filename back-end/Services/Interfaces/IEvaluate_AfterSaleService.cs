using BackEnd.Dtos.AfterSaleApplication;

namespace BackEnd.Services.Interfaces
{
    public interface IEvaluate_AfterSaleService
    {
        // 根据管理员ID，获取售后申请的 DTO 列表
        Task<IEnumerable<GetAfterSaleApplication>> GetApplicationsForAdminAsync(int adminId);
    }
}
