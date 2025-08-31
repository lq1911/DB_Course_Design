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
            entity.Property(s => s.StoreName).HasColumnName("STORENAME").IsRequired().HasMaxLength(100);
            entity.Property(s => s.StoreAddress).HasColumnName("STOREADDRESS").IsRequired().HasMaxLength(200);
            
            // 营业时间列 - 处理Oracle的INTERVAL类型
            entity.Property(s => s.OpenTime).HasColumnName("OPENTIME").HasColumnType("INTERVAL DAY(8) TO SECOND(7)");
            entity.Property(s => s.CloseTime).HasColumnName("CLOSETIME").HasColumnType("INTERVAL DAY(8) TO SECOND(7)");
            
            entity.Property(s => s.StoreCreationTime).HasColumnName("STORECREATIONTIME").IsRequired();
            
            entity.Property(s => s.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
            entity.Property(s => s.MonthlySales).HasColumnName("MONTHLYSALES").IsRequired();
            entity.Property(s => s.StoreFeatures).HasColumnName("STOREFEATURES").HasMaxLength(1000);
            
            // 新增的列
            entity.Property(s => s.StoreState).HasColumnName("STORESTATE").HasMaxLength(40);
            entity.Property(s => s.StoreCategory).HasColumnName("STORECATEGORY").HasMaxLength(40);
            entity.Property(s => s.StoreImage).HasColumnName("STOREIMAGE").HasMaxLength(1000);
            
            entity.Property(s => s.SellerID).HasColumnName("SELLERID").IsRequired();
            
            entity.HasOne(s => s.Seller)
                   .WithMany()
                   .HasForeignKey(s => s.SellerID);
        }
    }
}