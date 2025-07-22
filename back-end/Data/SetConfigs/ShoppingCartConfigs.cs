using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> entity)
        {
            entity.ToTable("ShoppingCart");
            entity.HasKey(c => c.CartID);
            entity.Property(c => c.LastUpdatedTime).IsRequired();
            entity.Property(c => c.TotalPrice).HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        }
    }
}