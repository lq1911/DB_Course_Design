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

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 配置与 Menu 的多对多关系 (通过 Menu_Dish 中间表)
            // 一个菜品可以出现在多个 Menu_Dish 记录中
            builder.HasMany(d => d.MenuDishes)
                   .WithOne(md => md.Dish) // 每个 Menu_Dish 记录对应一个菜品
                   .HasForeignKey(md => md.DishID) // 外键在 Menu_Dish 表上
                   .OnDelete(DeleteBehavior.Cascade); // 当菜品被删除时，其在所有菜单中的记录也应被删除
        }
    }
}