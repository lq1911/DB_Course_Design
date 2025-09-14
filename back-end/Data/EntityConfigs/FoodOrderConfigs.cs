using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class FoodOrderConfig : IEntityTypeConfiguration<FoodOrder>
    {
        public void Configure(EntityTypeBuilder<FoodOrder> builder)
        {
            builder.ToTable("FOOD_ORDERS");

            builder.HasKey(fo => fo.OrderID);
            builder.Property(fo => fo.OrderID).HasColumnName("ORDERID").ValueGeneratedOnAdd();

            builder.Property(fo => fo.OrderTime).HasColumnName("ORDERTIME").IsRequired();

            builder.Property(fo => fo.PaymentTime).HasColumnName("PAYMENTTIME").IsRequired(false);

            builder.Property(fo => fo.Remarks).HasColumnName("REMARKS").HasMaxLength(255);

            builder.Property(fo => fo.FoodOrderState).HasColumnName("FOODORDERSTATE").IsRequired().HasConversion<string>().HasMaxLength(20);

            builder.Property(fo => fo.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            builder.Property(fo => fo.CartID).HasColumnName("CARTID").IsRequired(false);

            builder.Property(fo => fo.StoreID).HasColumnName("STOREID").IsRequired();
            
              builder.Property(fo => fo.DeliveryFee).HasColumnName("DELIVERYFEE").IsRequired();

            // 在 CartID 列上创建一个唯一索引
                     builder.HasIndex(fo => fo.CartID)
                   .IsUnique();

            // ---------------------------------------------------------------
            // 配置关系
            // ---------------------------------------------------------------

            // 关系一: FoodOrder -> Customer (多对一)
            builder.HasOne(e => e.Customer)
                   .WithMany(c => c.FoodOrders)
                   .HasForeignKey(e => e.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict); // 禁止删除仍有订单的顾客

            // 关系二: FoodOrder -> ShoppingCart (一对一)
            // 一个订单对应一个购物车，一个购物车结算后也只生成一个订单
            builder.HasOne(e => e.Cart)
                   .WithOne(c => c.Order)
                   .HasForeignKey<FoodOrder>(e => e.CartID)
                   .OnDelete(DeleteBehavior.SetNull); // 购物车删除时，订单的 CartID 设为 null

            // 关系三: FoodOrder -> Store (多对一)
            builder.HasOne(e => e.Store)
                   .WithMany(s => s.FoodOrders)
                   .HasForeignKey(e => e.StoreID)
                   .OnDelete(DeleteBehavior.Restrict); // 禁止删除仍有订单的商店
        }
    }
}