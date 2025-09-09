using BackEnd.Dtos.DeliveryComplaint;

namespace BackEnd.Services.Interfaces
{
    public interface ICreateComplaintService
    {
        Task<CreateComplaintResult> CreateComplaintAsync(CreateComplaintDto request, int userId);
    }
}
