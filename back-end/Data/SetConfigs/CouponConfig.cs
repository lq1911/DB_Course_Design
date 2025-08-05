using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class CouponConfig : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("COUPONS");

            builder.HasKey(c => c.CouponID);

            builder.Property(c => c.CouponID).HasColumnName("COUPONID");

            builder.Property(c => c.MinimumSpend).HasColumnName("MINIMUMSPEND").IsRequired().HasColumnType("decimal(10,2)");
            
            builder.Property(c => c.DiscountAmount).HasColumnName("DISCOUNTAMOUNT").IsRequired().HasColumnType("decimal(10,2)");
            
            builder.Property(c => c.ValidFrom).HasColumnName("VALIDFROM").IsRequired();
            
            builder.Property(c => c.ValidTo).HasColumnName("VALIDTO").IsRequired();
            
            builder.Property(c => c.ApplicableStoreID).HasColumnName("APPLICABLESTOREID");
            
            builder.Property(c => c.OrderID).HasColumnName("ORDERID");
            
            builder.Property(c => c.SellerID).HasColumnName("SELLERID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------
            
            // 关系一: Coupon -> Store (多对一, 可选)
            // 由于 Store 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(c => c.Store)
                   .WithMany()
                   .HasForeignKey(c => c.ApplicableStoreID);

            // 关系二: Coupon -> Order (多对一, 可选)
            // 由于 Order 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(c => c.Order)
                   .WithMany()
                   .HasForeignKey(c => c.OrderID);

            // 关系三: Coupon -> Seller (多对一)
            // 由于 Seller 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(c => c.Seller)
                   .WithMany()
                   .HasForeignKey(c => c.SellerID);
        }
    }
}