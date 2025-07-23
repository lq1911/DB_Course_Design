using BackEnd.Models;
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

            builder.Property(d => d.DishID).HasColumnName("DISHID");

            builder.Property(d => d.DishName).HasColumnName("DISHNAME").IsRequired().HasMaxLength(50);
            
            builder.Property(d => d.Price).HasColumnName("PRICE").IsRequired().HasColumnType("decimal(10,2)");
            
            builder.Property(d => d.Description).HasColumnName("DESCRIPTION").IsRequired().HasMaxLength(500);
            
            builder.Property(d => d.IsSoldOut).HasColumnName("ISSOLDOUT").IsRequired();
        }
    }
}