using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("MENU");

            builder.HasKey(m => m.MenuID);

            builder.Property(m => m.MenuID).HasColumnName("MENUID").ValueGeneratedOnAdd();

            builder.Property(m => m.Version).HasColumnName("VERSION").IsRequired().HasMaxLength(50);

            builder.Property(m => m.ActivePeriod).HasColumnName("ACTIVEPERIOD").IsRequired();

            builder.Property(m => m.StoreID).HasColumnName("STOREID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: Menu -> Store (多对一)
            builder.HasOne(m => m.Store)
                   .WithMany(s => s.Menus)
                   .HasForeignKey(m => m.StoreID)
                   .OnDelete(DeleteBehavior.Cascade); // 当商店被删除时，其所有菜单也应被级联删除

            // 关系二: Menu -> Menu_Dish (一对多，用于实现多对多)
            builder.HasMany(m => m.MenuDishes)
                   .WithOne(md => md.Menu)
                   .HasForeignKey(md => md.MenuID)
                   .OnDelete(DeleteBehavior.Cascade); // 当菜单被删除时，其所有菜品关联记录都应被级联删除
        }
    }
}