using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class Publish_TaskConfig : IEntityTypeConfiguration<Publish_Task>
    {
        public void Configure(EntityTypeBuilder<Publish_Task> entity)
        {
            entity.ToTable("PUBLISH_TASK");

            entity.HasKey(e => new { e.SellerID, e.DeliveryTaskID });
            entity.Property(e => e.SellerID).HasColumnName("SELLERID");
            entity.Property(e => e.DeliveryTaskID).HasColumnName("DELIVERYTASKID");
            entity.Property(e => e.PublishTime).HasColumnName("PUBLISHTIME").IsRequired();

            entity.HasOne(e => e.Seller)
                .WithMany()
                .HasForeignKey(e => e.SellerID);

            entity.HasOne(e => e.DeliveryTask)
                .WithMany()
                .HasForeignKey(e => e.DeliveryTaskID);
        }
    }
}