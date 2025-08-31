using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class SuperviseConfig : IEntityTypeConfiguration<Supervise_>
    {
        public void Configure(EntityTypeBuilder<Supervise_> entity)
        {
            entity.ToTable("Supervise");
            entity.HasKey(s => new { s.AdminID, s.PenaltyID });
            entity.HasOne(s => s.Admin)
                   .WithMany()
                   .HasForeignKey(s => s.AdminID);
            entity.HasOne(s => s.Penalty)
                   .WithMany()
                   .HasForeignKey(s => s.PenaltyID);
        }
    }
}