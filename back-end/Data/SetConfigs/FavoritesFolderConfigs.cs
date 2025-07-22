using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class FavoritesFolderConfig : IEntityTypeConfiguration<FavoritesFolder>
    {
        public void Configure(EntityTypeBuilder<FavoritesFolder> entity)
        {
            entity.ToTable("FavoritesFolder");
            entity.HasKey(f => f.FolderID);
            entity.Property(f => f.FolderName).IsRequired().HasMaxLength(50);
            entity.HasOne(f => f.Customer)
                   .WithMany()
                   .HasForeignKey(f => f.CustomerID);
        }
    }
}