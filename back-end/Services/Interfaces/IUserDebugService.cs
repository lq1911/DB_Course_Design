using BackEnd.Dtos.User;

namespace BackEnd.Services.Interfaces
{
    public interface IUserDebugService
    {
        Task<UserInfoResponseDto> GetUserInfoAsync(int userId);

        Task SubmitOrderAsync(SubmitOrderRequestDto dto);

        Task<GetUserIdResponseDto> GetUserIdAsync(GetUserIdRequestDto dto);
    }
}