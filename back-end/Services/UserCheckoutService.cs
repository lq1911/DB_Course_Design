using BackEnd.Models;
using BackEnd.Dtos.User;
using BackEnd.Models.Enums;
using BackEnd.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using BackEnd.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class UserCheckoutService : IUserCheckoutService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IDishRepository _dishRepository; // 用于获取菜品单价

        public UserCheckoutService(IUserRepository userRepository, ICouponRepository couponRepository, IShoppingCartRepository shoppingCartRepository, ICustomerRepository customerRepository, IShoppingCartItemRepository shoppingCartItemRepository, IDishRepository dishRepository)
        {
            _userRepository = userRepository;
            _couponRepository = couponRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _customerRepository = customerRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _dishRepository = dishRepository;
        }

        public async Task<List<UserCouponDto>> GetUserCouponsAsync(int userId)
        {
            // 验证用户是否存在
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ValidationException("用户不存在");
            }

            // 检查用户是否有顾客记录
            if (user.Customer == null)
            {
                return new List<UserCouponDto>();
            }

            // 查询用户的优惠券信息
            var coupons = await _couponRepository.GetByCustomerIdAsync(user.Customer.UserID);

            // 转换为 DTO
            return coupons.Select(c => new UserCouponDto
            {
                CouponID = c.CouponID,
                CouponState = GetActualCouponState(c),
                OrderID = c.OrderID,
                CouponManagerID = c.CouponManagerID,
                MinimumSpend = c.CouponManager.MinimumSpend,
                DiscountAmount = c.CouponManager.DiscountAmount,
                ValidTo = c.CouponManager.ValidTo.ToString("yyyy-MM-ddTHH:mm:ss")
            }).ToList();
        }

        private CouponState GetActualCouponState(Coupon coupon)
        {
            // 如果优惠券已过期且未使用，返回过期状态
            if (coupon.IsExpired && coupon.CouponState == CouponState.Unused)
            {
                return CouponState.Expired;
            }

            return coupon.CouponState;
        }
        public async Task<CartResponseDto> GetShoppingCartAsync(int userId, int storeId)
        {
            // 验证用户是否存在
            var customer = await _customerRepository.GetByIdAsync(userId);
            if (customer == null)
            {
                throw new ValidationException("用户不存在或不是顾客");
            }

            // 查找该用户未锁定的购物车
            var shoppingCart = await _shoppingCartRepository
                .GetActiveCartByCustomerIdAsync(customer.UserID); // 自定义方法：查找 IsLocked == false 的购物车

            // 如果没有未锁定的购物车，就新建一个
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    CustomerID = customer.UserID,
                    ShoppingCartItems = new List<ShoppingCartItem>(),
                    LastUpdatedTime = DateTime.UtcNow,
                    ShoppingCartState = ShoppingCartState.UnCompleted
                };

                await _shoppingCartRepository.AddAsync(shoppingCart);
            }

            // 获取购物车项（只取该店铺的）
            var cartItems = shoppingCart.ShoppingCartItems?
                .Where(item => item.Dish.MenuDishes.Any(md => md.Menu.Store.StoreID == storeId))
                .ToList() ?? new List<ShoppingCartItem>();

            // 计算总价
            var filteredTotalPrice = cartItems.Sum(item => item.TotalPrice);

            // 转换为 DTO
            return new CartResponseDto
            {
                CartId = shoppingCart.CartID,
                TotalPrice = filteredTotalPrice,
                Items = cartItems.Select(item => new ShoppingCartItemDto
                {
                    ItemId = item.ItemID,
                    DishId = item.DishID,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };
        }


        public async Task UpdateCartItemAsync(UpdateCartItemDto dto)
        {
            // 1. 获取购物车
            var shoppingCart = await _shoppingCartRepository.GetByIdAsync(dto.CartId);
            if (shoppingCart == null)
            {
                throw new ValidationException("购物车不存在");
            }

            // 2. 获取菜品
            var dish = await _dishRepository.GetByIdAsync(dto.DishId);

            // 3. 查找购物车项
            var cartItem = shoppingCart.ShoppingCartItems?
            .FirstOrDefault(item => item.DishID == dto.DishId);

            if (cartItem == null)
            {
                // 新增购物车项
                cartItem = new ShoppingCartItem
                {
                    DishID = dto.DishId,
                    Quantity = dto.Quantity,
                    TotalPrice = dish.Price * dto.Quantity,
                    CartID = shoppingCart.CartID
                };
                await _shoppingCartItemRepository.AddAsync(cartItem);
            }
            else
            {
                // 更新购物车项
                cartItem.Quantity = dto.Quantity;
                cartItem.TotalPrice = dish.Price * dto.Quantity;
                await _shoppingCartItemRepository.UpdateAsync(cartItem);
            }

            // 4. 更新购物车总价（只算该店铺的商品）
            shoppingCart.TotalPrice = shoppingCart.ShoppingCartItems?
                .Sum(item => item.TotalPrice) ?? 0;

            shoppingCart.LastUpdatedTime = DateTime.UtcNow;
            await _shoppingCartRepository.UpdateAsync(shoppingCart);
        }

        public async Task RemoveCartItemAsync(RemoveCartItemDto dto)
        {
            // 1. 获取购物车
            var shoppingCart = await _shoppingCartRepository.GetByIdAsync(dto.CartId);
            if (shoppingCart == null)
            {
                throw new ValidationException("购物车不存在");
            }

            // 2. 查找购物车项
            var cartItem = shoppingCart.ShoppingCartItems?
                .FirstOrDefault(item => item.DishID == dto.DishId);

            if (cartItem == null)
            {
                throw new ValidationException("该菜品不在购物车中");
            }

            // 3. 删除购物车项
            await _shoppingCartItemRepository.DeleteAsync(cartItem);

            // 4. 更新购物车总价（只算该店铺的商品）
            shoppingCart.TotalPrice = shoppingCart.ShoppingCartItems?
                .Sum(item => item.TotalPrice) ?? 0;

            shoppingCart.LastUpdatedTime = DateTime.UtcNow;
            await _shoppingCartRepository.UpdateAsync(shoppingCart);
        }
    }

}