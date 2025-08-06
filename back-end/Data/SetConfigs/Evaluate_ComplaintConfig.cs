using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class Evaluate_ComplaintConfig : IEntityTypeConfiguration<Evaluate_Complaint>
    {
        public void Configure(EntityTypeBuilder<Evaluate_Complaint> builder)
        {
            builder.ToTable("EVALUATE_COMPLAINT");

            // 设置复合主键
            builder.HasKey(ec => new { ec.AdminID, ec.ComplaintID });

            // 配置列属性和名称
            builder.Property(ec => ec.AdminID).HasColumnName("ADMINID");

            builder.Property(ec => ec.ComplaintID).HasColumnName("COMPLAINTID");

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Evaluate_Complaint -> Administrator (多对一)
            builder.HasOne(ec => ec.Admin)
                   .WithMany(a => a.EvaluateComplaints)
                   .HasForeignKey(ec => ec.AdminID)
                   .OnDelete(DeleteBehavior.Restrict); // 防止直接通过中间实体删除主实体


            // 关系二: Evaluate_Complaint -> DeliveryComplaint (多对一)
            builder.HasOne(ec => ec.Complaint)
                   .WithMany(dc => dc.EvaluateComplaints)
                   .HasForeignKey(ec => ec.ComplaintID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}