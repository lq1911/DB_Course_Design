using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class StoreViolationPenaltyConfig : IEntityTypeConfiguration<StoreViolationPenalty>
    {
        public void Configure(EntityTypeBuilder<StoreViolationPenalty> entity)
        {
            entity.ToTable("STOREVIOLATIONPENALTY");

            entity.HasKey(e => e.PenaltyID);
            entity.Property(e => e.PenaltyID).HasColumnName("PENALTYID");
            entity.Property(e => e.PenaltyReason).HasColumnName("PENALTYREASON").IsRequired().HasMaxLength(255);
            entity.Property(e => e.PenaltyTime).HasColumnName("PENALTYTIME").IsRequired();
            entity.Property(e => e.SellerPenalty).HasColumnName("SELLERPENALTY").HasMaxLength(50);
            entity.Property(e => e.StorePenalty).HasColumnName("STOREPENALTY").HasMaxLength(50);
            entity.Property(e => e.StoreID).HasColumnName("STOREID").IsRequired();

            entity.HasOne(e => e.Store)
                .WithMany()
                .HasForeignKey(e => e.StoreID);
        }
    }
}