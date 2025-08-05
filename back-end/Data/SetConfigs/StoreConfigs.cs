using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.ToTable("STORE");

            entity.HasKey(e => e.StoreID);
            entity.Property(e => e.StoreID).HasColumnName("STOREID");
            entity.Property(e => e.StoreName).HasColumnName("STORENAME").IsRequired().HasMaxLength(50);
            entity.Property(e => e.StoreAddress).HasColumnName("STOREADDRESS").IsRequired().HasMaxLength(100);
            entity.Property(e => e.BusinessHours).HasColumnName("BUSINESSHOURS").IsRequired();
            entity.Property(e => e.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.MonthlySales).HasColumnName("MONTHLYSALES").IsRequired();
            entity.Property(e => e.StoreFeatures).HasColumnName("STOREFEATURES").IsRequired().HasMaxLength(500);
            entity.Property(e => e.StoreCreationTime).HasColumnName("STORECREATIONTIME").IsRequired();
            entity.Property(e => e.SellerID).HasColumnName("SELLERID").IsRequired();

            entity.HasOne(e => e.Seller)
                .WithMany()
                .HasForeignKey(e => e.SellerID);
        }
    }
}