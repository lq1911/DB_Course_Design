using BackEnd.Dtos.DeliveryComplaint;

namespace BackEnd.Services.Interfaces
{
    public interface IEvaluate_DeliveryComplaintService
    {
        // 根据管理员ID获取相关的配送投诉列表
        Task<IEnumerable<GetComplaintInfo>> GetComplaintsForAdminAsync(int adminId);

        // 更新配送投诉处理结果
        Task<SetComplaintInfoResponse> UpdateComplaintAsync(SetComplaintInfo request);
    }
}
