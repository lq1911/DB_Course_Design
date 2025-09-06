namespace BackEnd.Dtos.Comment
{
    public class GetCommentInfo
    {
        public string ReviewId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Image { get; set; }
        public string Type { get; set; } = null!;
        public int Rating { get; set; }
        public string SubmitTime { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

    public class SetCommentInfo
    {
        public string ReviewId { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

    public class SetCommentInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public GetCommentInfo? Data { get; set; }
    }
}
