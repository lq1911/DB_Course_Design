using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class Customer_CouponConfig : IEntityTypeConfiguration<Customer_Coupon>
    {
        public void Configure(EntityTypeBuilder<Customer_Coupon> builder)
        {
            builder.ToTable("CUSTOMER_COUPON");

            // 设置复合主键
            builder.HasKey(cc => new { cc.CustomerID, cc.CouponID });

            // 配置列属性和名称
            builder.Property(cc => cc.CustomerID).HasColumnName("CUSTOMERID");
            
            builder.Property(cc => cc.CouponID).HasColumnName("COUPONID");
            
            builder.Property(cc => cc.CouponQuantity).HasColumnName("COUPONQUANTITY").IsRequired();
            
            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------
            
            // 关系一: Customer_Coupon -> Customer (多对一)
            // 由于 Customer 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(cc => cc.Customer)
                   .WithMany()
                   .HasForeignKey(cc => cc.CustomerID);

            // 关系二: Customer_Coupon -> Coupon (多对一)
            // 由于 Coupon 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(cc => cc.Coupon)
                   .WithMany()
                   .HasForeignKey(cc => cc.CouponID);
        }
    }
}