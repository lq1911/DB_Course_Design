using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Order");
            entity.HasKey(o => o.OrderID);
            entity.Property(o => o.PaymentTime).IsRequired();
            entity.Property(o => o.Remarks).HasMaxLength(255);
            entity.HasOne(o => o.Customer)
                   .WithMany()
                   .HasForeignKey(o => o.CustomerID);
            entity.HasOne(o => o.Cart)
                   .WithMany()
                   .HasForeignKey(o => o.CartID);
            entity.HasOne(o => o.Store)
                   .WithMany()
                   .HasForeignKey(o => o.StoreID);
            entity.HasOne(o => o.Seller)
                   .WithMany()
                   .HasForeignKey(o => o.SellerID);
        }
    }
}
