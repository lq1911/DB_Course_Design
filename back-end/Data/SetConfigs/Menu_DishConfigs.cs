using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class Menu_DishConfig : IEntityTypeConfiguration<Menu_Dish>
    {
        public void Configure(EntityTypeBuilder<Menu_Dish> builder)
        {
            builder.ToTable("MENU_DISH");

            builder.HasKey(md => new { md.MenuID, md.DishID });

            builder.Property(md => md.MenuID).HasColumnName("MENUID");

            builder.Property(md => md.DishID).HasColumnName("DISHID");

            // ---------------------------------------------------------------
            // 配置关系
            // ---------------------------------------------------------------

            // 关系一: Menu_Dish -> Menu (多对一)
            builder.HasOne(md => md.Menu)
                   .WithMany(m => m.MenuDishes)
                   .HasForeignKey(md => md.MenuID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 关系二: Menu_Dish -> Dish (多对一)
            builder.HasOne(md => md.Dish)
                   .WithMany(d => d.MenuDishes)
                   .HasForeignKey(md => md.DishID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}