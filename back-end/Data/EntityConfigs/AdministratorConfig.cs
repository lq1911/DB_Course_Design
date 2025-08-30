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

            builder.Property(a => a.UserID).HasColumnName("USERID").ValueGeneratedNever();

            builder.Property(a => a.AdminRegistrationTime).HasColumnName("ADMINREGISTRATIONTIME").IsRequired();

            builder.Property(a => a.ManagedEntities).HasColumnName("MANAGEDENTITIES").IsRequired().HasMaxLength(50);

            builder.Property(a => a.IssueHandlingScore).HasColumnName("ISSUEHANDLINGSCORE").HasColumnType("decimal(3,2)").HasDefaultValue(0.00m);

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Administrator -> User (一对一)
            builder.HasOne(a => a.User)
                   .WithOne()
                   .HasForeignKey<Administrator>(a => a.UserID)
                   .OnDelete(DeleteBehavior.Cascade); // 当User被删除时，关联的Administrator也应被删除

            // 忽略不映射到数据库的便捷属性
            builder.Ignore(a => a.Comments);
            builder.Ignore(a => a.Penalties);
            builder.Ignore(a => a.Applications);
            builder.Ignore(a => a.DeliveryComplaints);
        }
    }
}