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

            builder.Property(c => c.CouponID).HasColumnName("COUPONID").ValueGeneratedOnAdd();

            builder.Property(c => c.CouponState).HasColumnName("COUPONSTATE").HasConversion<string>().HasMaxLength(20).IsRequired();

            builder.Property(c => c.CouponManagerID).HasColumnName("COUPONMANAGERID").IsRequired();

            builder.Property(c => c.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            builder.Property(c => c.OrderID).HasColumnName("ORDERID").IsRequired(false);

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Coupon -> CouponManager (多对一)
            builder.HasOne(c => c.CouponManager)
                   .WithMany(cm => cm.Coupons)
                   .HasForeignKey(c => c.CouponManagerID)
                   .OnDelete(DeleteBehavior.Cascade); // 当优惠券模板删除时，所有相关优惠券也删除

            // 关系二: Coupon -> Customer (多对一)
            builder.HasOne(c => c.Customer)
                   .WithMany(cu => cu.Coupons)
                   .HasForeignKey(c => c.CustomerID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 关系三: Coupon -> FoodOrder (多对一)
            builder.HasOne(c => c.Order)
                   .WithMany(o => o.Coupons)
                   .HasForeignKey(c => c.OrderID)
                   .OnDelete(DeleteBehavior.SetNull); // 订单删除时，将优惠券的 OrderID 设为 NULL
        }
    }
}