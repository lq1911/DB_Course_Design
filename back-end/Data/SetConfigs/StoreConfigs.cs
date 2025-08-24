using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("STORES");

            // --- 主键和基础属性配置 ---
            builder.HasKey(s => s.StoreID);
            builder.Property(s => s.StoreID).HasColumnName("STOREID").ValueGeneratedOnAdd();

            builder.Property(s => s.StoreName).HasColumnName("STORENAME").IsRequired().HasMaxLength(50);

            builder.Property(s => s.StoreAddress).HasColumnName("STOREADDRESS").IsRequired().HasMaxLength(100);

            builder.Property(s => s.OpenTime).HasColumnName("OPENTIME").IsRequired();

            builder.Property(s => s.CloseTime).HasColumnName("CLOSETIME").IsRequired();

            builder.Property(s => s.AverageRating).HasColumnName("AVERAGERATING").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);

            builder.Property(s => s.MonthlySales).HasColumnName("MONTHLYSALES").IsRequired();

            builder.Property(s => s.StoreFeatures).HasColumnName("STOREFEATURES").IsRequired().HasMaxLength(500);

            builder.Property(s => s.StoreCreationTime).HasColumnName("STORECREATIONTIME").IsRequired();

            builder.Property(s => s.StoreState).HasColumnName("STORESTATE").IsRequired().HasConversion<string>().HasMaxLength(20);

            builder.Property(s => s.StoreCategory).HasColumnName("STORECATEGORY").IsRequired().HasConversion<string>().HasMaxLength(20);

            builder.Property(s => s.StoreImage).HasColumnName("STOREIMAGE").HasMaxLength(500).IsRequired(false);

            // 忽略不映射到数据库的属性
            builder.Ignore(s => s.IsOpen);
            builder.Ignore(s => s.BusinessHoursDisplay);

            // --- 外键属性声明 ---
            builder.Property(s => s.SellerID).HasColumnName("SELLERID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: Store -> Seller (一对一)
            builder.HasOne(s => s.Seller)
                   .WithOne(seller => seller.Store)
                   .HasForeignKey<Store>(s => s.SellerID)
                   .OnDelete(DeleteBehavior.Cascade); // 当商家被删除时，其拥有的店铺也应被级联删除，以保证数据一致性。

            // 关系二: Store -> FoodOrder (一对多)
            builder.HasMany(s => s.FoodOrders)
                   .WithOne(fo => fo.Store)
                   .HasForeignKey(fo => fo.StoreID)
                   .OnDelete(DeleteBehavior.Restrict); // 不允许删除一个还有历史订单的店铺，以保护交易记录。

            // 关系三: Store -> CouponManager (一对多)
            builder.HasMany(s => s.CouponManagers)
                   .WithOne(cm => cm.Store)
                   .HasForeignKey(cm => cm.StoreID)
                   .OnDelete(DeleteBehavior.Cascade); // 当店铺被删除时，其配置的优惠券信息也应被级联删除。

            // 关系四: Store -> Menu (一对多)
            builder.HasMany(s => s.Menus)
                   .WithOne(m => m.Store)
                   .HasForeignKey(m => m.StoreID)
                   .OnDelete(DeleteBehavior.Cascade); // 当店铺被删除时，其所有菜单也应被级联删除，因为菜单是店铺的附属物。

            // 关系五: Store -> FavoriteItem (一对多)
            builder.HasMany(s => s.FavoriteItems)
                   .WithOne(fi => fi.Store)
                   .HasForeignKey(fi => fi.StoreID)
                   .OnDelete(DeleteBehavior.Cascade); // 当店铺被删除时，用户收藏夹中关于此店铺的记录也应被移除。

            // 关系六: Store -> StoreViolationPenalty (一对多)
            builder.HasMany(s => s.StoreViolationPenalties)
                   .WithOne(svp => svp.Store)
                   .HasForeignKey(svp => svp.StoreID)
                   .OnDelete(DeleteBehavior.Restrict); // 不允许删除一个有违规处罚记录的店铺，以保留重要的管理历史。

            // 关系七: Store -> Comment (一对多)
            builder.HasMany(s => s.Comments)
                   .WithOne(c => c.Store)
                   .HasForeignKey(c => c.StoreID)
                   .OnDelete(DeleteBehavior.Restrict); // 不允许删除一个有用户评论的店铺，以保护用户生成的内容。

            // 关系八: Store -> DeliveryTask (一对多)
            builder.HasMany(s => s.DeliveryTasks)
                   .WithOne(dt => dt.Store)
                   .HasForeignKey(dt => dt.StoreID)
                   .OnDelete(DeleteBehavior.SetNull); // 如果店铺被删除，相关的配送任务记录本身不删除，仅将其与店铺的关联设为NULL。
        }
    }
}
