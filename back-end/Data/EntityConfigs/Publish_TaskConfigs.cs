using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class PublishTaskConfig : IEntityTypeConfiguration<Publish_Task>
    {
        public void Configure(EntityTypeBuilder<Publish_Task> entity)
        {
            entity.ToTable("PublishTask");
            entity.HasKey(pt => new { pt.SellerID, pt.DeliveryTaskID });
            entity.Property(pt => pt.PublishTime).IsRequired();
            entity.HasOne(pt => pt.Seller)
                   .WithMany()
                   .HasForeignKey(pt => pt.SellerID);
            entity.HasOne(pt => pt.DeliveryTask)
                   .WithMany()
                   .HasForeignKey(pt => pt.DeliveryTaskID);
        }
    }
}