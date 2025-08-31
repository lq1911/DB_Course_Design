using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> entity)
        {
            entity.ToTable("SHOPPING_CARTS");
            entity.HasKey(c => c.CartID);
            
            // 映射列名到Oracle数据库中的大写列名
            entity.Property(c => c.CartID).HasColumnName("CARTID");
            
            // 移除DATE类型指定，让Entity Framework自动处理
            entity.Property(c => c.LastUpdatedTime).HasColumnName("LASTUPDATEDTIME").IsRequired();
            
            entity.Property(c => c.TotalPrice).HasColumnName("TOTALPRICE").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        }
    }
}