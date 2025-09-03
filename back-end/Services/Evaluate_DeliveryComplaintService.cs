using BackEnd.Dtos.DeliveryComplaint;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class Evaluate_DeliveryComplaintService : IEvaluate_DeliveryComplaintService
    {
        private readonly IAdministratorRepository _administratorRepository;

        public Evaluate_DeliveryComplaintService(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        public async Task<IEnumerable<GetComplaintInfo>> GetComplaintsForAdminAsync(int adminId)
        {
            // 1. 调用仓储层，从数据库获取原始的、未经处理的数据模型
            var complaintsFromDb = await _administratorRepository.GetDeliveryComplaintsByAdminIdAsync(adminId);

            // 如果找不到任何投诉，返回一个空的列表，而不是 null
            if (complaintsFromDb == null || !complaintsFromDb.Any())
            {
                return Enumerable.Empty<GetComplaintInfo>();
            }

            // 2. 将数据模型映射/转换为 DTO
            var complaintDtos = complaintsFromDb.Select(complaint => new GetComplaintInfo
            {
                ComplaintId = complaint.ComplaintID.ToString(),
                DeliveryTaskId = complaint.DeliveryTaskID.ToString(),
                ApplicationTime = complaint.ComplaintTime.ToString("yyyy-MM-dd HH:mm"),
                Content = complaint.ComplaintReason,
                Status = complaint.ComplaintState == ComplaintState.Pending ? "待处理" : "已完成",
                Punishment = complaint.ProcessingResult ?? "-",
                PunishmentReason = complaint.ProcessingReason ?? "",
                ProcessingNote = complaint.ProcessingRemark ?? ""
            });

            return complaintDtos;
        }

        public async Task<SetComplaintInfoResponse> UpdateComplaintAsync(SetComplaintInfo request)
        {
            try
            {
                // 1. 验证输入参数
                if (!int.TryParse(request.ComplaintId, out int complaintId))
                {
                    return new SetComplaintInfoResponse
                    {
                        Success = false,
                        Message = "无效的投诉编号格式"
                    };
                }

                if (request.Status != "已完成")
                {
                    return new SetComplaintInfoResponse
                    {
                        Success = false,
                        Message = "状态只能更新为'已完成'"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Punishment) ||
                    string.IsNullOrWhiteSpace(request.PunishmentReason))
                {
                    return new SetComplaintInfoResponse
                    {
                        Success = false,
                        Message = "处罚措施和处罚原因都是必填项"
                    };
                }

                // 2. 获取现有的配送投诉
                var existingComplaint = await _administratorRepository.GetDeliveryComplaintByIdAsync(complaintId);
                if (existingComplaint == null)
                {
                    return new SetComplaintInfoResponse
                    {
                        Success = false,
                        Message = "未找到指定的配送投诉"
                    };
                }

                // 检查投诉是否已经处理
                if (existingComplaint.ComplaintState == ComplaintState.Completed)
                {
                    return new SetComplaintInfoResponse
                    {
                        Success = false,
                        Message = "该投诉已经处理完成，无法重复处理"
                    };
                }

                // 3. 更新配送投诉信息
                existingComplaint.ComplaintState = ComplaintState.Completed;
                existingComplaint.ProcessingResult = request.Punishment;
                existingComplaint.ProcessingReason = request.PunishmentReason;
                existingComplaint.ProcessingRemark = request.ProcessingNote;

                // 4. 保存更改
                bool updateSuccess = await _administratorRepository.UpdateDeliveryComplaintAsync(existingComplaint);
                if (!updateSuccess)
                {
                    return new SetComplaintInfoResponse
                    {
                        Success = false,
                        Message = "更新配送投诉失败，请稍后重试"
                    };
                }

                // 5. 返回更新后的完整信息
                var updatedComplaintDto = new GetComplaintInfo
                {
                    ComplaintId = existingComplaint.ComplaintID.ToString(),
                    DeliveryTaskId = existingComplaint.DeliveryTaskID.ToString(),
                    ApplicationTime = existingComplaint.ComplaintTime.ToString("yyyy-MM-dd HH:mm"),
                    Content = existingComplaint.ComplaintReason,
                    Status = "已完成",
                    Punishment = existingComplaint.ProcessingResult ?? "-",
                    PunishmentReason = existingComplaint.ProcessingReason ?? "",
                    ProcessingNote = existingComplaint.ProcessingRemark ?? ""
                };

                return new SetComplaintInfoResponse
                {
                    Success = true,
                    Message = "配送投诉处理成功",
                    Data = updatedComplaintDto
                };
            }
            catch (Exception ex)
            {
                return new SetComplaintInfoResponse
                {
                    Success = false,
                    Message = $"处理配送投诉时发生错误：{ex.Message}"
                };
            }
        }
    }
}
