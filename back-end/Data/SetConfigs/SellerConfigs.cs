using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> entity)
        {
            entity.ToTable("Seller");
            entity.HasKey(s => s.UserID);
            entity.Property(s => s.SellerRegistrationTime).IsRequired();
            entity.Property(s => s.ReputationPoints).HasDefaultValue(0);
            entity.Property(s => s.BanStatus).IsRequired().HasMaxLength(10).HasDefaultValue("Normal");
            entity.HasOne(s => s.User).WithOne().HasForeignKey<Seller>(s => s.UserID);
            entity.HasOne(s => s.AfterSaleApplication)
                   .WithMany()
                   .HasForeignKey(s => s.AfterSaleApplicationID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}