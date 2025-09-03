using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class CouponManagerConfig : IEntityTypeConfiguration<CouponManager>
    {
        public void Configure(EntityTypeBuilder<CouponManager> builder)
        {
            builder.ToTable("COUPON_MANAGERS");

            builder.HasKey(cm => cm.CouponManagerID);

            // 主键配置 - 由前端提供
            builder.Property(cm => cm.CouponManagerID)
                   .HasColumnName("COUPONMANAGERID")
                   .ValueGeneratedNever();

            // 基本字段配置 - 根据数据库表结构配置
            // 必填字段（NOT NULL）
            builder.Property(cm => cm.MinimumSpend).HasColumnName("MINIMUMSPEND").IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(cm => cm.DiscountAmount).HasColumnName("DISCOUNTAMOUNT").IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(cm => cm.ValidFrom).HasColumnName("VALIDFROM").IsRequired();
            builder.Property(cm => cm.ValidTo).HasColumnName("VALIDTO").IsRequired();
            builder.Property(cm => cm.StoreID).HasColumnName("STOREID").IsRequired();
            
            // 可空字段（NULLABLE）
            builder.Property(cm => cm.CouponName).HasColumnName("COUPONNAME").HasMaxLength(100);
            builder.Property(cm => cm.CouponType).HasColumnName("COUPONTYPE");
            builder.Property(cm => cm.DiscountRate).HasColumnName("DISCOUNTRATE").HasColumnType("decimal(3,2)");
            builder.Property(cm => cm.TotalQuantity).HasColumnName("TOTALQUANTITY");
            builder.Property(cm => cm.UsedQuantity).HasColumnName("USEDQUANTITY");
            builder.Property(cm => cm.Description).HasColumnName("DESCRIPTION").HasMaxLength(500);
            builder.Property(cm => cm.SellerID).HasColumnName("SELLERID");

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: CouponManager -> Store (多对一)
            // 优惠券模板可以关联到特定商店
            builder.HasOne(cm => cm.Store)
                   .WithMany(s => s.CouponManagers)
                   .HasForeignKey(cm => cm.StoreID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 关系二: CouponManager -> Seller (多对一)
            // 优惠券模板可以关联到特定商家
            builder.HasOne(cm => cm.Seller)
                   .WithMany()
                   .HasForeignKey(cm => cm.SellerID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
