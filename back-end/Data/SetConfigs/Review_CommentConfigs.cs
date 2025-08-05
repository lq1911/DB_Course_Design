using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Models;

namespace BackEnd.Data.SetConfigs
{
    public class Review_CommentConfig : IEntityTypeConfiguration<Review_Comment>
    {
        public void Configure(EntityTypeBuilder<Review_Comment> entity)
        {
            entity.ToTable("REVIEW_COMMENT");

            entity.HasKey(e => new { e.AdminID, e.CommentID });
            entity.Property(e => e.AdminID).HasColumnName("ADMINID");
            entity.Property(e => e.CommentID).HasColumnName("COMMENTID");
            entity.Property(e => e.ReviewTime).HasColumnName("REVIEWTIME").IsRequired();

            entity.HasOne(e => e.Admin)
                .WithMany()
                .HasForeignKey(e => e.AdminID);

            entity.HasOne(e => e.Comment)
                .WithMany()
                .HasForeignKey(e => e.CommentID);
        }
    }
}