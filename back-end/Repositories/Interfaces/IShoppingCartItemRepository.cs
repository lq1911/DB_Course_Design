using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task<IEnumerable<ShoppingCartItem>> GetAllAsync();
        Task<ShoppingCartItem?> GetByIdAsync(int id);
        //新增
        Task<IEnumerable<ShoppingCartItem>> GetByCartIdAsync(int cartId);
        Task AddAsync(ShoppingCartItem shoppingcartitem);
        Task UpdateAsync(ShoppingCartItem shoppingcartitem);
        Task DeleteAsync(ShoppingCartItem shoppingcartitem);
        Task SaveAsync();
    }
}