using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.Merchant
{
    // 用于接收更新店铺字段的请求数据
    public class UpdateShopFieldRequestDto
    {
        [Required]
        public string Field { get; set; } = string.Empty;  // 字段名（'address' | 'businessHours' | 'feature'）
        
        [Required]
        public string Value { get; set; } = string.Empty;  // 字段值
    }
} 