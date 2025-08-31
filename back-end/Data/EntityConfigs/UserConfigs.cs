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

            builder.Property(u => u.PhoneNumber)
                   .HasColumnName("PHONENUMBER")
                   .IsRequired()
                   .HasMaxLength(20);

            // 在 UserConfig.cs 的 Configure 方法中添加
            builder.HasOne(u => u.Administrator)
                   .WithOne(a => a.User)
                   .HasForeignKey<Administrator>(a => a.UserID);

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
        }
    }
}
