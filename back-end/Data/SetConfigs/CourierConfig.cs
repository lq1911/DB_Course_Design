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

            builder.Property(c => c.UserID).HasColumnName("USERID");

            builder.Property(c => c.CourierRegistrationTime).HasColumnName("COURIERREGISTRATIONTIME").IsRequired();
            
            builder.Property(c => c.VehicleType).HasColumnName("VEHICLETYPE").IsRequired().HasMaxLength(20);
            
            builder.Property(c => c.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);
            
            builder.Property(c => c.TotalDeliveries).HasColumnName("TOTALDELIVERIES").HasDefaultValue(0);
            
            builder.Property(c => c.AvgDeliveryTime).HasColumnName("AVGDELIVERYTIME").HasDefaultValue(0);
            
            builder.Property(c => c.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(3,2)").HasDefaultValue(0.00m);
            
            builder.Property(c => c.MonthlySalary).HasColumnName("MONTHLYSALARY").HasDefaultValue(0);

            // 配置一对一关系: Courier -> User
            // 由于 User 类中没有反向导航属性 Courier，我们使用不带参数的 WithOne()
            builder.HasOne(c => c.User)
                   .WithOne() // 明确表示 User 端没有 Courier 导航属性
                   .HasForeignKey<Courier>(c => c.UserID);
        }
    }
}