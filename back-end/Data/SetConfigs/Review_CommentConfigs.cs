using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.SetConfigs
{
    public class ReviewCommentConfig : IEntityTypeConfiguration<Review_Comment>
    {
        public void Configure(EntityTypeBuilder<Review_Comment> entity)
        {
            entity.ToTable("ReviewComment");
            entity.HasKey(rc => new { rc.AdminID, rc.CommentID });
            entity.Property(rc => rc.ReviewTime).IsRequired();
            entity.HasOne(rc => rc.Admin)
                   .WithMany()
                   .HasForeignKey(rc => rc.AdminID);
            entity.HasOne(rc => rc.Comment)
                   .WithMany()
                   .HasForeignKey(rc => rc.CommentID);
        }
    }
}