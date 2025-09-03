namespace BackEnd.DTOs.User
{
    public class UserProfileDto
    {
        public string Name { get; set; } = null!;
        public long PhoneNumber { get; set; }
        public string? Image { get; set; }
    }
    public class UserAddressDto
    {
        public string Name { get; set; } = null!;
        public long PhoneNumber { get; set; }
        public string Address { get; set; } = null!;
    }
}
