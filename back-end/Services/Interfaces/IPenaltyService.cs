using BackEnd.Dtos.Penalty;

namespace BackEnd.Services.Interfaces
{
    public interface IPenaltyService
    {
        Task<List<PenaltyRecordDto>> GetPenaltiesAsync(int sellerId, string? keyword);
        Task<PenaltyRecordDto?> GetPenaltyByIdAsync(string id);
        Task<AppealResponseDto?> AppealPenaltyAsync(string id, AppealDto appealDto);
    }
}