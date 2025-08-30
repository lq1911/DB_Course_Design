namespace BackEnd.Dtos.AfterSaleApplication
{
    public class GetAfterSaleApplication
    {
        public string ApplicationId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string ApplicationTime { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Punishment { get; set; } = "-";
    }


}
