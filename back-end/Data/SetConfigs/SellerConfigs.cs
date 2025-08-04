using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> entity)
        {

            entity.ToTable("SELLER");

            entity.HasKey(e => e.UserID);
            entity.Property(e => e.UserID).HasColumnName("USERID");
            entity.Property(e => e.SellerRegistrationTime).HasColumnName("SELLERREGISTRATIONTIME").IsRequired();
            entity.Property(e => e.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);
            entity.Property(e => e.BanStatus).HasColumnName("BANSTATUS").IsRequired().HasMaxLength(10);
            entity.Property(e => e.AfterSaleApplicationID).HasColumnName("AFTERSALEAPPLICATIONID");

            entity.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Seller>(e => e.UserID);

            entity.HasOne(e => e.AfterSaleApplication)
                .WithMany()
                .HasForeignKey(e => e.AfterSaleApplicationID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}