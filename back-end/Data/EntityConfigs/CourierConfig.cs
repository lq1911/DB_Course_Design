using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class CourierConfig : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.ToTable("COURIERS");

            builder.HasKey(c => c.UserID);

            builder.Property(c => c.UserID).HasColumnName("USERID").ValueGeneratedNever(); // 明确主键值不是由数据库生成的

            builder.Property(c => c.CourierRegistrationTime).HasColumnName("COURIERREGISTRATIONTIME").IsRequired();

            builder.Property(c => c.VehicleType).HasColumnName("VEHICLETYPE").IsRequired().HasMaxLength(20);

            builder.Property(c => c.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);

            builder.Property(c => c.TotalDeliveries).HasColumnName("TOTALDELIVERIES").HasDefaultValue(0);

            builder.Property(c => c.AvgDeliveryTime).HasColumnName("AVGDELIVERYTIME").HasDefaultValue(0);

            builder.Property(c => c.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(3,2)").HasDefaultValue(0.00m);

            builder.Property(c => c.MonthlySalary).HasColumnName("MONTHLYSALARY").HasDefaultValue(0);

            // 新增骑手属性配置
            builder.Property(c => c.IsOnline).HasColumnName("ISONLINE").IsRequired().HasConversion<string>().HasMaxLength(10);

            builder.Property(c => c.CourierLongitude).HasColumnName("COURIERLONGITUDE").HasColumnType("decimal(10,6)").IsRequired(false);

            builder.Property(c => c.CourierLatitude).HasColumnName("COURIERLATITUDE").HasColumnType("decimal(10,6)").IsRequired(false);

            builder.Property(c => c.LastOnlineTime).HasColumnName("LASTONLINETIME").IsRequired(false);

            builder.Property(c => c.CommissionThisMonth)
                   .HasColumnName("COMMISSIONTHISMONTH") // <-- 保证列名全大写
                   .HasColumnType("decimal(10,2)");       // <-- 精确控制类型

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Courier -> User (一对一)
            builder.HasOne(c => c.User)
                   .WithOne(u => u.Courier)
                   .HasForeignKey<Courier>(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade); // 当User被删除时，关联的Courier也应被删除
        }
    }
}