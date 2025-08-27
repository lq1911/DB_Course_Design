using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("MENUS");

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
        }
    }
}