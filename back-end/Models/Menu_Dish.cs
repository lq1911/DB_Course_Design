using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
   public class Menu_Dish
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }
        [ForeignKey("MenuID")]
        public Menu Menu { get; set; }

        [Key, Column(Order = 1)]
        public int DishID { get; set; }
        [ForeignKey("DishID")]
        public Dish Dish { get; set; }
    }
}
