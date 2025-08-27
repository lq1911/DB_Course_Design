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

            builder.Property(cm => cm.CouponManagerID).HasColumnName("COUPONMANAGERID").ValueGeneratedOnAdd();

            builder.Property(cm => cm.MinimumSpend).HasColumnName("MINIMUMSPEND").IsRequired().HasColumnType("decimal(10,2)");

            builder.Property(cm => cm.DiscountAmount).HasColumnName("DISCOUNTAMOUNT").IsRequired().HasColumnType("decimal(10,2)");

            builder.Property(cm => cm.ValidFrom).HasColumnName("VALIDFROM").IsRequired();

            builder.Property(cm => cm.ValidTo).HasColumnName("VALIDTO").IsRequired();

            builder.Property(cm => cm.StoreID).HasColumnName("STOREID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: CouponManager -> Store (多对一)
            // 优惠券模板可以关联到特定商店
            builder.HasOne(cm => cm.Store)
                   .WithMany(s => s.CouponManagers)
                   .HasForeignKey(cm => cm.StoreID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
