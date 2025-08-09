using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("COMMENTS");

            builder.HasKey(c => c.CommentID);

            builder.Property(c => c.CommentID).HasColumnName("COMMENTID").ValueGeneratedOnAdd();

            builder.Property(c => c.Content).HasColumnName("CONTENT").IsRequired().HasMaxLength(500);

            builder.Property(c => c.PostedAt).HasColumnName("POSTEDAT").IsRequired();

            builder.Property(c => c.Likes).HasColumnName("LIKES").HasDefaultValue(0);

            builder.Property(c => c.Replies).HasColumnName("REPLIES").HasDefaultValue(0);

            builder.Property(c => c.ReplyToCommentID).HasColumnName("REPLYTOCOMMENTID");

            builder.Property(c => c.StoreID).HasColumnName("STOREID").IsRequired();

            builder.Property(c => c.CommenterID).HasColumnName("COMMENTERID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Comment -> Store (多对一)
            builder.HasOne(c => c.Store)
                   .WithMany(s => s.Comments)
                   .HasForeignKey(c => c.StoreID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系二: Comment -> Customer (多对一)
            builder.HasOne(c => c.Commenter)
                   .WithMany(cu => cu.Comments)
                   .HasForeignKey(c => c.CommenterID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系三: Comment -> Comment (自引用，多对一)
            // 用于实现评论回复功能。一个评论可以回复另一个评论。
            builder.HasOne(c => c.ReplyToComment)
                   .WithMany(cr => cr.CommentReplies)
                   .HasForeignKey(c => c.ReplyToCommentID)
                   .OnDelete(DeleteBehavior.ClientSetNull); // 重要：防止级联删除导致问题

            // 关系四: Comment <-> Administrator (多对多，通过 Review_Comment)
            builder.HasMany(c => c.ReviewComments)
                   .WithOne(rc => rc.Comment)
                   .HasForeignKey(rc => rc.CommentID)
                   .OnDelete(DeleteBehavior.Cascade); // 当评论删除时，相关的审核记录也删除
        }
    }
}