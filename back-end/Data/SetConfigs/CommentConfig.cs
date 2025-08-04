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

            builder.Property(c => c.CommentID).HasColumnName("COMMENTID");

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
            // 由于 Store 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(c => c.Store)
                   .WithMany()
                   .HasForeignKey(c => c.StoreID);

            // 关系二: Comment -> Customer (多对一)
            // 由于 Customer 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(c => c.Commenter)
                   .WithMany()
                   .HasForeignKey(c => c.CommenterID);

            // 关系三: Comment -> Comment (自引用，多对一)
            // 用于实现评论回复功能。一个评论可以回复另一个评论。
            // 由于 Comment 类中没有 ICollection<Comment> 来表示“回复列表”，我们同样使用不带参数的 WithMany()
            builder.HasOne(c => c.ReplyToComment)
                   .WithMany()
                   .HasForeignKey(c => c.ReplyToCommentID)
                   .OnDelete(DeleteBehavior.ClientSetNull); // 重要：防止级联删除导致问题
        }
    }
}