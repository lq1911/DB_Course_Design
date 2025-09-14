namespace BackEnd.Dtos.Merchant
{
    // 用于返回商家信息
    public class MerchantInfoResponseDto
    {
        public string Username { get; set; } = string.Empty;  // 商家用户名
        public int SellerId { get; set; }
    }
}