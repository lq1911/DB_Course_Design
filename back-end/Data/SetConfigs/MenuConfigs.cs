using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            entity.ToTable("MENU");

            entity.HasKey(e => e.MenuID);
            entity.Property(e => e.MenuID).HasColumnName("MENUID");
            entity.Property(e => e.Version).HasColumnName("VERSION").IsRequired().HasMaxLength(50);
            entity.Property(e => e.ActivePeriod).HasColumnName("ACTIVEPERIOD").IsRequired();
            entity.Property(e => e.StoreID).HasColumnName("STOREID").IsRequired();

            entity.HasOne(e => e.Store)
                .WithMany()
                .HasForeignKey(e => e.StoreID);
        }
    }
}