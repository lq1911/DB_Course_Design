namespace BackEnd.Dtos.Merchant
{
    // 用于返回店铺概况数据
    public class ShopOverviewResponseDto
    {
        public decimal Rating { get; set; }        // 店铺评分
        public int MonthlySales { get; set; }     // 月销量
        public bool IsOpen { get; set; }          // 营业状态
    }
} 