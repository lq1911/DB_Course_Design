using BackEnd.Models;
using BackEnd.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class DeliveryTaskConfig : IEntityTypeConfiguration<DeliveryTask>
    {
        public void Configure(EntityTypeBuilder<DeliveryTask> builder)
        {
            builder.ToTable("DELIVERY_TASKS");

            builder.HasKey(dt => dt.TaskID);

            builder.Property(dt => dt.TaskID).HasColumnName("TASKID").ValueGeneratedOnAdd();

            builder.Property(dt => dt.EstimatedArrivalTime).HasColumnName("ESTIMATEDARRIVALTIME").IsRequired();

            builder.Property(dt => dt.EstimatedDeliveryTime).HasColumnName("ESTIMATEDDELIVERYTIME").IsRequired();

            // [新增配置]
            builder.Property(dt => dt.PublishTime)
                   .HasColumnName("PUBLISHTIME")
                   .IsRequired();

            // [新增配置]
            builder.Property(dt => dt.AcceptTime)
                   .HasColumnName("ACCEPTTIME")
                   .IsRequired();

            builder.Property(dt => dt.Status)
                  .HasColumnName("STATUS")
                  .IsRequired()
                  .HasConversion<string>() // 将枚举存储为字符串
                  .HasMaxLength(20)
                  .HasDefaultValue(DeliveryStatus.Pending);

            builder.Property(dt => dt.CompletionTime).HasColumnName("COMPLETIONTIME").IsRequired(false);

            builder.Property(dt => dt.DeliveryFee).HasColumnName("DELIVERYFEE").HasColumnType("decimal(5,2)").IsRequired().HasDefaultValue(0.00m);

            builder.Property(dt => dt.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            builder.Property(dt => dt.StoreID).HasColumnName("STOREID").IsRequired();

            builder.Property(dt => dt.CourierID).HasColumnName("COURIERID").IsRequired();
            builder.Property(dt => dt.OrderID).HasColumnName("ORDERID").IsRequired();


            // ---------------------------------------------------------------
                     // 配置外键关系
                     // ---------------------------------------------------------------

            // 关系一: DeliveryTask -> Customer (多对一)
            // Customer 类中有 DeliveryTasks 导航属性
           builder.HasOne(dt => dt.Customer)
                   .WithMany(c => c.DeliveryTasks)
                   .HasForeignKey(dt => dt.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系二: DeliveryTask -> Store (多对一)
            builder.HasOne(dt => dt.Store)
                   .WithMany(s => s.DeliveryTasks)
                   .HasForeignKey(dt => dt.StoreID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系三: DeliveryTask -> Courier (多对一)
            builder.HasOne(dt => dt.Courier)
                   .WithMany(c => c.DeliveryTasks)
                   .HasForeignKey(dt => dt.CourierID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系四: DeliveryTask -> FoodOrder (一对一)
            builder.HasOne(dt => dt.Order)
                   .WithOne(fd => fd.DeliveryTask)
                   .HasForeignKey<DeliveryTask>(dt => dt.OrderID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}