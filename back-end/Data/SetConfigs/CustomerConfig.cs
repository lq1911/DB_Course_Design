using BackEnd.Models;
using BackEnd.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CUSTOMERS");

            builder.HasKey(c => c.UserID);

            builder.Property(c => c.UserID).HasColumnName("USERID").ValueGeneratedNever();

            builder.Property(c => c.DefaultAddress).HasColumnName("DEFAULTADDRESS").HasMaxLength(100);

            builder.Property(c => c.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);

            builder.Property(c => c.IsMember)
                    .HasColumnName("ISMEMBER")
                    .HasConversion<string>() // 将枚举存储为字符串（如 "NotMember", "Active"），更具可读性
                    .HasMaxLength(20) // 确保字符串长度足够
                    .HasDefaultValue(MembershipStatus.NotMember);

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: 与 User 的一对一关系
            builder.HasOne(c => c.User)
                   .WithOne(u => u.Customer)
                   .HasForeignKey<Customer>(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 关系二: 与 DeliveryTask 的一对多关系
            // 不允许删除仍有关联配送任务的顾客
            builder.HasMany(c => c.DeliveryTasks)
                   .WithOne(dt => dt.Customer)
                   .HasForeignKey(dt => dt.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系三: 与 FoodOrder 的一对多关系
            // 不允许删除仍有订单历史的顾客
            builder.HasMany(c => c.FoodOrders)
                   .WithOne(fo => fo.Customer)
                   .HasForeignKey(fo => fo.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系四: 与 Coupon 的一对多关系
            // 不允许删除仍持有优惠券的顾客
            builder.HasMany(c => c.Coupons)
                   .WithOne(co => co.Customer)
                   .HasForeignKey(co => co.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系五: 与 FavoritesFolder 的一对多关系
            // 收藏夹是顾客的私有数据，当顾客被删除时，其收藏夹也应被级联删除
            builder.HasMany(c => c.FavoritesFolders)
                   .WithOne(ff => ff.Customer)
                   .HasForeignKey(ff => ff.CustomerID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 关系六: 与 Comment 的一对多关系
            // 不允许删除仍有评论的顾客
            builder.HasMany(c => c.Comments)
                   .WithOne(co => co.Commenter)
                   .HasForeignKey(co => co.CommenterID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系七: 与 ShoppingCart 的一对多关系
            // 不允许删除仍有购物车的顾客
            builder.HasMany(c => c.ShoppingCarts)
                   .WithOne(sc => sc.Customer)
                   .HasForeignKey(sc => sc.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}