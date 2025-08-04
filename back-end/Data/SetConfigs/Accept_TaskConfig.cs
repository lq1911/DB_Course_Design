using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class Accept_TaskConfig : IEntityTypeConfiguration<Accept_Task>
    {
        public void Configure(EntityTypeBuilder<Accept_Task> builder)
        {
            builder.ToTable("ACCEPT_TASK");

            builder.HasKey(at => new { at.CourierID, at.DeliveryTaskID });

            builder.Property(at => at.CourierID).HasColumnName("COURIERID");
            
            builder.Property(at => at.DeliveryTaskID).HasColumnName("DELIVERYTASKID");
            
            builder.Property(at => at.AcceptTime).HasColumnName("ACCEPTTIME").IsRequired();

            // 关系一：Accept_Task -> Courier (多对一)
            // 由于 Courier 类中没有反向导航属性 ICollection<Accept_Task>，我们使用不带参数的 WithMany()
            builder.HasOne(at => at.Courier)
                   .WithMany()
                   .HasForeignKey(at => at.CourierID);

            // 关系二：Accept_Task -> DeliveryTask (多对一)
            // 由于 DeliveryTask 类中没有反向导航属性 ICollection<Accept_Task>，我们使用不带参数的 WithMany()
            builder.HasOne(at => at.DeliveryTask)
                   .WithMany()
                   .HasForeignKey(at => at.DeliveryTaskID);
        }
    }
}