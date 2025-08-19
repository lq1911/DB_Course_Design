using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Menu
    {
        // 菜单类
        // 主码：MenuID
        // 外码：StoreID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }

        [Required]
        [StringLength(50)]
        public string Version { get; set; } = null!;

        [Required]
        public DateTime ActivePeriod { get; set; }

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; } = null!;

        // 多对多关系
        // 一个菜单包含多个菜品
        public ICollection<Menu_Dish> MenuDishes { get; set; } = new List<Menu_Dish>();

        // 便捷属性：直接获取菜品列表
        [NotMapped]
        public IEnumerable<Dish> Dishes => MenuDishes.Select(md => md.Dish);
    }
}
