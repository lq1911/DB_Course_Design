using BackEnd.Dtos.AuthRequest;

namespace BackEnd.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginRequest request);
        Task LogoutAsync(int userId); // 参数可以是用户ID，用于记录日志等
    }
}
