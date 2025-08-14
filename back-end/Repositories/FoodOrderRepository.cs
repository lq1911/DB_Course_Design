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
            return await _context.FoodOrders
                                 .Include(fo => fo.Customer)               // 顾客
                                 .Include(fo => fo.Cart)                   // 购物车
                                 .Include(fo => fo.Store)                  // 店铺
                                 .Include(fo => fo.Coupons)                // 优惠券
                                 .Include(fo => fo.AfterSaleApplications)  // 售后申请
                                 .ToListAsync();
        }

        public async Task<FoodOrder?> GetByIdAsync(int id)
        {
            return await _context.FoodOrders
                                 .Include(fo => fo.Customer)
                                 .Include(fo => fo.Cart)
                                 .Include(fo => fo.Store)
                                 .Include(fo => fo.Coupons)
                                 .Include(fo => fo.AfterSaleApplications)
                                 .FirstOrDefaultAsync(fo => fo.OrderID == id);
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