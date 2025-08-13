using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            // --- 主键和基础属性配置 ---
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.UserID).HasColumnName("USERID").ValueGeneratedOnAdd();

            builder.Property(u => u.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15);

            // 建议：密码存储的是哈希值，长度通常较长。128位是一个合理的选择。
            builder.Property(u => u.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(128);

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
            // 关系配置：用户作为主表，与各个角色子表建立一对一关系
            // ---------------------------------------------------------------

            // 关系一: User -> Customer (一对一)
            builder.HasOne(u => u.Customer)
                   .WithOne(c => c.User) // 在 Customer 模型中，反向导航属性为 User
                   .HasForeignKey<Customer>(c => c.UserID) // 外键在 Customer 表上
                   .OnDelete(DeleteBehavior.Cascade); // 当 User 被删除时，其关联的 Customer 身份也应被级联删除。

            // 关系二: User -> Courier (一对一)
            builder.HasOne(u => u.Courier)
                   .WithOne(c => c.User) // 在 Courier 模型中，反向导航属性为 User
                   .HasForeignKey<Courier>(c => c.UserID) // 外键在 Courier 表上
                   .OnDelete(DeleteBehavior.Cascade); // 当 User 被删除时，其关联的 Courier 身份也应被级联删除。

            // 关系三: User -> Administrator (一对一)
            builder.HasOne(u => u.Administrator)
                   .WithOne(a => a.User) // 在 Administrator 模型中，反向导航属性为 User
                   .HasForeignKey<Administrator>(a => a.UserID) // 外键在 Administrator 表上
                   .OnDelete(DeleteBehavior.Cascade); // 当 User 被删除时，其关联的 Administrator 身份也应被级联删除。

            // 关系四: User -> Seller (一对一)
            builder.HasOne(u => u.Seller)
                   .WithOne(s => s.User) // 在 Seller 模型中，反向导航属性为 User
                   .HasForeignKey<Seller>(s => s.UserID) // 外键在 Seller 表上
                   .OnDelete(DeleteBehavior.Cascade); // 当 User 被删除时，其关联的 Seller 身份也应被级联删除。
        }
    }
}
