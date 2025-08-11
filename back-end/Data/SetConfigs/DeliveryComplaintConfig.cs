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

            builder.Property(dc => dc.ComplaintID).HasColumnName("COMPLAINTID");

            builder.Property(dc => dc.ComplaintReason).HasColumnName("COMPLAINTREASON").IsRequired().HasMaxLength(255);
            
            builder.Property(dc => dc.ComplaintTime).HasColumnName("COMPLAINTTIME").IsRequired();
            
            builder.Property(dc => dc.CourierID).HasColumnName("COURIERID").IsRequired();
            
            builder.Property(dc => dc.CustomerID).HasColumnName("CUSTOMERID").IsRequired();
            
            builder.Property(dc => dc.DeliveryTaskID).HasColumnName("DELIVERYTASKID").IsRequired();
            
            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------
            
            // 关系一: DeliveryComplaint -> Courier (多对一)
            // 由于 Courier 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(dc => dc.Courier)
                   .WithMany()
                   .HasForeignKey(dc => dc.CourierID);

            // 关系二: DeliveryComplaint -> Customer (多对一)
            // 由于 Customer 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(dc => dc.Customer)
                   .WithMany()
                   .HasForeignKey(dc => dc.CustomerID);

            // 关系三: DeliveryComplaint -> DeliveryTask (多对一)
            // 由于 DeliveryTask 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(dc => dc.DeliveryTask)
                   .WithMany()
                   .HasForeignKey(dc => dc.DeliveryTaskID);
        }
    }
}