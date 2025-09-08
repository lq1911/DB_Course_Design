using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class ShoppingCart
    {
        // 购物车类
        // 主码：CartID
        // 外码：OrderID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        [Required]
        public DateTime LastUpdatedTime { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; } = 0.00m;

        public ShoppingCartState? ShoppingCartState { get; set; }

        public FoodOrder? Order { get; set; }

        // 新增：直接关联店铺ID，便于快速查询
        public int? StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store? Store { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        // 一对多导航属性
        // 购物车项
        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
