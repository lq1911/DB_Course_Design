using BackEnd.DTOs.Courier; // 假设所有 DTO 都放在这里

namespace BackEnd.Services.Interfaces
{
    public interface ICourierService
    {
        Task<CourierProfileDto?> GetProfileAsync(int courierId);
        Task<WorkStatusDto?> GetWorkStatusAsync(int courierId);

        Task<IEnumerable<OrderListItemDto>> GetOrdersAsync(int courierId, string status);
    
    }
}