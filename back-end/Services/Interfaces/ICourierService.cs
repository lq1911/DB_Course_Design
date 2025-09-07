using BackEnd.DTOs.Courier; // 假设所有 DTO 都放在这里

namespace BackEnd.Services.Interfaces
{
    public interface ICourierService
    {
        Task<CourierProfileDto?> GetProfileAsync(int courierId);
        Task<WorkStatusDto?> GetWorkStatusAsync(int courierId);

        Task<IEnumerable<OrderListItemDto>> GetOrdersAsync(int courierId, string status);

       Task<string> GetCurrentLocationAsync(int courierId);
        Task<bool> ToggleWorkStatusAsync(int courierId, bool isOnline);
        Task<NewOrderDetailsDto?> GetNewOrderDetailsAsync(int notificationId);
        Task<bool> AcceptOrderAsync(int courierId, int orderId);
        Task<bool> RejectOrderAsync(int orderId);
        Task<decimal> GetMonthlyIncomeAsync(int courierId);
        Task MarkTaskAsCompletedAsync(int taskId, int courierId); // 增加 courierId 用于权限验证
        
    }
}