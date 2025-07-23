using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class AdministratorConfig : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.ToTable("ADMINISTRATORS");

            builder.HasKey(a => a.UserID);

            builder.Property(a => a.UserID).HasColumnName("USERID");
            
            builder.Property(a => a.AdminRegistrationTime).HasColumnName("ADMINREGISTRATIONTIME").IsRequired();
            
            builder.Property(a => a.ManagedEntities).HasColumnName("MANAGEDENTITIES").IsRequired().HasMaxLength(50);
            
            builder.Property(a => a.IssueHandlingScore).HasColumnName("ISSUEHANDLINGSCORE").HasColumnType("decimal(3,2)").HasDefaultValue(0.00m);

            // 配置一对一关系: Administrator -> User
            // 每个 Administrator 必须对应一个 User
            // 由于 User 类中没有反向导航属性 Administrator，我们使用不带参数的 WithOne()
            builder.HasOne(a => a.User)
                   .WithOne() // 表示关系的另一端(User)没有显式的导航属性指向回来
                   .HasForeignKey<Administrator>(a => a.UserID);
        }
    }
}