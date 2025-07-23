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
            // 由于 Administrator 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(ec => ec.Admin)
                   .WithMany()
                   .HasForeignKey(ec => ec.AdminID);

            // 关系二: Evaluate_Complaint -> DeliveryComplaint (多对一)
            // 由于 DeliveryComplaint 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(ec => ec.Complaint)
                   .WithMany()
                   .HasForeignKey(ec => ec.ComplaintID);
        }
    }
}