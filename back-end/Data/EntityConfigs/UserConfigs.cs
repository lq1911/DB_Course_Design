using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USERS");

            // --- 主键和基础属性配置 ---
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.UserID).HasColumnName("USERID").ValueGeneratedOnAdd();

            builder.Property(u => u.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15);

            builder.Property(u => u.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(64);

            // 修正：PhoneNumber 是 long 类型，不应有 MaxLength 限制。
            builder.Property(u => u.PhoneNumber).HasColumnName("PHONENUMBER").IsRequired();

            builder.Property(u => u.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(30);

            // 修正：与模型 [MaxLength(2)] 保持一致
            builder.Property(u => u.Gender).HasColumnName("GENDER").HasMaxLength(2);

            // 修正：与模型 [MaxLength(6)] 保持一致
            builder.Property(u => u.FullName).HasColumnName("FULLNAME").HasMaxLength(6);

            builder.Property(u => u.Avatar).HasColumnName("AVATAR").HasMaxLength(255);
            builder.Property(u => u.Birthday).HasColumnName("BIRTHDAY");
            builder.Property(u => u.AccountCreationTime).HasColumnName("ACCOUNTCREATIONTIME").IsRequired();

            // --- 枚举类型配置 ---
            // 将枚举存储为字符串，便于数据库阅读和调试
            builder.Property(u => u.IsProfilePublic).HasColumnName("ISPROFILEPUBLIC").IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(u => u.Role).HasColumnName("ROLE").IsRequired().HasConversion<string>().HasMaxLength(20);

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------
            
            // 配置一对一关系 - 这些关系不需要在USERS表中添加外键列
            // 因为Customer、Courier、Administrator、Seller表都使用UserID作为主键
            
            // 配置与Customer的一对一关系
            builder.HasOne(u => u.Customer)
                   .WithOne(c => c.User)
                   .HasForeignKey<Customer>(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 配置与Courier的一对一关系
            builder.HasOne(u => u.Courier)
                   .WithOne(c => c.User)
                   .HasForeignKey<Courier>(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 配置与Administrator的一对一关系
            builder.HasOne(u => u.Administrator)
                   .WithOne(a => a.User)
                   .HasForeignKey<Administrator>(a => a.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 配置与Seller的一对一关系
            builder.HasOne(u => u.Seller)
                   .WithOne(s => s.User)
                   .HasForeignKey<Seller>(s => s.UserID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
