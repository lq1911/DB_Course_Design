using BackEnd.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Services.Interfaces
{
    public interface IUserCouponService
    {
        Task<List<UserCouponDto>> GetUserCouponsAsync(int userId);
    }
    public interface IShoppingCartService
    {
        Task<CartResponseDto> GetShoppingCartAsync(int userId, string storeId);
    }
}