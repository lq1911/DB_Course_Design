using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class FavoritesFolderConfig : IEntityTypeConfiguration<FavoritesFolder>
    {
        public void Configure(EntityTypeBuilder<FavoritesFolder> builder)
        {
            builder.ToTable("FAVORITES_FOLDERS");

            builder.HasKey(ff => ff.FolderID);

            builder.Property(ff => ff.FolderID).HasColumnName("FOLDERID").ValueGeneratedOnAdd();

            builder.Property(ff => ff.FolderName).HasColumnName("FOLDERNAME").IsRequired().HasMaxLength(50);

            builder.Property(ff => ff.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: FavoritesFolder -> Customer (多对一)
            // Customer 类中有 FavoritesFolders 导航属性，应明确指定
            builder.HasOne(f => f.Customer)
                   .WithMany(c => c.FavoritesFolders) // 明确指定 Customer 端的反向导航属性
                   .HasForeignKey(f => f.CustomerID)
                   .OnDelete(DeleteBehavior.Cascade); // 当顾客被删除时，其所有收藏夹也应被级联删除
        }
    }
}