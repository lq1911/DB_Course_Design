using BackEnd.Models;
using BackEnd.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class DishConfig : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("DISHES");

            builder.HasKey(d => d.DishID);

            builder.Property(d => d.DishID).HasColumnName("DISHID").ValueGeneratedOnAdd();

            builder.Property(d => d.DishName).HasColumnName("DISHNAME").IsRequired().HasMaxLength(50);

            builder.Property(d => d.Price).HasColumnName("PRICE").IsRequired().HasColumnType("decimal(10,2)");

            builder.Property(d => d.Description).HasColumnName("DESCRIPTION").IsRequired().HasMaxLength(500);

            builder.Property(d => d.DishImage).HasColumnName("DISHIMAGE").HasMaxLength(500).IsRequired(false);

            builder.Property(d => d.IsSoldOut)
                   .HasColumnName("ISSOLDOUT")
                   .IsRequired()
                   .HasConversion<string>() // 将枚举存储为字符串（如 "IsSoldOut", "Available"），更具可读性
                   .HasMaxLength(20)
                   .HasDefaultValue(DishIsSoldOut.IsSoldOut); // 使用强类型枚举设置默认值

            builder.Property(d => d.Type)
                   .HasColumnName("TYPE")
                   .IsRequired()
                   .HasConversion<string>() // 将枚举存储为字符串（如 "SignatureRecommendation", "Meat"），更具可读性
                   .HasMaxLength(30)
                   .HasDefaultValue(DishTypes.SignatureRecommendation); // 使用强类型枚举设置默认值

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------
        }
    }
}