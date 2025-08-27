using BackEnd.Models;
using BackEnd.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CUSTOMERS");

            builder.HasKey(c => c.UserID);

            builder.Property(c => c.UserID).HasColumnName("USERID").ValueGeneratedNever();

            builder.Property(c => c.DefaultAddress).HasColumnName("DEFAULTADDRESS").HasMaxLength(100);

            builder.Property(c => c.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);

            builder.Property(c => c.IsMember)
                    .HasColumnName("ISMEMBER")
                    .HasConversion<string>() // 将枚举存储为字符串（如 "NotMember", "Active"），更具可读性
                    .HasMaxLength(20) // 确保字符串长度足够
                    .HasDefaultValue(MembershipStatus.NotMember);

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Customer -> User (一对一)
            builder.HasOne(c => c.User)
                   .WithOne(u => u.Customer)
                   .HasForeignKey<Customer>(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}