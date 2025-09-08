using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCart>> GetAllAsync();
        Task<ShoppingCart?> GetByIdAsync(int id);
        Task<ShoppingCart?> GetActiveCartWithStoreFilterAsync(int customerId, int storeId);
        Task<ShoppingCart?> GetByCustomerIdAsync(int customerId);
        Task<ShoppingCart?> GetActiveCartByCustomerIdAsync(int customerId);
        Task AddAsync(ShoppingCart shoppingcart);
        Task UpdateAsync(ShoppingCart shoppingcart);
        Task DeleteAsync(ShoppingCart shoppingcart);
        Task SaveAsync();
    }
}