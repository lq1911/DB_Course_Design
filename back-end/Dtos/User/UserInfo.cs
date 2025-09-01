namespace BackEnd.Dtos.User
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Role { get; set; }
        public string? Avatar { get; set; }
    }
}
