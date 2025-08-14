using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Menu_Dish
    {
        // 菜单与菜品之间的包含关系
        // 主码：MenuID，DishID

        [Key, Column(Order = 0)]
        public int MenuID { get; set; }
        [ForeignKey("MenuID")]
        public Menu Menu { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int DishID { get; set; }
        [ForeignKey("DishID")]
        public Dish Dish { get; set; } = null!;
    }
}
