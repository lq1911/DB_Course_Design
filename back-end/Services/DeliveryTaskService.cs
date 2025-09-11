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
        private readonly ICourierRepository _courierRepo;

        public DeliveryTaskService(IDeliveryTaskRepository deliveryRepo,
                                  IFoodOrderRepository orderRepo,
                                  IStoreRepository storeRepo,
                                  ICourierRepository courierRepo)
        {
            _deliveryRepo = deliveryRepo;
            _orderRepo = orderRepo;
            _storeRepo = storeRepo;
            _courierRepo = courierRepo;
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
                Status = DeliveryStatus.To_Be_Taken,
                DeliveryFee = order.DeliveryFee
            };

            Console.WriteLine("Status before saving: " + task.Status);
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
            // 开始调试输出：记录请求的订单ID
            Console.WriteLine($"[DEBUG] 获取订单配送信息，订单ID: {orderId}");

            var task = await _deliveryRepo.GetByOrderIdAsync(orderId);
            if (task == null)
            {
                // 记录找不到任务的情况
                Console.WriteLine($"[DEBUG] 未找到订单的配送任务，订单ID: {orderId}");
                return new OrderDeliveryInfoDto();
            }

            // 输出任务相关信息
            Console.WriteLine($"[DEBUG] 找到配送任务，任务ID: {task.TaskID}, 客户ID: {task.CustomerID}, 商店ID: {task.StoreID}");

            var courier = task.CourierID.HasValue
                              ? await _courierRepo.GetByIdAsync(task.CourierID.Value)
                              : null;

            // 构建返回数据
            var result = new OrderDeliveryInfoDto
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
                Accept = task.CourierID.HasValue
                    ? new AcceptTaskDto
                    {
                        CourierId = task.CourierID,
                        DeliveryTaskId = task.TaskID,
                        AcceptTime = task.AcceptTime.ToString("o")
                    }
                    : null,
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
                    FullName = courier.User?.FullName,
                    PhoneNumber = courier.User?.PhoneNumber
                }
            };

            // 输出返回的配送任务信息
            Console.WriteLine("[DEBUG] 配送任务信息构建完成");
            Console.WriteLine($"[DEBUG] 配送任务ID: {result.DeliveryTask?.TaskId}, 发布任务ID: {result.Publish?.DeliveryTaskId}");

            // 返回结果
            return result;
        }

    }
}