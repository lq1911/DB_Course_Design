using BackEnd.Dtos.Delivery;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class DeliveryTaskService : IDeliveryTaskService
    {
        private readonly IDeliveryTaskRepository _deliveryRepo;
        private readonly IFoodOrderRepository _orderRepo;
        private readonly IStoreRepository _storeRepo;

        public DeliveryTaskService(IDeliveryTaskRepository deliveryRepo,
                                  IFoodOrderRepository orderRepo,
                                  IStoreRepository storeRepo)
        {
            _deliveryRepo = deliveryRepo;
            _orderRepo = orderRepo;
            _storeRepo = storeRepo;
        }

        public async Task<(DeliveryTaskDto? DeliveryTask, PublishTaskDto? Publish)> PublishDeliveryTaskAsync(
            PublishDeliveryTaskDto dto, int sellerId)
        {
            // 验证订单存在且属于当前商家
            var order = await _orderRepo.GetByIdAsync(dto.OrderId)
                ?? throw new KeyNotFoundException("订单不存在");

            var store = await _storeRepo.GetStoreInfoForUserAsync(order.StoreID);
            if (store?.SellerID != sellerId)
                throw new UnauthorizedAccessException("无权操作此订单");

            // 创建配送任务
            var task = new DeliveryTask
            {
                OrderID = dto.OrderId,
                EstimatedArrivalTime = DateTime.Parse(dto.EstimatedArrivalTime),
                EstimatedDeliveryTime = DateTime.Parse(dto.EstimatedDeliveryTime),
                CustomerID = order.CustomerID,
                StoreID = order.StoreID,
                Status = DeliveryStatus.Pending,
                DeliveryFee = order.DeliveryFee

            };

            await _deliveryRepo.AddAsync(task);

            // 创建发布记录
            var publish = new PublishTaskDto
            {
                SellerId = sellerId,
                DeliveryTaskId = task.TaskID,
                PublishTime = DateTime.Now.ToString("o")
            };

            return (new DeliveryTaskDto
            {
                TaskId = task.TaskID,
                EstimatedArrivalTime = task.EstimatedArrivalTime.ToString("o"),
                EstimatedDeliveryTime = task.EstimatedDeliveryTime.ToString("o"),
                CustomerId = task.CustomerID,
                StoreId = task.StoreID
            }, publish);
        }

        public async Task<OrderDeliveryInfoDto> GetOrderDeliveryInfoAsync(int orderId)
        {
            var task = await _deliveryRepo.GetByOrderIdAsync(orderId);
            if (task == null)
                return new OrderDeliveryInfoDto();

            var courier = task.Courier;
            var courierUser = courier?.User;

            return new OrderDeliveryInfoDto
            {
                DeliveryTask = new DeliveryTaskDto
                {
                    TaskId = task.TaskID,
                    EstimatedArrivalTime = task.EstimatedArrivalTime.ToString("o"),
                    EstimatedDeliveryTime = task.EstimatedDeliveryTime.ToString("o"),
                    CustomerId = task.CustomerID,
                    StoreId = task.StoreID
                },
                Publish = new PublishTaskDto
                {
                    SellerId = task.Store.SellerID,
                    DeliveryTaskId = task.TaskID,
                    PublishTime = task.PublishTime.ToString("o")
                },
                Accept = new AcceptTaskDto
                {
                    CourierId = task.CourierID,
                    DeliveryTaskId = task.TaskID,
                    AcceptTime = task.AcceptTime.ToString("o")
                },
                Courier = courier == null ? null : new CourierSummaryDto
                {
                    UserId = courier.UserID,
                    CourierRegistrationTime = courier.CourierRegistrationTime.ToString("o"),
                    VehicleType = courier.VehicleType,
                    ReputationPoints = courier.ReputationPoints,
                    TotalDeliveries = courier.TotalDeliveries,
                    AvgDeliveryTime = courier.AvgDeliveryTime,
                    AverageRating = courier.AverageRating,
                    MonthlySalary = courier.MonthlySalary,
                    FullName = courierUser?.FullName,
                    PhoneNumber = courierUser?.PhoneNumber
                }
            };
        }
    }
}