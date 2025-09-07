using BackEnd.Data; // 【新增】引入 AppDbContext 所在的命名空间
using BackEnd.DTOs.Courier;
using BackEnd.Models; // 【新增】引入 Models 命名空间
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System; // 【新增】引入基础命名空间
using System.Collections.Generic; // 【新增】引入集合命名空间
using System.Linq; // 【新增】引入 LINQ 命名空间
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class CourierService : ICourierService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly IDeliveryTaskRepository _deliveryTaskRepository;
        private readonly AppDbContext _context; // 【新增】用于数据库事务的上下文

        // 【已修正】构造函数，增加了 AppDbContext 的注入
        public CourierService(
            IUserRepository userRepository,
            ICourierRepository courierRepository,
            IDeliveryTaskRepository deliveryTaskRepository,
            AppDbContext context) // <-- 新增参数
        {
            _userRepository = userRepository;
            _courierRepository = courierRepository;
            _deliveryTaskRepository = deliveryTaskRepository;
            _context = context; // <-- 新增赋值
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

        // CourierService.cs
public async Task<string> GetCurrentLocationAsync(int courierId)
{
    // 1. 从数据库中查询骑手信息
    var courier = await _courierRepository.GetByIdAsync(courierId);

    // 2. 检查骑手是否存在，以及是否有坐标信息
    if (courier == null || !courier.CourierLongitude.HasValue || !courier.CourierLatitude.HasValue)
    {
        return "位置信息未提供"; // 返回一个明确的默认值
    }

    // 3. 核心模拟逻辑：根据数据库中的经纬度，构造一个用于展示的模拟字符串
    var simulatedArea = $"模拟位置 (经度: {courier.CourierLongitude.Value:F6}, 纬度: {courier.CourierLatitude.Value:F6})";

    // 4. 使用 Task.FromResult 将字符串包装成异步方法需要的 Task<string> 类型并返回
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

        public async Task<IEnumerable<OrderListItemDto>> GetOrdersAsync(int courierId, string status)
        {
            if (!Enum.TryParse<DeliveryStatus>(status, true, out var targetStatus))
            {
                return new List<OrderListItemDto>();
            }
            var tasksQuery = _deliveryTaskRepository.GetQueryable()
                .Where(t => t.CourierID == courierId && t.Status == targetStatus)
                .Include(t => t.Store)
                .Include(t => t.Customer);
            var tasks = await tasksQuery
                .OrderByDescending(t => t.PublishTime)
                .ToListAsync();
            var orderDtos = tasks.Select(task => new OrderListItemDto
            {
                Id = task.TaskID.ToString(),
                Status = task.Status.ToString().ToLower(),
                Restaurant = task.Store?.StoreName ?? "未知商家",
                Address = task.Customer?.DefaultAddress ?? "未知地址",
                Fee = task.DeliveryFee.ToString("F2"),
                StatusText = GetStatusText(task.Status)
            }).ToList();
            return orderDtos;
        }

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

        public async Task<NewOrderDetailsDto?> GetNewOrderDetailsAsync(int notificationId)
        {
            var taskId = notificationId;
            var task = await _deliveryTaskRepository.GetQueryable()
                .Include(t => t.Store)
                .Include(t => t.Customer)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(t => t.TaskID == taskId);
            if (task == null)
            {
                return null;
            }
            var orderDetailsDto = new NewOrderDetailsDto
            {
                Id = task.TaskID.ToString(),
                RestaurantName = task.Store?.StoreName ?? "未知商家",
                RestaurantAddress = task.Store?.StoreAddress ?? "未知商家地址",
                CustomerName = task.Customer?.User?.FullName ?? task.Customer?.User?.Username ?? "未知顾客",
                CustomerAddress = task.Customer?.DefaultAddress ?? "未知顾客地址",
                Fee = task.DeliveryFee,
                Distance = "约 2.5 公里",
                MapImageUrl = "https://example.com/static-map.png"
            };
            return orderDetailsDto;
        }

        public async Task<bool> AcceptOrderAsync(int courierId, int orderId)
        {
            var task = await _deliveryTaskRepository.GetByIdAsync(orderId);
            if (task == null || task.Status != DeliveryStatus.Pending)
            {
                return false;
            }
            task.Status = DeliveryStatus.Delivering;
            task.CourierID = courierId;
            task.AcceptTime = DateTime.UtcNow;
            await _deliveryTaskRepository.UpdateAsync(task);
            await _deliveryTaskRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RejectOrderAsync(int orderId)
        {
            var task = await _deliveryTaskRepository.GetByIdAsync(orderId);
            if (task == null || task.Status != DeliveryStatus.Pending)
            {
                return false;
            }
            task.Status = DeliveryStatus.Cancelled;
            await _deliveryTaskRepository.UpdateAsync(task);
            await _deliveryTaskRepository.SaveAsync();
            return true;
        }

        public async Task<decimal> GetMonthlyIncomeAsync(int courierId)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null)
            {
                return 0.00m;
            }
            decimal totalMonthlyIncome = courier.MonthlySalary + courier.CommissionThisMonth;
            return totalMonthlyIncome;
        }

        // 【已修正】
        public async Task MarkTaskAsCompletedAsync(int taskId, int courierId)
        {
            // 使用事务确保数据的一致性
            using var transaction = await _context.Database.BeginTransactionAsync(); // <-- _context 现在可用
            try
            {
                // 1. 找到配送任务，并验证它是否属于当前操作的骑手
                var task = await _deliveryTaskRepository.GetByIdAsync(taskId);
                if (task == null || task.CourierID != courierId || task.Status == DeliveryStatus.Completed)
                {
                    await transaction.RollbackAsync();
                    return;
                }

                // 2. 更新任务状态
                task.Status = DeliveryStatus.Completed;
                task.CompletionTime = DateTime.UtcNow;
                await _deliveryTaskRepository.UpdateAsync(task); // <-- 已添加 await

                // 3. 找到对应的骑手
                var courier = await _courierRepository.GetByIdAsync(task.CourierID);
                if (courier != null)
                {
                    // 4. 为骑手累加本月提成
                    courier.CommissionThisMonth += task.DeliveryFee;
                    await _courierRepository.UpdateAsync(courier); // <-- 已添加 await
                }

                // 5. 一次性保存所有更改到数据库
                // (假设你的 SaveAsync 是对 SaveChangesAsync 的封装，如果不是，就用下面这行)
                await _context.SaveChangesAsync();

                // 6. 提交事务
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // 向上抛出异常，让 Controller 层知道操作失败了
            }
        }
    }
}