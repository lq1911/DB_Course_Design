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

            // 配置一对一关系: Administrator -> User
            builder.HasOne(a => a.User)
                   .WithOne()
                   .HasForeignKey<Administrator>(a => a.UserID);

            // 配置多对多关系: Administrator -> Review_Comment
            builder.HasMany(a => a.ReviewComments)
                   .WithOne(rc => rc.Admin)
                   .HasForeignKey(rc => rc.AdminID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 配置多对多关系: Administrator -> Supervise_
            builder.HasMany(a => a.Supervise_s)
                   .WithOne(s => s.Admin)
                   .HasForeignKey(s => s.AdminID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 配置多对多关系: Administrator -> Evaluate_AfterSale
            builder.HasMany(a => a.EvaluateAfterSales)
                   .WithOne(eas => eas.Admin)
                   .HasForeignKey(eas => eas.AdminID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 配置多对多关系: Administrator -> Evaluate_Complaint
            builder.HasMany(a => a.EvaluateComplaints)
                   .WithOne(ec => ec.Admin)
                   .HasForeignKey(ec => ec.AdminID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 忽略不映射到数据库的便捷属性
            builder.Ignore(a => a.Comments);
            builder.Ignore(a => a.Penalties);
            builder.Ignore(a => a.Applications);
            builder.Ignore(a => a.DeliveryComplaints);
        }
    }
}