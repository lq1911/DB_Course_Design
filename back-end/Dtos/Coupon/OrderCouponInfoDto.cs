namespace BackEnd.Dtos.Coupon
{
    public class OrderCouponInfoDto
    {
        public int CouponId { get; set; }
        public string? CouponName { get; set; }
        public string? Description { get; set; }
        public string DiscountType { get; set; } = null!; // "percentage" 或 "fixed"
        public decimal DiscountValue { get; set; }
        public string ValidFrom { get; set; } = null!;
        public string ValidTo { get; set; } = null!;
        public bool IsUsed { get; set; }
    }
}