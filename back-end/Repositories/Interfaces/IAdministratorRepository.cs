using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<IEnumerable<Administrator>> GetAllAsync();
        Task<Administrator?> GetByIdAsync(int id);
        // 更新管理员信息
        Task<bool> UpdateAdministratorAsync(Administrator administrator);
        // 根据管理员ID，获取所有相关的售后申请实体
        Task<IEnumerable<AfterSaleApplication>> GetAfterSaleApplicationsByAdminIdAsync(int adminId);
        // 根据售后申请ID，获取售后申请
        Task<AfterSaleApplication?> GetAfterSaleApplicationByIdAsync(int applicationId);
        // 更新售后申请
        Task<bool> UpdateAfterSaleApplicationAsync(AfterSaleApplication application);
        // 根据管理员ID，获取所有相关的配送投诉实体
        Task<IEnumerable<DeliveryComplaint>> GetDeliveryComplaintsByAdminIdAsync(int adminId);
        // 根据配送投诉ID，获取配送投诉
        Task<DeliveryComplaint?> GetDeliveryComplaintByIdAsync(int complaintId);
        // 更新配送投诉
        Task<bool> UpdateDeliveryComplaintAsync(DeliveryComplaint complaint);
        // 根据管理员ID，获取所有相关的违规处罚实体
        Task<IEnumerable<StoreViolationPenalty>> GetViolationPenaltiesByAdminIdAsync(int adminId);
        // 根据违规处罚ID，获取违规处罚
        Task<StoreViolationPenalty?> GetViolationPenaltyByIdAsync(int penaltyId);
        // 更新违规处罚
        Task<bool> UpdateViolationPenaltyAsync(StoreViolationPenalty penalty);
        // 根据管理员ID，获取所有相关的评论实体
        Task<IEnumerable<Comment>> GetReviewCommentsByAdminIdAsync(int adminId);
        // 根据评论ID，获取评论
        Task<Comment?> GetReviewCommentByIdAsync(int commentId);
        // 更新评论
        Task<bool> UpdateReviewCommentAsync(Comment comment);
        Task AddAsync(Administrator administrator);
        Task UpdateAsync(Administrator administrator);
        Task DeleteAsync(Administrator administrator);
        Task SaveAsync();
    }
}