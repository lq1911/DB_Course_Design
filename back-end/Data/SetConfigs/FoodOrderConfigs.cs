using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("FOOD_ORDERS");
            entity.HasKey(o => o.OrderID);
            
            // 映射列名到Oracle数据库中的大写列名
            entity.Property(o => o.OrderID).HasColumnName("ORDERID");
            entity.Property(o => o.PaymentTime).HasColumnName("PAYMENTTIME").IsRequired();
            entity.Property(o => o.Remarks).HasColumnName("REMARKS").HasMaxLength(255);
            entity.Property(o => o.CustomerID).HasColumnName("CUSTOMERID").IsRequired();
            entity.Property(o => o.CartID).HasColumnName("CARTID").IsRequired();
            entity.Property(o => o.StoreID).HasColumnName("STOREID").IsRequired();
            entity.Property(o => o.SellerID).HasColumnName("SELLERID").IsRequired();
            
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
