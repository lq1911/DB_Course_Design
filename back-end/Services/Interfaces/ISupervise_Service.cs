using BackEnd.Dtos.ViolationPenalty;

namespace BackEnd.Services.Interfaces
{
    public interface ISupervise_Service
    {
        Task<IEnumerable<GetViolationPenaltyInfo>> GetViolationPenaltiesForAdminAsync(int adminId);
        Task<SetViolationPenaltyInfoResponse> UpdateViolationPenaltyAsync(SetViolationPenaltyInfo request);
    }
}
