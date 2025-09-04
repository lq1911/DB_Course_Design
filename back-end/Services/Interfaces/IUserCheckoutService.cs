using BackEnd.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Services.Interfaces
{
    public interface IUserCheckoutService
    {
        Task<List<UserCouponDto>> GetUserCouponsAsync(int userId);
        Task<CartResponseDto> GetShoppingCartAsync(int userId, int storeId);
        Task UpdateCartItemAsync(UpdateCartItemDto Dto);
        Task RemoveCartItemAsync(RemoveCartItemDto Dto);
    }

}