using BackEnd.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Services.Interfaces
{
    public interface IUserCheckoutService
    {
        Task<List<UserCouponDto>> GetUserCouponsAsync(int userId);
        Task<CartResponseDto> GetShoppingCartAsync(int userId, int storeId);
        Task<CartResponseDto> UpdateCartItemAsync(UpdateCartItemDto Dto, int storeId);
        Task<CartResponseDto> RemoveCartItemAsync(RemoveCartItemDto Dto, int storeId);
    }

}