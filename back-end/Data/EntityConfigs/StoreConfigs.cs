using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("STORES");

            // --- 主键和基础属性配置 ---
            builder.HasKey(s => s.StoreID);
            builder.Property(s => s.StoreID).HasColumnName("STOREID").ValueGeneratedOnAdd();

            builder.Property(s => s.StoreName).HasColumnName("STORENAME").IsRequired().HasMaxLength(50);

            builder.Property(s => s.StoreAddress).HasColumnName("STOREADDRESS").IsRequired().HasMaxLength(100);

            builder.Property(s => s.OpenTime).HasColumnName("OPENTIME").IsRequired();

            builder.Property(s => s.CloseTime).HasColumnName("CLOSETIME").IsRequired();

            builder.Property(s => s.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);

            builder.Property(s => s.MonthlySales).HasColumnName("MONTHLYSALES").IsRequired();

            builder.Property(s => s.StoreFeatures).HasColumnName("STOREFEATURES").IsRequired().HasMaxLength(500);

            builder.Property(s => s.StoreCreationTime).HasColumnName("STORECREATIONTIME").IsRequired();

            builder.Property(s => s.StoreState).HasColumnName("STORESTATE").IsRequired().HasConversion<string>().HasMaxLength(20);

            builder.Property(s => s.StoreCategory).HasColumnName("STORECATEGORY").IsRequired().HasMaxLength(20);

            builder.Property(s => s.StoreImage).HasColumnName("STOREIMAGE").HasMaxLength(500).IsRequired(false);

            // 忽略不映射到数据库的属性
            builder.Ignore(s => s.IsOpen);
            builder.Ignore(s => s.BusinessHoursDisplay);

            // --- 外键属性声明 ---
            builder.Property(s => s.SellerID).HasColumnName("SELLERID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: Store -> Seller (一对一)
            builder.HasOne(s => s.Seller)
                   .WithOne(seller => seller.Store)
                   .HasForeignKey<Store>(s => s.SellerID)
                   .OnDelete(DeleteBehavior.Cascade); // 当商家被删除时，其拥有的店铺也应被级联删除，以保证数据一致性。
        }
    }
}
