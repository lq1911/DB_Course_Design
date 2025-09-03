using BackEnd.Data;
using BackEnd.Models;
using BackEnd.DTOs.User;
using Microsoft.EntityFrameworkCore;
using BackEnd.Repositories.Interfaces;
namespace BackEnd.Services
{
    public class UserService : IUserAdditionService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileDto
            {
                Name = user.Username,
                PhoneNumber = user.PhoneNumber,
                Image = user.Avatar
            };
        }
        public async Task<UserAddressDto?> GetUserAddressAsync(int userId)
        {
            // 获取用户和关联的Customer信息
            var userWithCustomer = await _userRepository.GetByIdAsync(userId);
            if (userWithCustomer == null || userWithCustomer.Customer == null)
            {
                return null;
            }
            
            var customer = userWithCustomer.Customer;
            
            // 检查默认地址是否存在
            if (string.IsNullOrEmpty(customer.DefaultAddress))
            {
                return null;
            }
            
            // 使用用户的全名作为收货人姓名，如果没有全名则使用用户名
            string recipientName = !string.IsNullOrEmpty(userWithCustomer.FullName) ? 
                userWithCustomer.FullName : userWithCustomer.Username;
            
            return new UserAddressDto
            {
                Name = recipientName,
                PhoneNumber = userWithCustomer.PhoneNumber, // 使用用户的电话号码
                Address = customer.DefaultAddress
            };
        }
    }
}