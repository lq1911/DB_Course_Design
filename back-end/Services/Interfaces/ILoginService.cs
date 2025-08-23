using BackEnd.Dtos.AuthRequest;

namespace BackEnd.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginRequest request);
    }
}
