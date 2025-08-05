using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? ReplyToCommentID { get; set; }
        [ForeignKey("ReplyToCommentID")]
        public Comment ReplyToComment { get; set; } = null!;

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        [Required]
        public int CommenterID { get; set; }
        [ForeignKey("CommenterID")]
        public Customer Commenter { get; set; } = null!;

        // 多对多关系
        // 可以由多个管理员负责
        public ICollection<Review_Comment> ReviewComments { get; set; } = new List<Review_Comment>();
    }

}
