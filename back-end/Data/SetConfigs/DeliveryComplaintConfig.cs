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

            // 关系一: DeliveryComplaint <-> DeliveryTask (多对一)
            builder.HasOne(dc => dc.DeliveryTask)
                   .WithMany(dt => dt.DeliveryComplaints)
                   .HasForeignKey(dc => dc.DeliveryTaskID)
                   .OnDelete(DeleteBehavior.Cascade); // 如果配送任务记录被删除，相关的投诉也应被删除

            // 关系二: 与 Administrator 的多对多关系 (通过 Evaluate_Complaint)
            // 投诉记录被删除时，相关的处理记录也应被级联删除
            builder.HasMany(dc => dc.EvaluateComplaints)
                   .WithOne(ec => ec.Complaint) // 假设 Evaluate_Complaint 中有 Complaint 导航属性
                   .HasForeignKey(ec => ec.ComplaintID) // 假设 Evaluate_Complaint 中有 ComplaintID 外键
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}