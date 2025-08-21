namespace BackEnd.Dtos.MerchantInfo
{
    public class MerchantUpdateResultDto
    {
        public string[] UpdatedFields { get; set; } = Array.Empty<string>();
        public string UpdateTime { get; set; } = null!;
    }
}