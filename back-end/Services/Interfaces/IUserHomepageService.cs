using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Dtos.User;

namespace BackEnd.Services.Interfaces
{
    public interface IUserHomepageService
    {
        // 获取推荐商家（评分最高的若干店铺中随机选4个）
        Task<HomeRecmDto> GetRecommendedStoresAsync();

        // 搜索商家和菜品
        Task<(IEnumerable<HomeSearchGetDto> Stores, IEnumerable<HomeSearchGetDto> Dishes)>
            SearchAsync(HomeSearchDto searchDto);
        Task<List<HistoryOrderDto>> GetOrderHistoryAsync(int userId);
        Task<UserInfoResponse> GetUserInfoAsync(int userId);

        // 获取指定用户的优惠券信息
        Task<IEnumerable<CouponDto>> GetUserCouponsAsync(UserIdDto userIdDto);

        Task<StoresResponseDto> GetAllStoresAsync();

    }
    
}
