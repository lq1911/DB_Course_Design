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
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<CouponService> _logger;

        public CouponService(ICouponManagerRepository couponRepository, IStoreRepository storeRepository, ILogger<CouponService> logger)
        {
            _couponRepository = couponRepository;
            _storeRepository = storeRepository;
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

                int? storeIdNullable = await _storeRepository.GetStoreIdBySellerIdAsync(sellerId);
                if (!storeIdNullable.HasValue)
                {
                    throw new ArgumentException($"商家 {sellerId} 没有对应的店铺");
                }
                int storeId = storeIdNullable.Value;

                var (coupons, total) = await _couponRepository.GetByStoreIdAsync(storeId, page, pageSize);
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

                int? storeIdNullable = await _storeRepository.GetStoreIdBySellerIdAsync(sellerId);
                if (!storeIdNullable.HasValue)
                {
                    throw new ArgumentException($"商家 {sellerId} 没有对应的店铺");
                }
                int storeId = storeIdNullable.Value;

                var (total, active, expired, upcoming, totalUsed, totalDiscountAmount) =
                    await _couponRepository.GetStatsByStoreIdAsync(storeId);

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

                // 获取商家默认店铺ID
                var storeId = await GetDefaultStoreIdForSeller(sellerId);
                _logger.LogInformation("商家 {SellerId} 的店铺ID: {StoreId}", sellerId, storeId);

                // 创建优惠券模型（使用自动获取的storeId）
                var coupon = request.ToModel(sellerId, storeId);

                // 主键将由Oracle Identity列自动生成，不需要手动设置

                // 保存到数据库
                await _couponRepository.AddAsync(coupon);

                // 获取数据库生成的ID
                var generatedId = coupon.CouponManagerID;
                _logger.LogInformation("优惠券创建成功，ID: {CouponId}", generatedId);

                return new CreateCouponResponseDto
                {
                    id = generatedId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商家 {SellerId} 创建优惠券失败: {CouponName}", sellerId, request.name);

                // 特殊处理Oracle磁盘空间不足错误
                if (ex.Message.Contains("ORA-01114") && ex.Message.Contains("No space left on device"))
                {
                    _logger.LogCritical("数据库磁盘空间不足，无法创建优惠券");
                    throw new InvalidOperationException("数据库磁盘空间不足，请联系管理员清理空间后重试。错误详情：ORA-01114 - 磁盘空间不足");
                }

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

                int? storeIdNullable = await _storeRepository.GetStoreIdBySellerIdAsync(sellerId);
                if (!storeIdNullable.HasValue)
                {
                    throw new ArgumentException($"商家 {sellerId} 没有对应的店铺");
                }
                int storeId = storeIdNullable.Value;

                // 获取优惠券
                var coupon = await _couponRepository.GetByIdAndStoreIdAsync(request.id ?? 0, storeId);
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

                int? storeIdNullable = await _storeRepository.GetStoreIdBySellerIdAsync(sellerId);
                if (!storeIdNullable.HasValue)
                {
                    throw new ArgumentException($"商家 {sellerId} 没有对应的店铺");
                }
                int storeId = storeIdNullable.Value;

                // 获取优惠券
                var coupon = await _couponRepository.GetByIdAndStoreIdAsync(couponId, storeId);
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

                int? storeIdNullable = await _storeRepository.GetStoreIdBySellerIdAsync(sellerId);
                if (!storeIdNullable.HasValue)
                {
                    throw new ArgumentException($"商家 {sellerId} 没有对应的店铺");
                }
                int storeId = storeIdNullable.Value;

                if (request.ids == null || !request.ids.Any())
                {
                    throw new ArgumentException("优惠券ID列表不能为空");
                }

                // 批量删除
                var deletedCount = await _couponRepository.BatchDeleteAsync(request.ids, storeId);

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
        /// 检查数据库连接状态
        /// </summary>
        public async Task<bool> CheckDatabaseHealthAsync()
        {
            try
            {
                // 尝试执行一个简单的查询来检查数据库连接
                var testResult = await _couponRepository.GetStatsByStoreIdAsync(1);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库健康检查失败");

                // 检查是否是磁盘空间问题
                if (ex.Message.Contains("ORA-01114") && ex.Message.Contains("No space left on device"))
                {
                    _logger.LogCritical("数据库磁盘空间不足，系统无法正常运行");
                }

                return false;
            }
        }

        /// <summary>
        /// 获取商家默认店铺ID
        /// </summary>
        private async Task<int> GetDefaultStoreIdForSeller(int sellerId)
        {
            // 通过商家ID查找店铺ID
            int? storeIdNullable = await _storeRepository.GetStoreIdBySellerIdAsync(sellerId);
            if (!storeIdNullable.HasValue)
            {
                throw new ArgumentException($"商家 {sellerId} 没有对应的店铺");
            }
            int storeId = storeIdNullable.Value;

            return storeId;
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

            if (request.type == "discount" && (request.value <= 0 || request.value >= 1))
                throw new ArgumentException("折扣券的折扣比例必须在0-1之间");

            if (request.type == "fixed" && request.minAmount.HasValue && request.minAmount <= request.value)
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

    }
}
