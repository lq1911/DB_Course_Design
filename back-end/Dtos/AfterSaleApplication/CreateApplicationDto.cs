namespace BackEnd.Dtos.AfterSaleApplication
{
    public class CreateApplicationDto
    {
        public int OrderId { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class CreateApplicationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? ApplicationId { get; set; }
    }
}
