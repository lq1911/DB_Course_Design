using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.EntityConfigs
{
    public class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> entity)
        {
            entity.ToTable("CARTITEM");

            entity.HasKey(e => e.ItemID);

            entity.Property(e => e.ItemID)
                  .HasColumnName("ITEMID");

            entity.Property(e => e.Quantity)
                  .HasColumnName("QUANTITY")
                  .IsRequired();

            entity.Property(e => e.TotalPrice)
                  .HasColumnName("TOTALPRICE")
                  .HasColumnType("decimal(10,2)")
                  .HasDefaultValue(0.00m);

            entity.Property(e => e.DishID)
                  .HasColumnName("DISHID")
                  .IsRequired();

            entity.Property(e => e.CartID)
                  .HasColumnName("CARTID")
                  .IsRequired();

            entity.HasOne(e => e.Dish)
                  .WithMany()
                  .HasForeignKey(e => e.DishID);

            entity.HasOne(e => e.Cart)
                  .WithMany()
                  .HasForeignKey(e => e.CartID);
        }
    }
}