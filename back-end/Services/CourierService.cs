using BackEnd.DTOs.Courier; // 确保 using 了新的 DTO 路径
using BackEnd.Models;
using BackEnd.Models.Enums; // 引入枚举的命名空间
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; // 引入 LINQ 的命名空间
using System.Threading.Tasks;

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
            // ... (这个方法的实现保持不变)
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

        // --- 这是 GetWorkStatusAsync 的最终实现 ---
        public async Task<WorkStatusDto?> GetWorkStatusAsync(int courierId)
        {
            // 1. 查询骑手的基础状态信息
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null)
            {
                return null; // 骑手不存在
            }

            // 2. 计算在线时长
            int onlineHours = 0;
            int onlineMinutes = 0;
            if (courier.IsOnline && courier.LastOnlineTime.HasValue)
            {
                TimeSpan onlineDuration = DateTime.UtcNow - courier.LastOnlineTime.Value;
                onlineHours = (int)onlineDuration.TotalHours;
                onlineMinutes = onlineDuration.Minutes;
            }

            // 3. 查询今日的订单数据
            // 定义今天的时间范围
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            // 因为 GetAllAsync 返回 IEnumerable，我们会将所有任务加载到内存中再进行筛选。
            // 注意：如果数据量巨大，这里是性能优化的关键点。
            var allTasks = await _deliveryTaskRepository.GetAllAsync();
            var todayTasks = allTasks.Where(t => t.CourierID == courierId && t.AcceptTime >= today && t.AcceptTime < tomorrow).ToList();

            int todayOrders = todayTasks.Count;

            // 筛选出其中已完成的任务
            var completedTasks = todayTasks.Where(t => t.Status == DeliveryStatus.Completed).ToList();
            int completedOrders = completedTasks.Count;

            // 4. 计算准时率
            double punctualityRate = 0.0;
            if (completedOrders > 0)
            {
                // 筛选出准时的任务 (实际完成时间 <= 预计送达时间)
                int punctualCount = completedTasks.Count(t => t.CompletionTime.HasValue && t.CompletionTime.Value <= t.EstimatedDeliveryTime);
                punctualityRate = Math.Round((double)punctualCount / completedOrders * 100, 2);
            }

            // 5. 组装并返回 DTO
            var statusDto = new WorkStatusDto
            {
                IsOnline = courier.IsOnline,
                OnlineHours = onlineHours,
                OnlineMinutes = onlineMinutes,
                TodayOrders = todayOrders,
                CompletedOrders = completedOrders,
                PunctualityRate = punctualityRate
            };

            return statusDto;
        }
        

        // --- 这是 GetOrdersAsync 的最终实现 ---
        public async Task<IEnumerable<OrderListItemDto>> GetOrdersAsync(int courierId, string status)
        {
            // 1. 将前端传来的 status 字符串映射到我们定义的 DeliveryStatus 枚举
            //    这样做更安全、更健壮，可以避免直接比较字符串。
            if (!Enum.TryParse<DeliveryStatus>(status, true, out var targetStatus))
            {
                // 如果前端传来一个无效的状态值 (比如 "abc")，返回一个空列表
                return new List<OrderListItemDto>();
            }

            // 2. 构建查询
            //    由于 IDeliveryTaskRepository 的 GetAllAsync 返回 IEnumerable，
            //    我们将先加载所有任务到内存，然后进行筛选。
            //    注意：为了提高性能，可以让仓储层支持 IQueryable，这样就可以先筛选再执行SQL。
            var allTasks = await _deliveryTaskRepository.GetAllAsync();
            var filteredTasks = allTasks
                .Where(t => t.CourierID == courierId && t.Status == targetStatus)
                .OrderByDescending(t => t.PublishTime) // 按发布时间降序排序，让最新的订单在前面
                .ToList();

            // 3. 将查询到的 DeliveryTask 实体列表，映射到 DTO 列表
            //    使用 Select (LINQ) 可以非常方便地进行批量转换。
            var orderDtos = filteredTasks.Select(task => new OrderListItemDto
            {
                Id = task.TaskID.ToString(),
                // 如果 Store 导航属性没有被加载，这里会是 null。
                // 需要确保仓储层加载了关联数据，或者在这里手动查询。
                Restaurant = task.Store?.StoreName ?? "未知餐厅", 
                Address = task.Store?.StoreAddress ?? "未知地址",
                Fee = task.DeliveryFee.ToString("F2"), // 格式化为两位小数的字符串
                Time = task.PublishTime.ToString("yyyy-MM-dd HH:mm:ss"), // 格式化时间
                Status = task.Status.ToString().ToLower(), // 将枚举转为小写字符串，如 "delivering"
                StatusText = GetStatusText(task.Status) // 使用辅助方法获取中文描述
            }).ToList();

            return orderDtos;
        }

        // 辅助方法：将枚举状态转换为中文描述
        private string GetStatusText(DeliveryStatus status)
        {
            switch (status)
            {
                case DeliveryStatus.Pending:
                    return "待处理";
                case DeliveryStatus.Delivering:
                    return "配送中";
                case DeliveryStatus.Completed:
                    return "已完成";
                case DeliveryStatus.Cancelled:
                    return "已取消";
                default:
                    return "未知状态";
            }
        }
    }
}