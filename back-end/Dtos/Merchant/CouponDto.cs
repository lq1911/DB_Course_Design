using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.Merchant
{
    /// <summary>
    /// 优惠券DTO - 用于前端显示
    /// </summary>
    public class CouponDto
    {
        public int id { get; set; }                    // 优惠券ID
        public string name { get; set; } = string.Empty;               // 优惠券名称
        public string type { get; set; } = string.Empty;               // 优惠券类型: 'fixed' | 'discount'
        public decimal value { get; set; }             // 优惠值（金额或折扣比例）
        public decimal? minAmount { get; set; }        // 最低消费（仅满减券）
        public string startTime { get; set; } = string.Empty;          // 开始时间（ISO格式）
        public string endTime { get; set; } = string.Empty;            // 结束时间（ISO格式）
        public int totalQuantity { get; set; }         // 发放数量
        public int usedQuantity { get; set; }          // 已使用数量
        public string description { get; set; } = string.Empty;        // 描述
        public string status { get; set; } = string.Empty;             // 状态: 'active' | 'expired' | 'upcoming'
    }

    /// <summary>
    /// 优惠券统计DTO
    /// </summary>
    public class CouponStatsDto
    {
        public int total { get; set; }                 // 总数量
        public int active { get; set; }                // 有效数量
        public int expired { get; set; }               // 已过期数量
        public int upcoming { get; set; }              // 未开始数量
        public int totalUsed { get; set; }             // 总使用次数
        public decimal totalDiscountAmount { get; set; } // 总优惠金额
    }

    /// <summary>
    /// 优惠券列表响应DTO
    /// </summary>
    public class CouponListResponseDto
    {
        public List<CouponDto> list { get; set; } = new List<CouponDto>();
        public int total { get; set; }                 // 总条数
    }

    /// <summary>
    /// 创建优惠券请求DTO
    /// </summary>
    public class CreateCouponRequestDto
    {
        // 优惠券ID由后端自动生成，前端不需要提供
        public int? id { get; set; }

        [Required(ErrorMessage = "优惠券名称不能为空")]
        [MaxLength(100, ErrorMessage = "优惠券名称长度不能超过100个字符")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "优惠券类型不能为空")]
        public string type { get; set; } = string.Empty; // 'fixed' | 'discount'

        [Required(ErrorMessage = "优惠值不能为空")]
        [Range(0.01, 999999.99, ErrorMessage = "优惠值必须在0.01-999999.99之间")]
        public decimal value { get; set; }

        [Range(0.01, 999999.99, ErrorMessage = "最低消费金额必须在0.01-999999.99之间")]
        public decimal? minAmount { get; set; }

        // 优惠金额由后端根据类型自动计算，前端不需要提供
        public decimal? discountAmount { get; set; }

        // 店铺ID由后端自动获取，前端不需要提供
        public int? storeId { get; set; }

        [Required(ErrorMessage = "发放数量不能为空")]
        [Range(1, 100000, ErrorMessage = "发放数量必须在1-100000之间")]
        public int totalQuantity { get; set; }

        [Required(ErrorMessage = "开始时间不能为空")]
        public string startTime { get; set; } = string.Empty;

        [Required(ErrorMessage = "结束时间不能为空")]
        public string endTime { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "描述长度不能超过500个字符")]
        public string description { get; set; } = string.Empty;
    }

    /// <summary>
    /// 更新优惠券请求DTO
    /// </summary>
    public class UpdateCouponRequestDto : CreateCouponRequestDto
    {
        // 继承CreateCouponRequestDto的所有字段，包括id字段
        // 不需要重新定义id字段
    }

    /// <summary>
    /// 批量删除请求DTO
    /// </summary>
    public class BatchDeleteCouponsRequestDto
    {
        [Required(ErrorMessage = "优惠券ID列表不能为空")]
        public List<int> ids { get; set; } = new List<int>();
    }

    /// <summary>
    /// 批量删除响应DTO
    /// </summary>
    public class BatchDeleteResponseDto
    {
        public int deletedCount { get; set; }
    }

    /// <summary>
    /// 创建优惠券响应DTO
    /// </summary>
    public class CreateCouponResponseDto
    {
        public int id { get; set; }                    // 新创建的优惠券ID
    }
}
