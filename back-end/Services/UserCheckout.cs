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
    public class UserCouponService : IUserCouponService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerRepository _customerRepository;

        public UserCouponService(IUserRepository userRepository, ICouponRepository couponRepository, IShoppingCartRepository shoppingCartRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _couponRepository = couponRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _customerRepository = customerRepository;
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
         public async Task<CartResponseDto> GetShoppingCartAsync(int userId, string storeId)
        {
            // 验证用户是否存在并获取顾客信息
            var customer = await _customerRepository.GetByIdAsync(userId);
            if (customer == null)
            {
                throw new ValidationException("用户不存在或不是顾客");
            }

            // 尝试将storeId转换为int
            if (!int.TryParse(storeId, out int storeIdInt))
            {
                throw new ValidationException("店铺ID格式不正确");
            }

            // 获取用户的购物车
            var shoppingCart = await _shoppingCartRepository.GetByCustomerIdAsync(customer.UserID);
            
            // 如果购物车不存在，返回空购物车
            if (shoppingCart == null)
            {
                return new CartResponseDto
                {
                    CartId = 0,
                    TotalPrice = 0,
                    Items = new List<ShoppingCartItemDto>()
                };
            }

           // 获取购物车项，并筛选出属于指定店铺的商品
            var cartItems = shoppingCart.ShoppingCartItems?
                .Where(item => item.Dish.MenuDishes.Any(md => md.Menu.Store.StoreID == storeIdInt))
                .ToList() ?? new List<ShoppingCartItem>();

            // 计算筛选后的总价
            var filteredTotalPrice = cartItems.Sum(item => item.TotalPrice);

            // 转换为DTO
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
    }
}