using BackEnd.Dtos.MerchantInfo;

namespace BackEnd.Services.Interfaces
{
    public interface IMerchantInformationService
    {
        Task<(bool Success, string? Message, MerchantProfileDto? Data)> GetMerchantInfoAsync(int merchantUserId);
        Task<(bool Success, string? Message, MerchantUpdateResultDto? Data)> UpdateMerchantInfoAsync(int merchantUserId, UpdateMerchantProfileDto dto);
    }
}