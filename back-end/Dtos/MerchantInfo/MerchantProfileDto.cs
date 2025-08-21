namespace BackEnd.Dtos.MerchantInfo
{
    public class MerchantProfileDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RegisterTime { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}