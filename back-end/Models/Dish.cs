using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class Dish
    {
        // 菜品类
        // 主码：DishID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DishID { get; set; }

        [Required]
        [StringLength(50)]
        public string DishName { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public DishIsSoldOut IsSoldOut { get; set; } = DishIsSoldOut.IsSoldOut;
        public DishTypes Type { get; set; } = DishTypes.SignatureRecommendation;

        // 商家ID，关联到Seller表
        [Required]
        public int SellerID { get; set; }

        // 新增菜品图片
        public string? DishImage { get; set; }

        // 一对多导航属性
        // 购物车项
        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }

        // 多对多关系
        // 一个菜品可以出现在多个菜单中
        public ICollection<Menu_Dish> MenuDishes { get; set; } = new List<Menu_Dish>();

        // 便捷属性：获取包含此菜品的菜单
        [NotMapped]
        public IEnumerable<Menu> Menus => MenuDishes.Select(md => md.Menu);
    }
}
