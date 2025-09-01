using BackEnd.Dtos.AuthRequest;

namespace BackEnd.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterResult> RegisterAsync(RegisterRequest request);
    }
}
