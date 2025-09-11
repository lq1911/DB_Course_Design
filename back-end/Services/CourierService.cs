using BackEnd.Data; // 【新增】引入 AppDbContext 所在的命名空间
using BackEnd.Dtos.Courier;
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
        private readonly IGeoHelper _geoHelper; // <-- 新增这一行

        // 【已修正】构造函数，增加了 AppDbContext 的注入
        public CourierService(
            IUserRepository userRepository,
            ICourierRepository courierRepository,
            IDeliveryTaskRepository deliveryTaskRepository,
            AppDbContext context,
            IGeoHelper geoHelper) // <-- 新增参数
        {
            _userRepository = userRepository;
            _courierRepository = courierRepository;
            _deliveryTaskRepository = deliveryTaskRepository;
            _context = context; // <-- 新增赋值
            _geoHelper = geoHelper;
        }

        public async Task<CourierProfileDto?> GetProfileAsync(int courierId)
        {
            // 依然使用 AsNoTracking() 来避免缓存问题
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Courier)
                .FirstOrDefaultAsync(u => u.UserID == courierId);

            if (user == null)
            {
                return null;
            }

            return new CourierProfileDto
            {
                Id = user.UserID.ToString(),
                Name = user.Username,
                RegisterDate = user.AccountCreationTime.ToString("yyyy-MM-dd"),
                Rating = user.Courier?.AverageRating ?? 0,
                CreditScore = user.Courier?.ReputationPoints ?? 0,

                // --- 关键修正：把 Avatar 的值也映射过来 ---
                Avatar = user.Avatar
                // ---------------------------------------------
            };
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
            var simulatedArea = $"(经度: {courier.CourierLongitude.Value:F6}, 纬度: {courier.CourierLatitude.Value:F6})";

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
                case DeliveryStatus.To_Be_Taken: return "待处理";
                case DeliveryStatus.Pending: return "待取餐";
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


        public async Task<bool> AcceptOrderAsync(int courierId, int taskId)
        {
            // 步骤 1: 使用 FindAsync 直接、高效地查找任务实体
            // 这种方式不会生成不必要的 INNER JOIN
            var taskToAccept = await _context.DeliveryTasks.FindAsync(taskId);

            // 步骤 2: 验证订单是否存在，以及状态是否正确
            // 必须是存在的、并且状态是 "To_Be_Taken" 的订单才能被接受
            if (taskToAccept == null || taskToAccept.Status != DeliveryStatus.To_Be_Taken)
            {
                // 如果订单不存在，或已被其他人接受，或状态不正确，则返回失败
                return false;
            }

            // 步骤 3: 更新订单状态和信息
            taskToAccept.CourierID = courierId; // 将当前骑手ID分配给这个任务
            taskToAccept.Status = DeliveryStatus.Pending; // 将状态更新为 "Pending" (待取件)
            taskToAccept.AcceptTime = DateTime.UtcNow; // 记录接单时间 (使用 UTC 时间是好习惯)

            // 步骤 4: 保存更改到数据库
            await _context.SaveChangesAsync();

            // 返回成功
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
                var courier = await _courierRepository.GetByIdAsync(task.CourierID!.Value);
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

        // CourierService.cs

        /// <summary>
        /// 骑手确认取货
        /// </summary>
        public async Task<bool> PickupOrderAsync(int orderId, int courierId)
        {
            var task = await _deliveryTaskRepository.GetByIdAsync(orderId);

            // 验证：任务是否存在、是否分配给了当前骑手、状态是否为“待处理”
            if (task == null || task.CourierID != courierId || task.Status != DeliveryStatus.Pending)
            {
                return false;
            }

            // 更新状态
            task.Status = DeliveryStatus.Delivering;
            // (可选) 如果你的模型中有 ActualPickupTime 字段，可以在这里记录
            // task.ActualPickupTime = DateTime.UtcNow;

            await _deliveryTaskRepository.UpdateAsync(task);
            await _deliveryTaskRepository.SaveAsync();

            return true;
        }

        /// <summary>
        /// 骑手确认送达
        /// </summary>
        public async Task<bool> DeliverOrderAsync(int orderId, int courierId)
        {
            // 注意：这个方法的逻辑与我们之前写的 MarkTaskAsCompletedAsync 非常相似！
            // 我们可以直接复用或整合。为了清晰，我们先独立实现。
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var task = await _deliveryTaskRepository.GetByIdAsync(orderId);

                // 验证：任务是否存在、是否分配给了当前骑手、状态是否为“配送中”
                if (task == null || task.CourierID != courierId || task.Status != DeliveryStatus.Delivering)
                {
                    return false;
                }

                // 更新任务状态
                task.Status = DeliveryStatus.Completed;
                task.CompletionTime = DateTime.UtcNow;
                await _deliveryTaskRepository.UpdateAsync(task);

                // 累加骑手提成
                var courier = await _courierRepository.GetByIdAsync(courierId);
                if (courier != null)
                {
                    courier.CommissionThisMonth += task.DeliveryFee;
                    await _courierRepository.UpdateAsync(courier);
                }

                // 统一保存
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // 向上抛出异常
            }
        }



        public async Task<IEnumerable<AvailableOrderDto>> GetAvailableOrdersAsync(int courierId, decimal latitude, decimal longitude, decimal maxDistance)
        {
            var tasksQuery = _context.DeliveryTasks
                .Where(task => task.Status == DeliveryStatus.To_Be_Taken)
                .Include(task => task.Store)
                .Include(task => task.Order)
                    .ThenInclude(order => order.Customer)
                        .ThenInclude(customer => customer.User);

            var allTasks = await tasksQuery.ToListAsync();

            var nearbyTasks = new List<DeliveryTask>();
            foreach (var task in allTasks)
            {
                if (task.Store?.Latitude.HasValue == true && task.Store?.Longitude.HasValue == true)
                {
                    // 使用注入的 _geoHelper 服务进行计算
                    var distanceToStore = _geoHelper.CalculateDistance(
                        latitude, longitude,
                        task.Store.Latitude.Value, task.Store.Longitude.Value
                    );

                    if (distanceToStore <= (double)maxDistance)
                    {
                        nearbyTasks.Add(task);
                    }
                }
            }

            var resultDtos = nearbyTasks.Select(task => new AvailableOrderDto
            {
                Id = task.TaskID.ToString(),
                Status = "to_be_taken",
                Restaurant = task.Store.StoreName,
                PickupAddress = task.Store.StoreAddress,
                Customer = task.Order.Customer.User.Username,
                Fee = task.DeliveryFee.ToString("F2"),
                DeliveryAddress = "接单后可见详细地址", // 占位符
                Distance = "2.5",                   // 占位符
                Time = "15"                         // 占位符
            }).ToList();

            return resultDtos;
        }


        public async Task<IEnumerable<ComplaintDto>> GetComplaintsAsync(int courierId)
        {
            // 1. 直接从 DeliveryComplaints 表查询，因为 CourierID 已是该表的列
            var complaints = await _context.DeliveryComplaints
                .Where(c => c.CourierID == courierId) // 直接根据骑手ID筛选，确保只能看自己的
                .OrderByDescending(c => c.ComplaintTime) // 按投诉时间降序排列
                .ToListAsync();

            // 2. 将数据库实体映射为 DTO，严格遵循前端 TS 格式
            var complaintDtos = complaints.Select(complaint =>
            {
                PunishmentDto? punishmentDto = null;

                // 逻辑：如果存在处理结果（ProcessingResult 不为空或"-")，
                // 则我们认为这是一个 "处罚" 或 "处理决定"。
                if (!string.IsNullOrEmpty(complaint.ProcessingResult) && complaint.ProcessingResult != "-")
                {
                    punishmentDto = new PunishmentDto
                    {
                        // 将 ProcessingResult 用作 'description'，因为它是最详细的信息
                        Description = complaint.ProcessingResult,

                        // 模型中没有单独的 'type' 和 'duration' 字段，
                        // 我们设定一个通用的类型，并将 duration 留空 (null)
                        Type = "官方处理结果",
                        Duration = null
                    };
                }

                return new ComplaintDto
                {
                    ComplaintID = complaint.ComplaintID.ToString(),
                    DeliveryTaskID = complaint.DeliveryTaskID.ToString(),
                    ComplaintTime = complaint.ComplaintTime.ToString("yyyy-MM-dd HH:mm"), // 格式化为前端需要的字符串
                    ComplaintReason = complaint.ComplaintReason,
                    Punishment = punishmentDto // 赋值我们刚刚构建的 punishmentDto 对象
                };
            }).ToList();

            return complaintDtos;
        }


        public async Task<bool> UpdateCourierLocationAsync(int courierId, decimal latitude, decimal longitude)
        {
            // 1. 根据 courierId 查找骑手实体
            var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.UserID == courierId);

            // 2. 如果找不到骑手，操作失败
            if (courier == null)
            {
                return false;
            }

            // 3. 更新骑手的经纬度属性
            // 注意：这里的属性名 CourierLatitude 和 CourierLongitude 必须与你的 Courier.cs 模型文件中的完全一致
            courier.CourierLatitude = latitude;
            courier.CourierLongitude = longitude;

            // 4. 保存更改到数据库
            await _context.SaveChangesAsync();

            // 5. 操作成功
            return true;
        }


        public async Task<bool> UpdateProfileAsync(int courierId, UpdateProfileDto profileDto)
        {
            // --- 步骤 1: 更新 User 表 ---
            var userToUpdate = await _context.Users.FindAsync(courierId);
            if (userToUpdate == null)
            {
                // 如果连用户基础信息都找不到，直接返回失败
                return false;
            }

            // 将 DTO 中的数据赋值给 User 实体
            userToUpdate.Username = profileDto.Name;
            userToUpdate.Gender = profileDto.Gender;
            userToUpdate.Birthday = profileDto.Birthday;
            userToUpdate.Avatar = profileDto.Avatar;

            // 标记 User 实体为已修改状态
            _context.Users.Update(userToUpdate);

            // --- 步骤 2: 更新 Courier 表 ---
            var courierToUpdate = await _context.Couriers.FindAsync(courierId);
            if (courierToUpdate == null)
            {
                // 如果骑手专属信息找不到，也返回失败（理论上不应发生）
                return false;
            }

            // 将 DTO 中的数据赋值给 Courier 实体
            courierToUpdate.VehicleType = profileDto.VehicleType;

            // 标记 Courier 实体为已修改状态
            _context.Couriers.Update(courierToUpdate);

            // --- 步骤 3: 一次性将所有更改保存到数据库 ---
            // SaveChangesAsync 会在一个事务中执行所有更新操作
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<UpdateProfileDto?> GetProfileForEditAsync(int courierId)
        {
            var user = await _context.Users
                .AsNoTracking() // 确保从数据库读取最新数据
                .Include(u => u.Courier)
                .FirstOrDefaultAsync(u => u.UserID == courierId);

            if (user == null || user.Courier == null)
            {
                return null;
            }

            // 直接创建并返回一个 UpdateProfileDto 对象
            return new UpdateProfileDto
            {
                Name = user.Username,
                Gender = user.Gender,
                Birthday = user.Birthday, // 直接返回 DateTime? 类型
                Avatar = user.Avatar,
                VehicleType = user.Courier.VehicleType
            };
        }






    }
}