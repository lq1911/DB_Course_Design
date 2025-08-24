using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
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

            // 关系四: FoodOrder -> Coupon (一对多)
            builder.HasMany(e => e.Coupons)
                   .WithOne(c => c.Order)
                   .HasForeignKey(c => c.OrderID)
                   .OnDelete(DeleteBehavior.SetNull); // 订单删除时，关联的优惠券设为未使用，而不是删除优惠券本身

            // 关系五: FoodOrder -> AfterSaleApplication (一对多)
            builder.HasMany(e => e.AfterSaleApplications)
                   .WithOne(a => a.Order)
                   .HasForeignKey(a => a.OrderID)
                   .OnDelete(DeleteBehavior.Cascade); // 当订单被删除时，其所有售后申请都应被级联删除

            // 关系六: FoodOrder -> Comment (一对多)
            builder.HasMany(fo => fo.Comments)
                   .WithOne(c => c.FoodOrder)
                   .HasForeignKey(c => c.FoodOrderID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}