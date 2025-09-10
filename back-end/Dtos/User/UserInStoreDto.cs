using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackEnd.Models.Enums;


namespace BackEnd.Dtos.User
{
    public class StoreRequestDto
    {

        [Required]
        public int StoreId { get; set; }
    }

    public class StoreResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Address { get; set; } = string.Empty;

        // 开业时间

        [Required]
        [JsonIgnore]
        public TimeSpan OpenTime { get; set; } = TimeSpan.FromHours(9);  // 09:00

        [Required]
        [JsonIgnore]
        public TimeSpan CloseTime { get; set; } = TimeSpan.FromHours(22); // 22:00

        public string BusinessHours { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Rating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime CreateTime { get; set; }
    }

    public class MenuRequestDto
    {
        [Required]
        public int StoreId { get; set; }
    }

    public class MenuResponseDto
    {
        [Required]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        public DishIsSoldOut IsSoldOut { get; set; } = DishIsSoldOut.IsSoldOut;
    }

    public class CommentResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Range(1, 5)]
        public int? Rating { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Avatar { get; set; }

        public string[] Images { get; set; } = Array.Empty<string>();
    }

    public class CommentStateDto
    {
        public IEnumerable<int> Status { get; set; } = new List<int>();
        // [好评数, 中评数, 差评数]
    }

    public class CreateCommentDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "评分必须在 1 到 5 之间")]
        public int Rating { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "评论内容不能超过 500 字")]

        public string Content { get; set; } = string.Empty;
    }

    public class UserStoreReportDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "投诉内容不能超过 500 字")]
        public string Content { get; set; } = string.Empty;
    }
}
