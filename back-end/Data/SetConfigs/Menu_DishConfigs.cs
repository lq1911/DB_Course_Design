using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class MenuDishConfig : IEntityTypeConfiguration<Menu_Dish>
    {
        public void Configure(EntityTypeBuilder<Menu_Dish> entity)
        {
            entity.ToTable("MenuDish");
            entity.HasKey(md => new { md.MenuID, md.DishID });
            entity.HasOne(md => md.Menu)
                   .WithMany()
                   .HasForeignKey(md => md.MenuID);
            entity.HasOne(md => md.Dish)
                   .WithMany()
                   .HasForeignKey(md => md.DishID);
        }
    }
}