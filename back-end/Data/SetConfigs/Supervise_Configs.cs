using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class Supervise_Config : IEntityTypeConfiguration<Supervise_>
    {
        public void Configure(EntityTypeBuilder<Supervise_> entity)
        {
            entity.ToTable("SUPERVISE");

            entity.HasKey(e => new { e.AdminID, e.PenaltyID });
            entity.Property(e => e.AdminID).HasColumnName("ADMINID");
            entity.Property(e => e.PenaltyID).HasColumnName("PENALTYID");

            entity.HasOne(e => e.Admin)
                .WithMany()
                .HasForeignKey(e => e.AdminID);

            entity.HasOne(e => e.Penalty)
                .WithMany()
                .HasForeignKey(e => e.PenaltyID);
        }
    }
}