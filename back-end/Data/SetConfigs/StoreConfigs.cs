using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.ToTable("STORES");
            entity.HasKey(s => s.StoreID);
            
            // 映射列名到Oracle数据库中的大写列名
            entity.Property(s => s.StoreID).HasColumnName("STOREID");
            entity.Property(s => s.StoreName).HasColumnName("STORENAME").IsRequired().HasMaxLength(50);
            entity.Property(s => s.StoreAddress).HasColumnName("STOREADDRESS").IsRequired().HasMaxLength(100);
            
            // 移除DATE类型指定，让Entity Framework自动处理
            entity.Property(s => s.BusinessHours).HasColumnName("BUSINESSHOURS").IsRequired();
            entity.Property(s => s.StoreCreationTime).HasColumnName("STORECREATIONTIME").IsRequired();
            
            entity.Property(s => s.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
            entity.Property(s => s.MonthlySales).HasColumnName("MONTHLYSALES").IsRequired();
            entity.Property(s => s.StoreFeatures).HasColumnName("STOREFEATURES").IsRequired().HasMaxLength(500);
            entity.Property(s => s.SellerID).HasColumnName("SELLERID").IsRequired();
            
            entity.HasOne(s => s.Seller)
                   .WithMany()
                   .HasForeignKey(s => s.SellerID);
        }
    }
}