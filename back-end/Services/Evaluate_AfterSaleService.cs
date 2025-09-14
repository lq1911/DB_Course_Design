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
        public async Task<IEnumerable<GetAfterSaleApplicationInfo>> GetApplicationsForAdminAsync(int adminId)
        {
            // 1. 调用仓储层，从数据库获取原始的、未经处理的数据模型
            var applicationsFromDb = await _administratorRepository.GetAfterSaleApplicationsByAdminIdAsync(adminId);

            // 如果找不到任何申请，返回一个空的列表，而不是 null
            if (applicationsFromDb == null || !applicationsFromDb.Any())
            {
                return Enumerable.Empty<GetAfterSaleApplicationInfo>();
            }

            // 2. 将数据模型映射/转换为 DTO 
            var applicationDtos = applicationsFromDb.Select(app => new GetAfterSaleApplicationInfo
            {
                ApplicationId = app.ApplicationID.ToString(),
                OrderId = app.OrderID.ToString(),
                ApplicationTime = app.ApplicationTime.ToString("yyyy-MM-dd HH:mm"), // 格式化日期时间字符串
                Description = app.Description,
                Status = app.AfterSaleState == AfterSaleState.Pending ? "待处理" : "已完成",
                Punishment = app.ProcessingResult ?? "-",
                PunishmentReason = app.ProcessingReason ?? "",
                ProcessingNote = app.ProcessingRemark ?? "-"
            });

            return applicationDtos;
        }

        // 根据售后申请ID，设置售后申请
        public async Task<SetAfterSaleApplicationResponse> UpdateAfterSaleApplicationAsync(SetAfterSaleApplicationInfo request)
        {
            try
            {
                // 1. 验证输入参数
                if (!int.TryParse(request.ApplicationId, out int applicationId))
                {
                    return new SetAfterSaleApplicationResponse
                    {
                        Success = false,
                        Message = "无效的申请编号格式"
                    };
                }

                if (request.Status != "已完成")
                {
                    return new SetAfterSaleApplicationResponse
                    {
                        Success = false,
                        Message = "状态只能更新为'已完成'"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Punishment) ||
                string.IsNullOrWhiteSpace(request.PunishmentReason))
                {
                    return new SetAfterSaleApplicationResponse
                    {
                        Success = false,
                        Message = "处罚措施和处罚原因都是必填项"
                    };
                }

                // 2. 获取现有的售后申请
                var existingApplication = await _administratorRepository.GetAfterSaleApplicationByIdAsync(applicationId);
                if (existingApplication == null)
                {
                    return new SetAfterSaleApplicationResponse
                    {
                        Success = false,
                        Message = "未找到指定的售后申请"
                    };
                }

                // 3. 更新售后申请信息
                existingApplication.AfterSaleState = AfterSaleState.Completed;
                existingApplication.ProcessingResult = request.Punishment;
                existingApplication.ProcessingRemark = request.ProcessingNote;
                existingApplication.ProcessingReason = request.PunishmentReason;

                // 4. 保存更改
                bool updateSuccess = await _administratorRepository.UpdateAfterSaleApplicationAsync(existingApplication);
                if (!updateSuccess)
                {
                    return new SetAfterSaleApplicationResponse
                    {
                        Success = false,
                        Message = "更新售后申请失败，请稍后重试"
                    };
                }

                // 6. 返回更新后的完整信息
                var updatedApplicationDto = new GetAfterSaleApplicationInfo
                {
                    ApplicationId = existingApplication.ApplicationID.ToString(),
                    OrderId = existingApplication.OrderID.ToString(),
                    ApplicationTime = existingApplication.ApplicationTime.ToString("yyyy-MM-dd HH:mm"),
                    Description = existingApplication.Description,
                    Status = "已完成",
                    Punishment = existingApplication.ProcessingResult ?? "-"
                };

                return new SetAfterSaleApplicationResponse
                {
                    Success = true,
                    Message = "售后申请处理成功",
                    Data = updatedApplicationDto
                };
            }
            catch (Exception ex)
            {
                return new SetAfterSaleApplicationResponse
                {
                    Success = false,
                    Message = $"处理售后申请时发生错误：{ex.Message}"
                };
            }
        }
    }
}
