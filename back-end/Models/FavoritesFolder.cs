using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class FavoritesFolder
    {
        // 收藏夹类
        // 主码：FolderID
        // 外码：CustomerID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FolderID { get; set; }

        [Required]
        [StringLength(50)]
        public string FolderName { get; set; } = null!;

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        // 一对多导航属性
        public ICollection<FavoriteItem>? FavoriteItems { get; set; }
    }
}
