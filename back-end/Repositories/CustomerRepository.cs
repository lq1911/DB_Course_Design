using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                                 .Include(c => c.DeliveryTasks)
                                 .Include(c => c.FoodOrders)
                                 .Include(c => c.Coupons)
                                 .Include(c => c.FavoritesFolders)
                                 .Include(c => c.Comments)
                                 .Include(c => c.ShoppingCarts)
                                 .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                                 .Include(c => c.DeliveryTasks)
                                 .Include(c => c.FoodOrders)
                                 .Include(c => c.Coupons)
                                 .Include(c => c.FavoritesFolders)
                                 .Include(c => c.Comments)
                                 .Include(c => c.ShoppingCarts)
                                 .FirstOrDefaultAsync(c => c.UserID == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await SaveAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await SaveAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}