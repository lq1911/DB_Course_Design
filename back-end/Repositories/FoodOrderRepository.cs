using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class FoodOrderRepository : IFoodOrderRepository
    {
        private readonly AppDbContext _context;
        public FoodOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodOrder>> GetAllAsync()
        {
            var orders = await _context.FoodOrders
                                       .Include(fo => fo.Customer)               // 顾客
                                       .Include(fo => fo.Cart)                   // 购物车
                                       .Include(fo => fo.Store)                  // 店铺
                                       .Include(fo => fo.Coupons)                // 优惠券
                                       .Include(fo => fo.AfterSaleApplications)  // 售后申请
                                       .Include(fo => fo.Comments)               // 评论
                                       .ToListAsync();

            // 批量加载 DeliveryTasks
            var orderIds = orders.Select(o => o.OrderID).ToList();
            var tasks = await _context.DeliveryTasks
                .Where(d => orderIds.Contains(d.OrderID))
                .Select(d => new { d.OrderID, d.TaskID, d.Status })
                .ToListAsync();

            var taskDict = tasks.ToDictionary(t => t.OrderID);

            foreach (var order in orders)
            {
                if (taskDict.TryGetValue(order.OrderID, out var t))
                {
                    order.DeliveryTask = new DeliveryTask
                    {
                        TaskID = t.TaskID,
                        Status = t.Status,
                        OrderID = order.OrderID
                    };
                }
            }

            return orders;
        }

        public async Task<IEnumerable<FoodOrder>> GetByUserIdAsync(int userId)
        {
            var orders = await _context.FoodOrders
                                       .Where(fo => fo.CustomerID == userId)
                                       .Include(fo => fo.Customer)
                                       .Include(fo => fo.Cart)
                                       .Include(fo => fo.Store)
                                       .Include(fo => fo.Coupons)
                                       .Include(fo => fo.AfterSaleApplications)
                                       .ToListAsync();

            // 批量加载 DeliveryTasks
            var orderIds = orders.Select(o => o.OrderID).ToList();
            var tasks = await _context.DeliveryTasks
                .Where(d => orderIds.Contains(d.OrderID))
                .Select(d => new { d.OrderID, d.TaskID, d.Status })
                .ToListAsync();

            var taskDict = tasks.ToDictionary(t => t.OrderID);

            foreach (var order in orders)
            {
                if (taskDict.TryGetValue(order.OrderID, out var t))
                {
                    order.DeliveryTask = new DeliveryTask
                    {
                        TaskID = t.TaskID,
                        Status = t.Status,
                        OrderID = order.OrderID
                    };
                }
            }

            return orders;
        }

        public async Task<FoodOrder?> GetByIdAsync(int id)
        {
            return await _context.FoodOrders
                                 .Include(fo => fo.Customer)
                                 .Include(fo => fo.Cart)
                                 .Include(fo => fo.Store)
                                 .Include(fo => fo.Coupons)
                                 .Include(fo => fo.AfterSaleApplications)
                                 .Include(fo => fo.Comments)
                                 .FirstOrDefaultAsync(fo => fo.OrderID == id);
        }

        public async Task<List<FoodOrder>> GetOrdersByCustomerIdOrderedByDateAsync(int customerId)
        {
            var orders = await _context.FoodOrders
                .Where(o => o.CustomerID == customerId)
                .OrderByDescending(o => o.OrderTime)
                .ToListAsync();

            // 单独查 DeliveryTasks
            var orderIds = orders.Select(o => o.OrderID).ToList();
            var tasks = await _context.DeliveryTasks
                .Where(d => orderIds.Contains(d.OrderID))
                .Select(d => new { d.OrderID, d.TaskID, d.Status })
                .ToListAsync();

            var taskDict = tasks.ToDictionary(t => t.OrderID);

            foreach (var order in orders)
            {
                if (taskDict.TryGetValue(order.OrderID, out var t))
                {
                    order.DeliveryTask = new DeliveryTask
                    {
                        TaskID = t.TaskID,
                        Status = t.Status
                    };
                }
            }

            return orders;
        }


        public async Task<FoodOrder?> GetByCartIdAsync(int cartId)
        {
            return await _context.FoodOrders
                .FirstOrDefaultAsync(o => o.CartID == cartId);
        }

        public async Task AddAsync(FoodOrder foodOrder)
        {
            await _context.FoodOrders.AddAsync(foodOrder);
            await SaveAsync();
        }

        public async Task UpdateAsync(FoodOrder foodOrder)
        {
            _context.FoodOrders.Update(foodOrder);
            await SaveAsync();
        }

        public async Task DeleteAsync(FoodOrder foodOrder)
        {
            _context.FoodOrders.Remove(foodOrder);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}