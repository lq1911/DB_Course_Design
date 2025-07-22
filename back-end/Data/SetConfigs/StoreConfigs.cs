using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.ToTable("Store");
            entity.HasKey(s => s.StoreID);
            entity.Property(s => s.StoreName).IsRequired().HasMaxLength(50);
            entity.Property(s => s.StoreAddress).IsRequired().HasMaxLength(100);
            entity.Property(s => s.BusinessHours).IsRequired();
            entity.Property(s => s.AverageRating).HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
            entity.Property(s => s.MonthlySales).IsRequired();
            entity.Property(s => s.StoreFeatures).IsRequired().HasMaxLength(500);
            entity.Property(s => s.StoreCreationTime).IsRequired();
            entity.HasOne(s => s.Seller)
                   .WithMany()
                   .HasForeignKey(s => s.SellerID);
        }
    }
}