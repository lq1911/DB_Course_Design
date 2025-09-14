using BackEnd.Dtos.User;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using System.Text.RegularExpressions;

namespace BackEnd.Services
{
    public class UserDebugService : IUserDebugService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFoodOrderRepository _foodOrderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICouponRepository _couponRepository;

        public UserDebugService(IUserRepository userRepository,
                                IFoodOrderRepository foodOrderRepository,
                                IShoppingCartRepository shoppingCartRepository,
                                ICouponRepository couponRepository)
        {
            _userRepository = userRepository;
            _foodOrderRepository = foodOrderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _couponRepository = couponRepository;
        }

        public async Task<UserInfoResponseDto> GetUserInfoAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("用户不存在");
            }

            // 映射到 DTO
            var dto = new UserInfoResponseDto
            {
                Name = user.Username,
                PhoneNumber = user.PhoneNumber,
                Image = user.Avatar!,
                DefaultAddress = user.Customer?.DefaultAddress! // Customer 导航属性里的地址
            };

            return dto;
        }

        public async Task SubmitOrderAsync(SubmitOrderRequestDto dto)
        {
            // 找到用户未锁定的购物车
            var cart = await _shoppingCartRepository.GetByIdAsync(dto.CartId);

            if (cart == null)
            {
                Console.WriteLine("[SubmitOrderAsync] CRITICAL: Shopping cart query returned NULL.");
                throw new InvalidOperationException("没有可用的购物车，请先添加商品");
            }
            else
            {
                Console.WriteLine($"[SubmitOrderAsync] Cart found! CartID: {cart.CartID}, State: {cart.ShoppingCartState}, Belongs to StoreID: {cart.StoreID}");
            }

            // 检查购物车状态
            if (cart.Order != null || cart.ShoppingCartState == ShoppingCartState.Done)
            {
                Console.WriteLine($"[SubmitOrderAsync] CRITICAL: Cart is already processed. State: {cart.ShoppingCartState}");
                throw new InvalidOperationException("该购物车已生成过订单，不能重复下单");
            }

            // 解析时间
            if (!DateTime.TryParse(dto.PaymentTime, out DateTime paymentTime))
            {
                Console.WriteLine($"[SubmitOrderAsync] CRITICAL: Invalid PaymentTime format received: {dto.PaymentTime}");
                throw new ArgumentException("PaymentTime 格式无效", nameof(dto.PaymentTime));
            }

            // 创建订单
            var order = new FoodOrder
            {
                OrderTime = DateTime.UtcNow,
                PaymentTime = paymentTime.ToUniversalTime(),
                CustomerID = dto.CustomerId,
                CartID = cart.CartID,
                StoreID = dto.StoreId,
                DeliveryFee = dto.DeliveryFee,
                FoodOrderState = FoodOrderState.Pending
            };
            await _foodOrderRepository.AddAsync(order);

            // 锁定购物车（保留历史记录）
            cart.ShoppingCartState = ShoppingCartState.Done;
            cart.LastUpdatedTime = DateTime.UtcNow;
            await _shoppingCartRepository.UpdateAsync(cart);
        }

        /// <summary>
        /// 使用优惠券（直接删除）
        /// </summary>
        public async Task UseCouponAsync(int couponId)
        {
            var coupon = await _couponRepository.GetByIdAsync(couponId);
            if (coupon == null)
                throw new InvalidOperationException("优惠券不存在");

            // 删除优惠券
            await _couponRepository.DeleteAsync(coupon);
        }

        public async Task<GetUserIdResponseDto> GetUserIdAsync(GetUserIdRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Account))
            {
                throw new ArgumentException("账号不能为空");
            }

            // 判断是邮箱还是手机号
            bool isEmail = Regex.IsMatch(dto.Account, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            bool isPhone = long.TryParse(dto.Account, out long phoneNumber);

            if (isEmail)
            {
                // 根据邮箱查找
                var user = await _userRepository.GetByEmailAsync(dto.Account);
                if (user == null)
                    throw new KeyNotFoundException("邮箱未找到对应用户");

                return new GetUserIdResponseDto { Id = user.UserID };
            }
            else if (isPhone)
            {
                // 根据手机号查找
                var user = await _userRepository.GetByPhoneAsync(phoneNumber);
                if (user == null)
                    throw new KeyNotFoundException("手机号未找到对应用户");

                return new GetUserIdResponseDto { Id = user.UserID };
            }
            else
            {
                throw new ArgumentException("账号格式不正确，应为手机号或邮箱");
            }
        }
    }
}