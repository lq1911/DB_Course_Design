namespace BackEnd.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string OrderNo { get; set; } = null!;
        public RUserInfoDto User { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
    }
}