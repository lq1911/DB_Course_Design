using BackEnd.Dtos.AfterSaleApplication;

namespace BackEnd.Services.Interfaces
{
    public interface ICreateApplicationService
    {
        Task<CreateApplicationResult> CreateApplicationAsync(CreateApplicationDto request, int userId);
    }
}
