using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            entity.ToTable("Menu");
            entity.HasKey(m => m.MenuID);
            entity.Property(m => m.Version).IsRequired().HasMaxLength(50);
            entity.Property(m => m.ActivePeriod).IsRequired();
            entity.HasOne(m => m.Store)
                   .WithMany()
                   .HasForeignKey(m => m.StoreID);
        }
    }
}