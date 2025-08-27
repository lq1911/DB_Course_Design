using BackEnd.DTOs.Courier;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore; // 引入它以使用 Include 和 ThenInclude

namespace BackEnd.Services
{
    public class CourierService : ICourierService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly IDeliveryTaskRepository _deliveryTaskRepository;

        public CourierService(
            IUserRepository userRepository,
            ICourierRepository courierRepository,
            IDeliveryTaskRepository deliveryTaskRepository)
        {
            _userRepository = userRepository;
            _courierRepository = courierRepository;
            _deliveryTaskRepository = deliveryTaskRepository;
        }

        public async Task<CourierProfileDto?> GetProfileAsync(int courierId)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null) return null;
            var user = await _userRepository.GetByIdAsync(courierId);
            if (user == null) return null;
            var profileDto = new CourierProfileDto
            {
                Id = courier.UserID.ToString(),
                Name = !string.IsNullOrEmpty(user.FullName) ? user.FullName : user.Username,
                RegisterDate = courier.CourierRegistrationTime.ToString("yyyy-MM-dd"),
                Rating = courier.AverageRating,
                CreditScore = courier.ReputationPoints
            };
            return profileDto;
        }

        public async Task<WorkStatusDto?> GetWorkStatusAsync(int courierId)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null) return null;
            var statusDto = new WorkStatusDto
            {
                IsOnline = courier.IsOnline == CourierIsOnline.Online ? true : false
            };
            return statusDto;
        }

        public async Task<string> GetCurrentLocationAsync(int courierId)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null || !courier.CourierLongitude.HasValue || !courier.CourierLatitude.HasValue)
            {
                return "位置信息未提供";
            }
            var simulatedArea = $"模拟位置 (经度: {courier.CourierLongitude.Value:F6}, 纬度: {courier.CourierLatitude.Value:F6})";
            return await Task.FromResult(simulatedArea);
        }

        public async Task<bool> ToggleWorkStatusAsync(int courierId, bool isOnline)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null) return false;
            courier.IsOnline = isOnline ? CourierIsOnline.Online : CourierIsOnline.Offline;
            await _courierRepository.UpdateAsync(courier);
            await _courierRepository.SaveAsync();
            return true;
        }

        // --- 这是 GetOrdersAsync 的修正后版本 ---
        public async Task<IEnumerable<OrderListItemDto>> GetOrdersAsync(int courierId, string status)
        {
            if (!Enum.TryParse<DeliveryStatus>(status, true, out var targetStatus))
            {
                return new List<OrderListItemDto>();
            }

            // 使用 GetQueryable() 进行高效的数据库端筛选和关联查询
            var tasksQuery = _deliveryTaskRepository.GetQueryable()
                .Where(t => t.CourierID == courierId && t.Status == targetStatus)
                .Include(t => t.Store)      // 加载商家信息
                .Include(t => t.Customer);  // 加载顾客信息

            var tasks = await tasksQuery
                .OrderByDescending(t => t.PublishTime)
                .ToListAsync();

            var orderDtos = tasks.Select(task => new OrderListItemDto
            {
                Id = task.TaskID.ToString(),
                Status = task.Status.ToString().ToLower(),
                Restaurant = task.Store?.StoreName ?? "未知商家",
                Address = task.Customer?.DefaultAddress ?? "未知地址", // <-- 修正：使用顾客的默认地址
                Fee = task.DeliveryFee.ToString("F2"),
                StatusText = GetStatusText(task.Status)
                // Time 字段已根据新需求移除
            }).ToList();

            return orderDtos;
        }

        // 私有辅助方法，保持不变
        private string GetStatusText(DeliveryStatus status)
        {
            switch (status)
            {
                case DeliveryStatus.Pending: return "待处理";
                case DeliveryStatus.Delivering: return "配送中";
                case DeliveryStatus.Completed: return "已完成";
                case DeliveryStatus.Cancelled: return "已取消";
                default: return "未知状态";
            }
        }

        // --- 这是 GetNewOrderDetailsAsync 的最终实现 ---
        public async Task<NewOrderDetailsDto?> GetNewOrderDetailsAsync(int notificationId)
        {
            // 1. notificationId 通常就是订单任务的ID，我们用它来查询
            var taskId = notificationId;

            // 2. 构建一个复杂的查询，一次性加载所有需要的数据
            var task = await _deliveryTaskRepository.GetQueryable()
                // 加载关联的商家信息
                .Include(t => t.Store)
                // 加载关联的顾客信息
                .Include(t => t.Customer)
                    // 再加载顾客关联的用户信息 (用于获取姓名)
                    .ThenInclude(c => c.User)
                // 根据ID筛选出唯一的任务
                .FirstOrDefaultAsync(t => t.TaskID == taskId);

            // 3. 如果找不到任务，返回 null
            if (task == null)
            {
                return null;
            }

            // 4. 将查询到的实体数据，映射到 DTO
            var orderDetailsDto = new NewOrderDetailsDto
            {
                Id = task.TaskID.ToString(),
                RestaurantName = task.Store?.StoreName ?? "未知商家",
                RestaurantAddress = task.Store?.StoreAddress ?? "未知商家地址",
                // 优先使用真实姓名，如果没有则用用户名
                CustomerName = task.Customer?.User?.FullName ?? task.Customer?.User?.Username ?? "未知顾客",
                CustomerAddress = task.Customer?.DefaultAddress ?? "未知顾客地址",
                Fee = task.DeliveryFee, // 直接返回 decimal 类型

                // --- 模拟数据部分 ---
                Distance = "约 2.5 公里", // 提供一个固定的模拟距离
                MapImageUrl = "https://example.com/static-map.png" // 提供一个固定的模拟地图图片URL
            };

            return orderDetailsDto;
        }


        public async Task<bool> AcceptOrderAsync(int courierId, int orderId)
        {
            // 1. 根据 orderId 从数据库中查找订单任务
            var task = await _deliveryTaskRepository.GetByIdAsync(orderId);

            // 2. 验证订单是否存在，以及状态是否为“待处理”
            //    只有“待处理”状态的订单才能被接受。
            if (task == null || task.Status != DeliveryStatus.Pending)
            {
                // 如果订单不存在，或者已经被其他骑手接受/处理，则操作失败
                return false;
            }

            // 3. 更新订单信息
            task.Status = DeliveryStatus.Delivering; // 将状态更新为“配送中”
            task.CourierID = courierId;              // 将当前骑手ID分配给这个任务
            task.AcceptTime = DateTime.UtcNow;       // 记录接单时间

            // 4. 调用仓储层持久化更改
            await _deliveryTaskRepository.UpdateAsync(task);
            await _deliveryTaskRepository.SaveAsync();

            // 5. 返回 true 表示操作成功
            return true;
        }



        public async Task<bool> RejectOrderAsync(int orderId)
        {
            // 1. 根据 orderId 从数据库中查找订单任务
            var task = await _deliveryTaskRepository.GetByIdAsync(orderId);

            // 2. 验证订单是否存在，以及状态是否为“待处理”
            //    只有“待处理”状态的订单才能被拒绝。
            if (task == null || task.Status != DeliveryStatus.Pending)
            {
                // 如果订单不存在，或者已经被处理，则操作失败
                return false;
            }

            // 3. 更新订单信息
            // 拒绝一个订单，通常意味着它应该被系统“取消”或标记为某种失败状态。
            // 我们这里将其状态更新为“已取消”。
            // 具体的业务逻辑（是取消还是放回订单池）需要根据产品需求确定。
            // 假设拒绝等于取消：
            task.Status = DeliveryStatus.Cancelled;

            /*
            // 备选逻辑：如果拒绝是让订单重新被分配
            task.CourierID = 0; // 或者一个表示“未分配”的特殊ID
            task.Status = DeliveryStatus.Pending; // 状态保持Pending，但可能会有一个“被拒绝次数”的计数器
            */

            // 4. 调用仓储层持久化更改
            await _deliveryTaskRepository.UpdateAsync(task);
            await _deliveryTaskRepository.SaveAsync();

            // 5. 返回 true 表示操作成功
            return true;
        }

        private readonly Random _random = new Random();

        public async Task<decimal> GetMonthlyIncomeAsync(int courierId)
        {
            // 1. 核心模拟逻辑：生成一个 4000 到 9000 之间的随机收入

            // _random.NextDouble() 会生成一个 0.0 到 1.0 之间的随机双精度浮点数
            // (9000 - 4000) = 5000 是范围的跨度
            // 乘以跨度再加上基数 4000，就能得到 4000.0 到 9000.0 之间的随机数
            double randomIncome = 4000 + _random.NextDouble() * 5000;

            // 2. 将结果转换为 decimal 类型，并保留两位小数
            decimal monthlyIncome = Math.Round((decimal)randomIncome, 2);

            // 3. 使用 Task.FromResult 包装结果以满足异步方法的返回类型
            return await Task.FromResult(monthlyIncome);
        }
    }
}