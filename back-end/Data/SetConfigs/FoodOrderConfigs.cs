using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class FoodOrderConfig : IEntityTypeConfiguration<FoodOrder>
    {
        public void Configure(EntityTypeBuilder<FoodOrder> entity)
        {
            entity.ToTable("FOODORDER");

            entity.HasKey(e => e.OrderID);
            entity.Property(e => e.OrderID).HasColumnName("ORDERID");
            entity.Property(e => e.PaymentTime).HasColumnName("PAYMENTTIME").IsRequired();
            entity.Property(e => e.Remarks).HasColumnName("REMARKS").HasMaxLength(255);
            entity.Property(e => e.CustomerID).HasColumnName("CUSTOMERID").IsRequired();
            entity.Property(e => e.CartID).HasColumnName("CARTID").IsRequired();
            entity.Property(e => e.StoreID).HasColumnName("STOREID").IsRequired();
            entity.Property(e => e.SellerID).HasColumnName("SELLERID").IsRequired();

            entity.HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomerID);

            entity.HasOne(e => e.Cart)
                .WithMany()
                .HasForeignKey(e => e.CartID);

            entity.HasOne(e => e.Store)
                .WithMany()
                .HasForeignKey(e => e.StoreID);

            entity.HasOne(e => e.Seller)
                .WithMany()
                .HasForeignKey(e => e.SellerID);
        }
    }
}