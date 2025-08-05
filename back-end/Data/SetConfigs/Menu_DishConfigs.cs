using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class Menu_DishConfig : IEntityTypeConfiguration<Menu_Dish>
    {
        public void Configure(EntityTypeBuilder<Menu_Dish> entity)
        {
            entity.ToTable("MENU_DISH");

            entity.HasKey(e => new { e.MenuID, e.DishID });
            entity.Property(e => e.MenuID).HasColumnName("MENUID");
            entity.Property(e => e.DishID).HasColumnName("DISHID");

            entity.HasOne(e => e.Menu)
                .WithMany()
                .HasForeignKey(e => e.MenuID);

            entity.HasOne(e => e.Dish)
                .WithMany()
                .HasForeignKey(e => e.DishID);
        }
    }
}