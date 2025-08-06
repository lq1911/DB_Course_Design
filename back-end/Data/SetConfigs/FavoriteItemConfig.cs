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

            builder.Property(fi => fi.ItemID).HasColumnName("ITEMID").ValueGeneratedOnAdd();

            builder.Property(fi => fi.FavoritedAt).HasColumnName("FAVORITEDAT").IsRequired();

            builder.Property(fi => fi.FavoriteReason).HasColumnName("FAVORITEREASON").IsRequired().HasMaxLength(500);

            builder.Property(fi => fi.StoreID).HasColumnName("STOREID").IsRequired();

            builder.Property(fi => fi.FolderID).HasColumnName("FOLDERID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: FavoriteItem -> Store (多对一)
            builder.HasOne(fi => fi.Store)
                   .WithMany(s => s.FavoriteItems)
                   .HasForeignKey(fi => fi.StoreID)
                   .OnDelete(DeleteBehavior.Cascade); // 当商店被删除时，收藏夹中的相关项也应被删除

            // 关系二: FavoriteItem -> FavoritesFolder (多对一)
            builder.HasOne(fi => fi.Folder)
                   .WithMany(f => f.FavoriteItems)
                   .HasForeignKey(fi => fi.FolderID)
                   .OnDelete(DeleteBehavior.Cascade); // 当收藏夹被删除时，其包含的所有项都应被级联删除
        }
    }
}