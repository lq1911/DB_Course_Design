using BackEnd.Dtos.Administrator;

namespace BackEnd.Services.Interfaces
{
    public interface IAdministratorService
    {
        Task<GetAdminInfo?> GetAdministratorInfoAsync(int adminId);
        Task<SetAdminInfoResponse> UpdateAdministratorInfoAsync(int adminId, SetAdminInfo request);
    }
}
