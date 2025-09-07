using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Dtos.Penalty;

namespace BackEnd.Services.Interfaces
{
    public interface IPenaltyService
    {
        Task<List<PenaltyRecordDto>> GetPenaltiesAsync(string? keyword);
        Task<PenaltyRecordDto?> GetPenaltyByIdAsync(string id);
        Task<AppealResponseDto?> AppealPenaltyAsync(string id, AppealDto appealDto);
    }
}