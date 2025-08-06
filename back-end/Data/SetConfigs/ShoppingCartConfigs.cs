using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> entity)
        {
            entity.ToTable("SHOPPINGCART");

            entity.HasKey(e => e.CartID);
            entity.Property(e => e.CartID).HasColumnName("CARTID");
            entity.Property(e => e.LastUpdatedTime).HasColumnName("LASTUPDATEDTIME").IsRequired();
            entity.Property(e => e.TotalPrice).HasColumnName("TOTALPRICE").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        }
    }
}