using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class StoreViolationPenaltyConfig : IEntityTypeConfiguration<StoreViolationPenalty>
    {
        public void Configure(EntityTypeBuilder<StoreViolationPenalty> entity)
        {
            entity.ToTable("StoreViolationPenalty");
            entity.HasKey(p => p.PenaltyID);
            entity.Property(p => p.PenaltyReason).IsRequired().HasMaxLength(255);
            entity.Property(p => p.PenaltyTime).IsRequired();
            entity.Property(p => p.SellerPenalty).HasMaxLength(50);
            entity.Property(p => p.StorePenalty).HasMaxLength(50);
            entity.HasOne(p => p.Store)
                   .WithMany()
                   .HasForeignKey(p => p.StoreID);
        }
    }
}