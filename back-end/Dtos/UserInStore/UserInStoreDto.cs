using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection.Metadata;
using BackEnd.Models.Enums;


namespace BackEnd.Dtos.UserInStore
{
    public class StoreRequestDto
    {

        [Required]
        public int StoreID;
    }

    public class StoreResponseDto
    {
        [Required]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = null!;

        [Required]
        public string StoreImage { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string StoreAddress { get; set; } = null!;

        // 开业时间
        [Required]
        public TimeSpan OpenTime { get; set; } = TimeSpan.FromHours(9);  // 09:00

        [Required]
        public TimeSpan CloseTime { get; set; } = TimeSpan.FromHours(22); // 22:00

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }

        [Required]
        [StringLength(500)]
        public string StoreDiscription { get; set; } = null!;

        [Required]
        public DateTime StoreCreationTime { get; set; }
    }

    public class MenuRequestDto
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int StoreID { get; set; }
    }

    public class MenuResponseDto
    {
        [Required]
        public int DishID { get; set; }

        public int DishCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string DishName { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Dishimage { get; set; } = null!;

        [Required]
        public DishIsSoldOut IsSoldOut { get; set; } = DishIsSoldOut.IsSoldOut;
    }

    public class CommentResponseDto
    {
        [Required]
        public int CommentID { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        [Required]
        public DateTime PostedAt { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = null!;

        [MaxLength(255)]
        public string? Avatar { get; set; }

        public string[] CommentImage { get; set; } = Array.Empty<string>();
    }

    public class CommentStateDto
    {
        public IEnumerable<int> Status { get; set; } = new List<int>(); 
        // [好评数, 中评数, 差评数]
    }
}
