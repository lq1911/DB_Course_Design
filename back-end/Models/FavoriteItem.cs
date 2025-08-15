using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models
{
    public class FavoriteItem
    {
        // 收藏夹项类
        // 主码：ItemID
        // 外码：StoreID，FolderID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }

        [Required]
        public DateTime FavoritedAt { get; set; }

        [Required]
        [StringLength(500)]
        public string FavoriteReason { get; set; } = null!;

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        [Required]
        public int FolderID { get; set; }
        [ForeignKey("FolderID")]
        public FavoritesFolder Folder { get; set; } = null!;
    }
}
