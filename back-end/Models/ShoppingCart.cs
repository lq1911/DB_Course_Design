using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // 是否锁定（下单后不可再修改）
        [Required]
        public bool IsLocked { get; set; } = false;

        public FoodOrder? Order { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } = null!;

        // 一对多导航属性
        // 购物车项
        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
