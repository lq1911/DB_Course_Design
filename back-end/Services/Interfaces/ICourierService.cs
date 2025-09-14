using BackEnd.Dtos.Courier; // 假设所有 DTO 都放在这里

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
        Task<bool> PickupOrderAsync(int orderId, int courierId);
        Task<bool> DeliverOrderAsync(int orderId, int courierId);
        // 在 ICourierService.cs 接口定义中添加此行
        Task<IEnumerable<ComplaintDto>> GetComplaintsAsync(int courierId);

        // 在 ICourierService.cs 接口中添加此行
        Task<bool> UpdateCourierLocationAsync(int courierId, decimal latitude, decimal longitude);

        // 在 ICourierService.cs 接口中添加或替换为这个版本
        Task<IEnumerable<AvailableOrderDto>> GetAvailableOrdersAsync(int courierId, decimal latitude, decimal longitude, decimal maxDistance);

        Task<bool> UpdateProfileAsync(int courierId, UpdateProfileDto profileDto);

        Task<UpdateProfileDto?> GetProfileForEditAsync(int courierId);

    }
}