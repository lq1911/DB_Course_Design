namespace BackEnd.Dtos.Administrator
{
    public class GetAdminInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string RealName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string RegistrationDate { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string ManagementScope { get; set; } = string.Empty;
        public decimal AverageRating { get; set; }
        public bool IsPublic { get; set; }
    }

    public class SetAdminInfo
    {
        public string Username { get; set; } = string.Empty;
        public string ManagementScope { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
    }

    public class SetAdminInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAdminInfo? Data { get; set; }
    }
}
