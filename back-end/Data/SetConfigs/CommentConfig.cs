using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

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

            builder.Property(c => c.Rating).HasColumnName("RATING").IsRequired(false).HasComment("评分：1-5分");

            builder.Property(c => c.CommentImage).HasColumnName("COMMENTIMAGE").IsRequired(false).HasMaxLength(1000);

            // 配置评论类型
            builder.Property(c => c.CommentType)
                   .HasColumnName("COMMENTTYPE")
                   .IsRequired()
                   .HasConversion<int>() // 枚举存储为整数
                   .HasComment("评论类型：1=店铺评论，2=订单评论，3=回复评论");

            builder.Property(c => c.ReplyToCommentID).HasColumnName("REPLYTOCOMMENTID").IsRequired(false);

            builder.Property(c => c.StoreID).HasColumnName("STOREID").IsRequired(false);

            builder.Property(c => c.FoodOrderID).HasColumnName("FOODORDERID").IsRequired(false);

            builder.Property(c => c.CommenterID).HasColumnName("COMMENTERID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Comment -> Store (多对一)
            builder.HasOne(c => c.Store)
                   .WithMany(s => s.Comments)
                   .HasForeignKey(c => c.StoreID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 关系二: Comment -> Customer (多对一)
            builder.HasOne(c => c.Commenter)
                   .WithMany(cu => cu.Comments)
                   .HasForeignKey(c => c.CommenterID)
                   .OnDelete(DeleteBehavior.SetNull);

            // 关系三: Comment -> Comment (自引用，多对一)
            // 用于实现评论回复功能。一个评论可以回复另一个评论。
            builder.HasOne(c => c.ReplyToComment)
                   .WithMany(rc => rc.CommentReplies)
                   .HasForeignKey(c => c.ReplyToCommentID)
                   .OnDelete(DeleteBehavior.SetNull); // 重要：防止级联删除导致问题

            // 关系四: Comment -> FoodOrder (多对一)
            builder.HasOne(c => c.FoodOrder)
                  .WithMany(fo => fo.Comments)
                  .HasForeignKey(c => c.FoodOrderID)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}