using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("USERS");
            entity.HasKey(u => u.UserID);
            
            // 映射列名到Oracle数据库中的大写列名
            entity.Property(u => u.UserID).HasColumnName("USERID");
            entity.Property(u => u.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15);
            entity.Property(u => u.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(128);
            entity.Property(u => u.PhoneNumber).HasColumnName("PHONENUMBER").IsRequired().HasMaxLength(15);
            entity.Property(u => u.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(30);
            entity.Property(u => u.Gender).HasColumnName("GENDER").HasMaxLength(6);
            entity.Property(u => u.FullName).HasColumnName("FULLNAME").HasMaxLength(30);
            entity.Property(u => u.Avatar).HasColumnName("AVATAR").HasMaxLength(255);
            
            // 移除DATE类型指定，让Entity Framework自动处理
            entity.Property(u => u.Birthday).HasColumnName("BIRTHDAY");
            entity.Property(u => u.AccountCreationTime).HasColumnName("ACCOUNTCREATIONTIME").IsRequired();
            
            entity.Property(u => u.IsProfilePublic).HasColumnName("ISPROFILEPUBLIC").IsRequired();
        }
    }
}