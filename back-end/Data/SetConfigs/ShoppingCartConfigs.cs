using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("SHOPPINGCART");

            builder.HasKey(sc => sc.CartID);

            builder.Property(sc => sc.CartID).HasColumnName("CARTID").ValueGeneratedOnAdd();

            builder.Property(sc => sc.LastUpdatedTime).HasColumnName("LASTUPDATEDTIME").IsRequired();

            builder.Property(sc => sc.TotalPrice).HasColumnName("TOTALPRICE").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);

            builder.Property(sc => sc.OrderID).HasColumnName("ORDERID");

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: ShoppingCart 与 FoodOrder (一对一，可选关系)
            builder.HasOne(sc => sc.Order)
                   .WithOne(o => o.Cart)
                   .HasForeignKey<ShoppingCart>(sc => sc.OrderID)
                   .OnDelete(DeleteBehavior.SetNull); // 如果订单被删除，仅将购物车的 OrderID 设为 NULL，不删除购物车本身。

            // 关系二: ShoppingCart 与 ShoppingCartItem (一对多)
            // 一个购物车包含多个购物车项。
            builder.HasMany(cart => cart.ShoppingCartItems) // 一个购物车有多个 ShoppingCartItems
                   .WithOne(sci => sci.Cart)
                   .HasForeignKey(sci => sci.Cart)
                   .OnDelete(DeleteBehavior.Cascade); // 当购物车被删除时，其包含的所有项也应被级联删除，以保持数据干净。
        }
    }
}