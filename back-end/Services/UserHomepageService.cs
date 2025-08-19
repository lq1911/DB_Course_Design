using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.UserHomepage;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Services
{
    public class UserHomepageService : IUserHomepageService
    {
        private readonly AppDbContext _context;

        public UserHomepageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HomeRecmDto>> GetRecommendedStoresAsync()
        {
            var stores = await _context.Stores
            .AsNoTracking()
            .ToListAsync();

            // 按评分排序，取前10个，然后随机选4个
            var topStores = stores
                .OrderByDescending(s => s.AverageRating)
                .Take(10)
                .ToList();

            var random = new Random();
            var recommended = topStores
                .OrderBy(s => random.Next())
                .Take(4)
                .Select(s => new HomeRecmDto
                {
                    StoreID = s.StoreID,
                    StoreName = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                });

            return recommended;
        }

        public async Task<(IEnumerable<HomeSearchGetDto> Stores, IEnumerable<HomeSearchGetDto> Dishes)>
            SearchAsync(HomeSearchDto searchDto)
        {
            // 店铺搜索
            var stores = await _context.Stores
            .AsNoTracking()
            .ToListAsync();
            var storeResults = stores
                .Where(s => s.StoreName.Contains(searchDto.SearchName))
                .Select(s => new HomeSearchGetDto
                {
                    AverageRating = s.AverageRating,
                    StoreName = s.StoreName,
                    MonthlySales = s.MonthlySales,
                    StoreAddress = s.StoreAddress
                })
                .ToList();

            // 菜品搜索
            var dishes = await _context.Dishes
            .AsNoTracking()
            .ToListAsync();

            var menus = await _context.Menus
            .AsNoTracking()
            .ToListAsync();
            var menuDishes = await _context.Menu_Dishes
            .AsNoTracking()
            .ToListAsync();

            var dishResults = dishes
                .Where(d => d.DishName.Contains(searchDto.SearchName))
                .Join(menuDishes,
                      dish => dish.DishID,
                      md => md.DishID,
                      (dish, md) => new { dish, md })
                .Join(menus,
                      dm => dm.md.MenuID,
                      menu => menu.MenuID,
                      (dm, menu) => new { dm.dish, menu })
                .Join(stores,
                      dmMenu => dmMenu.menu.StoreID,
                      store => store.StoreID,
                      (dmMenu, store) => new HomeSearchGetDto
                      {
                          AverageRating = store.AverageRating,
                          StoreName = store.StoreName,
                          MonthlySales = store.MonthlySales,
                          StoreAddress = store.StoreAddress
                      })
                .Distinct()
                .ToList();

            return (storeResults, dishResults);
        }
        public async Task<HistoryOrderGetDto> GetOrderHistoryAsync(int userId)
        {
            // 查询用户的所有订单，包含相关数据
            var orders = await _context.FoodOrders
                .Where(o => o.CustomerID == userId)
                .Include(o => o.Store)  // 包含商家信息
                .Include(o => o.Cart)   // 包含购物车
                   .ThenInclude(cart => cart.ShoppingCartItems)
                   .ThenInclude(item => item.Dish)
                .OrderByDescending(o => o.PaymentTime)  // 按支付时间倒序排序
                .ToListAsync();

            // 转换为DTO
            var result = new HistoryOrderGetDto();

            foreach (var order in orders)
            {
                var orderDto = new HistoryOrderDto
                {
                    OrderID = order.OrderID,
                    PaymentTime = order.PaymentTime,
                    CartID = order.CartID,
                    StoreID = order.StoreID,
                    StoreName = order.Store?.StoreName ?? "未知商家",
                    //StoreImage = order.Store?.ImageUrl
                };

                // 添加订单中的菜品
                if (order.Cart?.ShoppingCartItems != null)
                {
                    foreach (var cartItem in order.Cart.ShoppingCartItems)
                    {
                        orderDto.OrderedDishes.Add(new DishDto
                        {
                            DishID = cartItem.DishID,
                            DishName = cartItem.Dish?.DishName ?? "未知菜品",
                            Price = cartItem.Dish?.Price ?? 0,

                            //DishImage = cartItem.Dish?.ImageUrl
                        });
                    }
                }

                result.Orders.Add(orderDto);
            }

            return result;
        }
        public async Task<UserInfoResponse> GetUserInfoAsync(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserID == userId);
            
            if (user == null)
                return null;

            return new UserInfoResponse
            {
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar
            };
        }
        
    }
}
