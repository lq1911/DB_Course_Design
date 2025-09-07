using BackEnd.Models;
using BackEnd.Models.Enums;

namespace BackEnd.Dtos.Merchant
{
    /// <summary>
    /// 优惠券映射器 - 用于模型和DTO之间的转换
    /// </summary>
    public static class CouponMapper
    {
        /// <summary>
        /// 将CouponManager模型转换为CouponDto
        /// </summary>
        public static CouponDto ToDto(this CouponManager coupon)
        {
            return new CouponDto
            {
                id = coupon.CouponManagerID,
                name = coupon.CouponName,
                type = coupon.CouponType == CouponType.Fixed ? "fixed" : "discount",
                value = coupon.CouponType == CouponType.Fixed ? coupon.DiscountAmount : (coupon.DiscountRate ?? 0),
                minAmount = coupon.CouponType == CouponType.Fixed ? coupon.MinimumSpend : null,
                startTime = coupon.ValidFrom.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                endTime = coupon.ValidTo.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                totalQuantity = coupon.TotalQuantity,
                usedQuantity = coupon.UsedQuantity,
                description = coupon.Description ?? "",
                status = coupon.Status
            };
        }

        /// <summary>
        /// 将CreateCouponRequestDto转换为CouponManager模型
        /// </summary>
        public static CouponManager ToModel(this CreateCouponRequestDto dto, int sellerId, int storeId)
        {
            var couponType = dto.type == "fixed" ? CouponType.Fixed : CouponType.Discount;
            
            return new CouponManager
            {
                CouponManagerID = dto.id,  // 使用前端提供的主键
                CouponName = dto.name,
                CouponType = couponType,
                MinimumSpend = dto.minAmount,
                DiscountAmount = dto.discountAmount,
                DiscountRate = couponType == CouponType.Discount ? dto.value : null,
                TotalQuantity = dto.totalQuantity,
                UsedQuantity = 0,
                ValidFrom = DateTime.Parse(dto.startTime),
                ValidTo = DateTime.Parse(dto.endTime),
                Description = dto.description,
                StoreID = storeId,  // 使用传入的storeId参数
                SellerID = sellerId
            };
        }

        /// <summary>
        /// 更新CouponManager模型
        /// </summary>
        public static void UpdateModel(this CouponManager model, CreateCouponRequestDto dto)
        {
            var couponType = dto.type == "fixed" ? CouponType.Fixed : CouponType.Discount;
            
            model.CouponName = dto.name;
            model.CouponType = couponType;
            model.MinimumSpend = dto.minAmount;
            model.DiscountAmount = dto.discountAmount;
            model.DiscountRate = couponType == CouponType.Discount ? dto.value : null;
            model.TotalQuantity = dto.totalQuantity;
            model.ValidFrom = DateTime.Parse(dto.startTime);
            model.ValidTo = DateTime.Parse(dto.endTime);
            model.Description = dto.description;
            // 注意：更新时不改变店铺ID，保持原有的店铺关联
            // model.StoreID = dto.storeId;
        }

        /// <summary>
        /// 将优惠券列表转换为DTO列表
        /// </summary>
        public static List<CouponDto> ToDtoList(this IEnumerable<CouponManager> coupons)
        {
            return coupons.Select(c => c.ToDto()).ToList();
        }
    }
}
