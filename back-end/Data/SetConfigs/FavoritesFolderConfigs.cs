using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class FavoritesFolderConfig : IEntityTypeConfiguration<FavoritesFolder>
    {
        public void Configure(EntityTypeBuilder<FavoritesFolder> entity)
        {
            entity.ToTable("FAVORITESFOLDER");

            entity.HasKey(e => e.FolderID);
            entity.Property(e => e.FolderID).HasColumnName("FOLDERID");
            entity.Property(e => e.FolderName).HasColumnName("FOLDERNAME").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CustomerID).HasColumnName("CUSTOMERID").IsRequired();

            entity.HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomerID);
        }
    }
}