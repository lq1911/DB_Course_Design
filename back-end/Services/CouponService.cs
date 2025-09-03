using BackEnd.Dtos.Merchant;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using BackEnd.Models;
using BackEnd.Models.Enums;

namespace BackEnd.Services
{
    /// <summary>
    /// 优惠券服务实现
    /// </summary>
    public class CouponService : ICouponService
    {
        private readonly ICouponManagerRepository _couponRepository;
        private readonly ILogger<CouponService> _logger;

        public CouponService(ICouponManagerRepository couponRepository, ILogger<CouponService> logger)
        {
            _couponRepository = couponRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取优惠券列表（分页）
        /// </summary>
        public async Task<CouponListResponseDto> GetCouponsAsync(int sellerId, int page, int pageSize)
        {
            try
            {
                _logger.LogInformation("获取商家 {SellerId} 的优惠券列表，页码: {Page}, 页大小: {PageSize}", sellerId, page, pageSize);

                var (coupons, total) = await _couponRepository.GetBySellerIdAsync(sellerId, page, pageSize);
                var couponDtos = coupons.ToDtoList();

                return new CouponListResponseDto
                {
                    list = couponDtos,
                    total = total
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商家 {SellerId} 的优惠券列表失败", sellerId);
                throw;
            }
        }

        /// <summary>
        /// 获取优惠券统计信息
        /// </summary>
        public async Task<CouponStatsDto> GetStatsAsync(int sellerId)
        {
            try
            {
                _logger.LogInformation("获取商家 {SellerId} 的优惠券统计信息", sellerId);

                var (total, active, expired, upcoming, totalUsed, totalDiscountAmount) = 
                    await _couponRepository.GetStatsBySellerIdAsync(sellerId);

                return new CouponStatsDto
                {
                    total = total,
                    active = active,
                    expired = expired,
                    upcoming = upcoming,
                    totalUsed = totalUsed,
                    totalDiscountAmount = totalDiscountAmount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商家 {SellerId} 的优惠券统计信息失败", sellerId);
                throw;
            }
        }

        /// <summary>
        /// 创建优惠券
        /// </summary>
        public async Task<CreateCouponResponseDto> CreateCouponAsync(int sellerId, CreateCouponRequestDto request)
        {
            try
            {
                _logger.LogInformation("商家 {SellerId} 创建优惠券: {CouponName}", sellerId, request.name);

                // 验证请求数据
                ValidateCouponRequest(request);

                // 创建优惠券模型（现在直接从请求中获取storeId）
                var coupon = request.ToModel(sellerId);
                
                // 主键将由Oracle Identity列自动生成，不需要手动设置

                // 保存到数据库
                await _couponRepository.AddAsync(coupon);

                _logger.LogInformation("优惠券创建成功，ID: {CouponId}", coupon.CouponManagerID);

                return new CreateCouponResponseDto
                {
                    id = coupon.CouponManagerID
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商家 {SellerId} 创建优惠券失败: {CouponName}", sellerId, request.name);
                throw;
            }
        }

        /// <summary>
        /// 更新优惠券
        /// </summary>
        public async Task UpdateCouponAsync(int sellerId, UpdateCouponRequestDto request)
        {
            try
            {
                _logger.LogInformation("商家 {SellerId} 更新优惠券: {CouponId}", sellerId, request.id);

                // 验证请求数据
                ValidateCouponRequest(request);

                // 获取优惠券
                var coupon = await _couponRepository.GetByIdAndSellerIdAsync(request.id, sellerId);
                if (coupon == null)
                {
                    throw new ArgumentException($"优惠券 {request.id} 不存在或不属于商家 {sellerId}");
                }

                // 更新优惠券
                coupon.UpdateModel(request);
                await _couponRepository.UpdateAsync(coupon);

                _logger.LogInformation("优惠券 {CouponId} 更新成功", request.id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商家 {SellerId} 更新优惠券 {CouponId} 失败", sellerId, request.id);
                throw;
            }
        }

        /// <summary>
        /// 删除优惠券
        /// </summary>
        public async Task DeleteCouponAsync(int sellerId, int couponId)
        {
            try
            {
                _logger.LogInformation("商家 {SellerId} 删除优惠券: {CouponId}", sellerId, couponId);

                // 获取优惠券
                var coupon = await _couponRepository.GetByIdAndSellerIdAsync(couponId, sellerId);
                if (coupon == null)
                {
                    throw new ArgumentException($"优惠券 {couponId} 不存在或不属于商家 {sellerId}");
                }

                // 删除优惠券
                await _couponRepository.DeleteAsync(coupon);

                _logger.LogInformation("优惠券 {CouponId} 删除成功", couponId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商家 {SellerId} 删除优惠券 {CouponId} 失败", sellerId, couponId);
                throw;
            }
        }

        /// <summary>
        /// 批量删除优惠券
        /// </summary>
        public async Task<BatchDeleteResponseDto> BatchDeleteCouponsAsync(int sellerId, BatchDeleteCouponsRequestDto request)
        {
            try
            {
                _logger.LogInformation("商家 {SellerId} 批量删除优惠券，数量: {Count}", sellerId, request.ids.Count);

                if (request.ids == null || !request.ids.Any())
                {
                    throw new ArgumentException("优惠券ID列表不能为空");
                }

                // 批量删除
                var deletedCount = await _couponRepository.BatchDeleteAsync(request.ids, sellerId);

                _logger.LogInformation("批量删除完成，实际删除数量: {DeletedCount}", deletedCount);

                return new BatchDeleteResponseDto
                {
                    deletedCount = deletedCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商家 {SellerId} 批量删除优惠券失败", sellerId);
                throw;
            }
        }

        /// <summary>
        /// 验证优惠券请求数据
        /// </summary>
        private void ValidateCouponRequest(CreateCouponRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.name))
                throw new ArgumentException("优惠券名称不能为空");

            if (request.type != "fixed" && request.type != "discount")
                throw new ArgumentException("优惠券类型必须是 'fixed' 或 'discount'");

            if (request.value <= 0)
                throw new ArgumentException("优惠值必须大于0");

            if (request.type == "fixed" && request.value >= 1)
                throw new ArgumentException("满减券的优惠金额不能大于等于1");

            if (request.type == "discount" && (request.value <= 0 || request.value >= 1))
                throw new ArgumentException("折扣券的折扣比例必须在0-1之间");

            if (request.type == "fixed" && request.minAmount <= request.value)
                throw new ArgumentException("满减券的最低消费必须大于优惠金额");

            if (request.totalQuantity <= 0)
                throw new ArgumentException("发放数量必须大于0");

            if (!DateTime.TryParse(request.startTime, out var startTime))
                throw new ArgumentException("开始时间格式不正确");

            if (!DateTime.TryParse(request.endTime, out var endTime))
                throw new ArgumentException("结束时间格式不正确");

            if (endTime <= startTime)
                throw new ArgumentException("结束时间必须晚于开始时间");
        }

        /// <summary>
        /// 获取商家的默认店铺ID（临时方案）
        /// </summary>
        private async Task<int> GetDefaultStoreIdForSeller(int sellerId)
        {
            // TODO: 这里应该从数据库查询商家的店铺
            // 临时返回一个默认值，后续需要实现真正的逻辑
            _logger.LogWarning("使用临时默认店铺ID，商家ID: {SellerId}", sellerId);
            return 101; // 临时默认店铺ID
        }
    }
}
