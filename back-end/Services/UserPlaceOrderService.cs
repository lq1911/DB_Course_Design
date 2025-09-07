using BackEnd.Models;
using BackEnd.Dtos.User;
using BackEnd.Services.Interfaces;
using BackEnd.Repositories.Interfaces;

namespace BackEnd.Services
{
    public class UserPlaceOrderService : IUserPlaceOrderService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IFoodOrderRepository _foodOrderRepository;

        public UserPlaceOrderService(IShoppingCartRepository cartRepository, IFoodOrderRepository foodOrderRepository, IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _cartRepository = cartRepository;
            _foodOrderRepository = foodOrderRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var cart = await _cartRepository.GetByIdAsync(dto.CartId);
            if (cart == null || cart.ShoppingCartItems.Count == 0)
            {
                return await Task.FromResult(new ResponseDto
                {
                    Success = false,
                    Message = "购物车为空，无法生成订单"
                });
            }

            var foodOrder = new FoodOrder
            {
                CustomerID = dto.CustomerId,
                CartID = dto.CartId,
                StoreID = dto.StoreId,
                OrderTime = DateTime.Now,
                PaymentTime = dto.PaymentTime,   // 下单时传入
                Remarks = dto.Remarks,
                FoodOrderState = Models.Enums.FoodOrderState.Pending
            };

            await _foodOrderRepository.AddAsync(foodOrder);
            await _foodOrderRepository.SaveAsync();

            return await Task.FromResult(new ResponseDto
            {
                Success = true,
                Message = "订单创建成功"
            });
        }
        public async Task<ResponseDto> UpdateAccountAsync(UpdateAccountDto dto)
        {  


            if (dto == null)
                return new ResponseDto { Success = false, Message = "参数不能为空" };
        // 检查 _userRepository 是否已注入
            if (_userRepository == null)
            {
            
                return new ResponseDto { Success = false, Message = "内部错误" };
            }
            // 查找用户
            var user = await _userRepository.GetByIdAsync(dto.Id);
            if (user == null)
            {
                return new ResponseDto { Success = false, Message = "用户不存在" };
            }

            // 更新用户信息
            user.FullName = dto.Name;
            user.PhoneNumber = dto.PhoneNumber;
            user.Avatar = string.IsNullOrWhiteSpace(dto.Image) 
                          ? "/images/default-avatar.png" // 默认头像路径，可替换成你项目里的默认图片
                          : dto.Image;
            await _userRepository.UpdateAsync(user);
            

            return new ResponseDto { Success = true, Message = "账户信息更新成功" };
        }

        public async Task<ResponseDto> SaveOrUpdateAddressAsync(SaveAddressDto dto)
        {
            if (dto == null)
                return new ResponseDto { Success = false, Message = "参数不能为空" };
            // 查找客户
            var customer = await _customerRepository.GetByIdAsync(dto.Id);
            if (customer == null)
            {
                return new ResponseDto { Success = false, Message = "客户不存在" };
            }

            // 构建地址字符串 (格式: 收件人姓名,手机号,详细地址)
            string addressString = $"{dto.Name},{dto.PhoneNumber},{dto.Address}";
            
            // 更新默认地址
            customer.DefaultAddress = addressString;

            // EF 已跟踪 customer，直接保存即可
            await _customerRepository.SaveAsync();
           

            return new ResponseDto { Success = true, Message = "收货地址保存成功" };
        }
    }
}