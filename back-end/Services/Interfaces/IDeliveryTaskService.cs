using BackEnd.Dtos.Delivery;

namespace BackEnd.Services.Interfaces
{
    public interface IDeliveryTaskService
    {
        Task<(DeliveryTaskDto? DeliveryTask, PublishTaskDto? Publish)> PublishDeliveryTaskAsync(
            PublishDeliveryTaskDto dto, int sellerId);
        
        Task<OrderDeliveryInfoDto> GetOrderDeliveryInfoAsync(int orderId);
    }
}