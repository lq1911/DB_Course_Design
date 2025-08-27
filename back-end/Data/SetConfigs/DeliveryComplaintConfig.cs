using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class DeliveryComplaintConfig : IEntityTypeConfiguration<DeliveryComplaint>
    {
        public void Configure(EntityTypeBuilder<DeliveryComplaint> builder)
        {
            builder.ToTable("DELIVERY_COMPLAINTS");

            builder.HasKey(dc => dc.ComplaintID);

            builder.Property(dc => dc.ComplaintID).HasColumnName("COMPLAINTID").ValueGeneratedOnAdd();

            builder.Property(dc => dc.ComplaintReason).HasColumnName("COMPLAINTREASON").IsRequired().HasMaxLength(255);

            builder.Property(dc => dc.ComplaintTime).HasColumnName("COMPLAINTTIME").IsRequired();

            builder.Property(dc => dc.CourierID).HasColumnName("COURIERID").IsRequired();

            builder.Property(dc => dc.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            builder.Property(dc => dc.DeliveryTaskID).HasColumnName("DELIVERYTASKID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: DeliveryComplaint -> DeliveryTask (多对一)
            builder.HasOne(dc => dc.DeliveryTask)
                   .WithMany(dt => dt.DeliveryComplaints)
                   .HasForeignKey(dc => dc.DeliveryTaskID)
                   .OnDelete(DeleteBehavior.SetNull); // 需要保存投诉记录
        }
    }
}