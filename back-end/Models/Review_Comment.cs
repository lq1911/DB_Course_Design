using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Review_Comment
    {
        // 管理员与评论之间的审核评论关系
        // 主码：AdminID，CommentID

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public Administrator Admin { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int CommentID { get; set; }
        [ForeignKey("CommentID")]
        public Comment Comment { get; set; } = null!;

        [Required]
        public DateTime ReviewTime { get; set; }
    }
}
