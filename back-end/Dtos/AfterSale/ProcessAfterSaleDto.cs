namespace BackEnd.Dtos.AfterSale
{
    public class ProcessAfterSaleDto
    {
        public string Action { get; set; } = null!; // 'approve' | 'reject' | 'negotiate'
        public string Remark { get; set; } = null!;
    }
}