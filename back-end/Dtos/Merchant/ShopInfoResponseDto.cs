namespace BackEnd.Dtos.Merchant
{
    // 用于返回店铺详细信息
    public class ShopInfoResponseDto
    {
        public string Id { get; set; } = string.Empty;           // 店铺ID
        public string Name { get; set; } = string.Empty;         // 店铺名称
        public string CreateTime { get; set; } = string.Empty;   // 创建时间
        public string Address { get; set; } = string.Empty;      // 店铺地址
        public string BusinessHours { get; set; } = string.Empty; // 营业时间
        public string Feature { get; set; } = string.Empty;      // 店铺特色
        public int? CreditScore { get; set; }                    // 信誉积分
    }
} 