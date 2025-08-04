using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class FavoriteItemConfig : IEntityTypeConfiguration<FavoriteItem>
    {
        public void Configure(EntityTypeBuilder<FavoriteItem> builder)
        {
            builder.ToTable("FAVORITE_ITEMS");

            builder.HasKey(fi => fi.ItemID);

            builder.Property(fi => fi.ItemID).HasColumnName("ITEMID");

            builder.Property(fi => fi.FavoritedAt).HasColumnName("FAVORITEDAT").IsRequired();
            
            builder.Property(fi => fi.FavoriteReason).HasColumnName("FAVORITEREASON").IsRequired().HasMaxLength(500);
            
            builder.Property(fi => fi.StoreID).HasColumnName("STOREID").IsRequired();
            
            builder.Property(fi => fi.FolderID).HasColumnName("FOLDERID").IsRequired();
            
            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: FavoriteItem -> Store (多对一)
            // 由于 Store 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(fi => fi.Store)
                   .WithMany()
                   .HasForeignKey(fi => fi.StoreID);

            // 关系二: FavoriteItem -> FavoritesFolder (多对一)
            // 由于 FavoritesFolder 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(fi => fi.Folder)
                   .WithMany()
                   .HasForeignKey(fi => fi.FolderID);
        }
    }
}