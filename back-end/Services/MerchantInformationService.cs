using BackEnd.Dtos.MerchantInfo;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class MerchantInformationService : IMerchantInformationService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IUserRepository _userRepository;

        public MerchantInformationService(ISellerRepository sellerRepository, IUserRepository userRepository)
        {
            _sellerRepository = sellerRepository;
            _userRepository = userRepository;
        }

        public async Task<(bool Success, string? Message, MerchantProfileDto? Data)> GetMerchantInfoAsync(int merchantUserId)
        {
            // 获取商家信息及关联用户
            var seller = await _sellerRepository.GetByIdAsync(merchantUserId);
            if (seller == null)
                return (false, "商家不存在", null);

            var user = seller.User;
            if (user == null)
                return (false, "用户信息不存在", null);

            // 转换状态显示文本
            var statusText = seller.BanStatus == SellerState.Normal ? "正常营业" : "封禁中";

            var result = new MerchantProfileDto
            {
                Id = seller.UserID.ToString(),
                Name = user.Username,
                Phone = user.PhoneNumber.ToString(),
                Email = user.Email,
                RegisterTime = seller.SellerRegistrationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = statusText
            };

            return (true, null, result);
        }

        public async Task<(bool Success, string? Message, MerchantUpdateResultDto? Data)> UpdateMerchantInfoAsync(int merchantUserId, UpdateMerchantProfileDto dto)
        {
            // 获取用户信息
            var user = await _userRepository.GetByIdAsync(merchantUserId);
            if (user == null)
                return (false, "用户不存在", null);

            var updatedFields = new List<string>();

            // 处理手机号更新
            if (long.TryParse(dto.Phone, out long newPhone) && user.PhoneNumber != newPhone)
            {
                user.PhoneNumber = newPhone;
                updatedFields.Add("phone");
            }

            // 处理邮箱更新
            if (user.Email != dto.Email)
            {
                user.Email = dto.Email;
                updatedFields.Add("email");
            }

            // 保存更新
            if (updatedFields.Count > 0)
            {
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveAsync();

                return (true, null, new MerchantUpdateResultDto
                {
                    UpdatedFields = updatedFields.ToArray(),
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            return (true, "没有需要更新的信息", null);
        }
    }
}