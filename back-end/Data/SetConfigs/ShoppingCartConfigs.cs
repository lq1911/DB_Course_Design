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

            builder.Property(sc => sc.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: ShoppingCart 与 FoodOrder (一对一)
            builder.HasOne(sc => sc.Order)
                   .WithOne(fo => fo.Cart)
                   .HasForeignKey<FoodOrder>(fo => fo.CartID) // 外键在 FoodOrder 表中
                   .OnDelete(DeleteBehavior.SetNull); // 购物车删除时，订单保留但 CartID 设为 null

            // 关系二: ShoppingCart 与 ShoppingCartItem (一对多)
            // 一个购物车包含多个购物车项。
            builder.HasMany(sc => sc.ShoppingCartItems) // 一个购物车有多个 ShoppingCartItems
                   .WithOne(sci => sci.Cart)
                   .HasForeignKey(sci => sci.CartID)
                   .OnDelete(DeleteBehavior.Cascade); // 当购物车被删除时，其包含的所有项也应被级联删除，以保持数据干净。

            // 关系三: ShoppingCart 与 Customer (多对一)
            builder.HasOne(sc => sc.Customer)
                   .WithMany(cu => cu.ShoppingCarts)
                   .HasForeignKey(sc => sc.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict); // 防止删除有优惠券的客户
        }
    }
}