using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("SHOPPING_CARTS");

            builder.HasKey(sc => sc.CartID);

            builder.Property(sc => sc.CartID).HasColumnName("CARTID").ValueGeneratedOnAdd();

            builder.Property(sc => sc.LastUpdatedTime).HasColumnName("LASTUPDATEDTIME").IsRequired();

            builder.Property(sc => sc.TotalPrice).HasColumnName("TOTALPRICE").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);

            builder.Property(sc => sc.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: ShoppingCart -> Customer (多对一)
            builder.HasOne(sc => sc.Customer)
                   .WithMany(cu => cu.ShoppingCarts)
                   .HasForeignKey(sc => sc.CustomerID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}