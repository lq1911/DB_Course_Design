using BackEnd.Dtos.Merchant;

namespace BackEnd.Services.Interfaces
{
    public interface IMerchantService
    {
        // 获取店铺概况（评分、月销量、营业状态）
        Task<ShopOverviewResponseDto> GetShopOverviewAsync(int sellerId);
        
        // 获取店铺详细信息
        Task<ShopInfoResponseDto?> GetShopInfoAsync(int sellerId);
        
        // 获取商家信息
        Task<MerchantInfoResponseDto?> GetMerchantInfoAsync(int sellerId);
        
        // 切换营业状态
        Task<CommonResponseDto> ToggleBusinessStatusAsync(int sellerId, ToggleBusinessStatusRequestDto request);
        
        // 更新店铺字段
        Task<CommonResponseDto> UpdateShopFieldAsync(int sellerId, UpdateShopFieldRequestDto request);
    }
} 