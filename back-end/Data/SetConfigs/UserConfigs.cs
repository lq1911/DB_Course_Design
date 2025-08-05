using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("USER");

            entity.HasKey(e => e.UserID);
            entity.Property(e => e.UserID).HasColumnName("USERID");
            entity.Property(e => e.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15);
            entity.Property(e => e.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(128);
            entity.Property(e => e.PhoneNumber).HasColumnName("PHONENUMBER").IsRequired().HasMaxLength(15);
            entity.Property(e => e.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(30);
            entity.Property(e => e.Gender).HasColumnName("GENDER").HasMaxLength(6);
            entity.Property(e => e.FullName).HasColumnName("FULLNAME").HasMaxLength(30);
            entity.Property(e => e.Avatar).HasColumnName("AVATAR").HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnName("BIRTHDAY");
            entity.Property(e => e.AccountCreationTime).HasColumnName("ACCOUNTCREATIONTIME").IsRequired();
            entity.Property(e => e.IsProfilePublic).HasColumnName("ISPROFILEPUBLIC").IsRequired();
        }
    }
}