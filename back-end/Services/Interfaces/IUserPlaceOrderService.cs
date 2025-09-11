using BackEnd.Dtos.User;

namespace BackEnd.Services.Interfaces
{
    public interface IUserPlaceOrderService
    {
        Task<ResponseDto> CreateOrderAsync(CreateOrderDto dto);
        Task<ResponseDto> UpdateAccountAsync(UpdateAccountDto dto);
        Task<string> UpdateUserAvatarAsync(int userId, IFormFile file);
        Task<ResponseDto> SaveOrUpdateAddressAsync(SaveAddressDto dto);
    }
}