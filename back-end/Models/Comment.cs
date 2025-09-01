using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class Comment
    {
        // 评价类：
        // 主码：CommentID
        // 外码：ReplyToCommentID，StoreID，CommenterID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime PostedAt { get; set; }

        public int Likes { get; set; } = 0;
        public int Replies { get; set; } = 0;

        // 新增属性
        [Range(1, 5)]
        public int? Rating { get; set; }

        [StringLength(1000)] // 假设所有URL加起来总长度不超过1000个字符，你可以根据需要调整
        public string? CommentImage { get; set; }

        [Required]
        public CommentType CommentType { get; set; }

        public int? StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store? Store { get; set; }

        public int? FoodOrderID { get; set; }
        [ForeignKey("FoodOrderID")]
        public FoodOrder? FoodOrder { get; set; }

        public int? ReplyToCommentID { get; set; }
        [ForeignKey("ReplyToCommentID")]
        public Comment? ReplyToComment { get; set; }

        [Required]
        public int CommenterID { get; set; }
        [ForeignKey("CommenterID")]
        public Customer Commenter { get; set; } = null!;

        // 一对多导航属性
        public ICollection<Comment>? CommentReplies { get; set; }

        // 多对多关系
        // 可以由多个管理员负责
        public ICollection<Review_Comment> ReviewComments { get; set; } = new List<Review_Comment>();
    }
}
