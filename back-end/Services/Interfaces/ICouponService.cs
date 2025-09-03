using BackEnd.Dtos.Merchant;

namespace BackEnd.Services.Interfaces
{
    /// <summary>
    /// 优惠券服务接口
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// 获取优惠券列表（分页）
        /// </summary>
        Task<CouponListResponseDto> GetCouponsAsync(int sellerId, int page, int pageSize);

        /// <summary>
        /// 获取优惠券统计信息
        /// </summary>
        Task<CouponStatsDto> GetStatsAsync(int sellerId);

        /// <summary>
        /// 创建优惠券
        /// </summary>
        Task<CreateCouponResponseDto> CreateCouponAsync(int sellerId, CreateCouponRequestDto request);

        /// <summary>
        /// 更新优惠券
        /// </summary>
        Task UpdateCouponAsync(int sellerId, UpdateCouponRequestDto request);

        /// <summary>
        /// 删除优惠券
        /// </summary>
        Task DeleteCouponAsync(int sellerId, int couponId);

        /// <summary>
        /// 批量删除优惠券
        /// </summary>
        Task<BatchDeleteResponseDto> BatchDeleteCouponsAsync(int sellerId, BatchDeleteCouponsRequestDto request);
    }
}
