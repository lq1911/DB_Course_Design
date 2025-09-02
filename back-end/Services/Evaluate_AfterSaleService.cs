using BackEnd.Dtos.AfterSaleApplication;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class Evaluate_AfterSaleService : IEvaluate_AfterSaleService
    {
        private readonly IAdministratorRepository _administratorRepository;

        public Evaluate_AfterSaleService(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        // 根据管理员ID，获取与其相关的所有售后申请。
        public async Task<IEnumerable<GetAfterSaleApplication>> GetApplicationsForAdminAsync(int adminId)
        {
            // 1. 调用仓储层，从数据库获取原始的、未经处理的数据模型
            var applicationsFromDb = await _administratorRepository.GetAfterSaleApplicationsByAdminIdAsync(adminId);

            // 如果找不到任何申请，返回一个空的列表，而不是 null
            if (applicationsFromDb == null || !applicationsFromDb.Any())
            {
                return Enumerable.Empty<GetAfterSaleApplication>();
            }

            // 2. 将数据模型映射/转换为 DTO 
            var applicationDtos = applicationsFromDb.Select(app => new GetAfterSaleApplication
            {
                ApplicationId = app.ApplicationID.ToString(),
                OrderId = app.OrderID.ToString(),
                ApplicationTime = app.ApplicationTime.ToString("yyyy-MM-dd HH:mm"), // 格式化日期时间字符串
                Description = app.Description,
                Status = app.AfterSaleState == AfterSaleState.Pending ? "待处理" : "已完成",
                Punishment = app.ProcessingResult ?? "-"
            });

            return applicationDtos;
        }
    }
}
