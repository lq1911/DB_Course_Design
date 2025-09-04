namespace BackEnd.Dtos.AfterSale
{
    public class AfterSaleApplicationDto
    {
        public int Id { get; set; }
        public string OrderNo { get; set; } = null!;
        public AUserInfoDto User { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
    }
}