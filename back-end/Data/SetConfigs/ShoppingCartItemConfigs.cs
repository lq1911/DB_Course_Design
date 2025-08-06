using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class ShoppingCartItemConfig : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.ToTable("SHOPPINGCARTITEM");

            // --- 主键和基础属性配置 ---
            builder.HasKey(sci => sci.ItemID);
            builder.Property(sci => sci.ItemID).HasColumnName("ITEMID").ValueGeneratedOnAdd();

            builder.Property(sci => sci.Quantity).HasColumnName("QUANTITY").IsRequired();
            builder.Property(sci => sci.TotalPrice).HasColumnName("TOTALPRICE").HasColumnType("decimal(10,2)");

            // --- 外键属性声明 ---
            builder.Property(sci => sci.DishID).HasColumnName("DISHID").IsRequired();
            builder.Property(sci => sci.CartID).HasColumnName("CARTID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: ShoppingCartItem 与 Dish (多对一)
            // 多个购物车项可以指向同一个菜品
            builder.HasOne(sci => sci.Dish) // 一个购物车项只有一个菜品
                   .WithMany(d => d.ShoppingCartItems)
                   .HasForeignKey(sci => sci.DishID)
                   .OnDelete(DeleteBehavior.Restrict); // 防止菜品被删除，如果它还在某人的购物车里。

            // 关系二: ShoppingCartItem 与 ShoppingCart (多对一)
            // 多个购物车项属于同一个购物车
            builder.HasOne(sci => sci.Cart) // 一个购物车项只属于一个购物车
                   .WithMany(sc => sc.ShoppingCartItems) // 一个购物车拥有多个购物车项 (ShoppingCartItems 集合)
                   .HasForeignKey(sci => sci.CartID) // 外键是 CartID
                   .OnDelete(DeleteBehavior.Cascade); // 关键：当购物车被删除时，其所有项也应被级联删除。
        }
    }
}
