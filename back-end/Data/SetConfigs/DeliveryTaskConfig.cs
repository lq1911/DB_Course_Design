using BackEnd.Models;
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

            builder.Property(dt => dt.TaskID).HasColumnName("TASKID");
            
            builder.Property(dt => dt.EstimatedArrivalTime).HasColumnName("ESTIMATEDARRIVALTIME").IsRequired();
            
            builder.Property(dt => dt.EstimatedDeliveryTime).HasColumnName("ESTIMATEDDELIVERYTIME").IsRequired();
            
            builder.Property(dt => dt.CourierLongitude).HasColumnName("COURIERLONGITUDE").HasColumnType("decimal(10,6)");
            
            builder.Property(dt => dt.CourierLatitude).HasColumnName("COURIERLATITUDE").HasColumnType("decimal(10,6)");
            
            builder.Property(dt => dt.CustomerID).HasColumnName("CUSTOMERID").IsRequired();
            
            builder.Property(dt => dt.StoreID).HasColumnName("STOREID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: DeliveryTask -> Customer (多对一)
            // 由于 Customer 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(dt => dt.Customer)
                   .WithMany()
                   .HasForeignKey(dt => dt.CustomerID);

            // 关系二: DeliveryTask -> Store (多对一)
            // 由于 Store 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(dt => dt.Store)
                   .WithMany()
                   .HasForeignKey(dt => dt.StoreID);
        }
    }
}