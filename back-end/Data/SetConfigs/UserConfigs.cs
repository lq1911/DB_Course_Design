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
            
            // 映射列名到Oracle数据库中的大写列名，明确指定所有数据类型
            entity.Property(u => u.UserID).HasColumnName("USERID").HasColumnType("NUMBER(10,0)");
            entity.Property(u => u.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15).HasColumnType("NVARCHAR2(15)");
            entity.Property(u => u.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(128).HasColumnType("NVARCHAR2(128)");
            entity.Property(u => u.PhoneNumber).HasColumnName("PHONENUMBER").IsRequired().HasColumnType("NUMBER");
            entity.Property(u => u.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(30).HasColumnType("NVARCHAR2(30)");
            entity.Property(u => u.Gender).HasColumnName("GENDER").HasMaxLength(2).HasColumnType("NVARCHAR2(2)");
            entity.Property(u => u.FullName).HasColumnName("FULLNAME").HasMaxLength(6).HasColumnType("NVARCHAR2(6)");
            entity.Property(u => u.Avatar).HasColumnName("AVATAR").HasMaxLength(255).HasColumnType("NVARCHAR2(255)");
            
            // 明确指定数据类型以避免转换错误
            entity.Property(u => u.Birthday).HasColumnName("BIRTHDAY").HasColumnType("TIMESTAMP(7)");
            entity.Property(u => u.AccountCreationTime).HasColumnName("ACCOUNTCREATIONTIME").IsRequired().HasColumnType("TIMESTAMP(7)");
            
            entity.Property(u => u.IsProfilePublic).HasColumnName("ISPROFILEPUBLIC").IsRequired().HasMaxLength(10).HasColumnType("NVARCHAR2(10)");
            entity.Property(u => u.Role).HasColumnName("ROLE").HasMaxLength(10).HasColumnType("NVARCHAR2(10)");
        }
    }
}