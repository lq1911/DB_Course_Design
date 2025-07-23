using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");
            entity.HasKey(u => u.UserID);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(15);
            entity.Property(u => u.Password).IsRequired().HasMaxLength(128);
            entity.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(30);
            entity.Property(u => u.Gender).HasMaxLength(6);
            entity.Property(u => u.FullName).HasMaxLength(30);
            entity.Property(u => u.Avatar).HasMaxLength(255);
            entity.Property(u => u.AccountCreationTime).IsRequired();
            entity.Property(u => u.IsProfilePublic).IsRequired();
        }
    }
}