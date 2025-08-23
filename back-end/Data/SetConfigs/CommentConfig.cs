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

            // 配置字符串数组属性 - 存储为 JSON
            builder.Property(c => c.CommentImage)
                   .HasConversion(
                       v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                       v => string.IsNullOrEmpty(v) ? null : JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions?)null)
                   )
                   .HasColumnName("COMMENTIMAGE")
                   .HasColumnType("nvarchar(max)")
                   .IsRequired(false);

            builder.Property(c => c.CommentImage)
                   .Metadata.SetValueComparer(
                       ValueComparer.CreateDefault(typeof(string[]), favorStructuralComparisons: true)
                   );

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
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系二: Comment -> Customer (多对一)
            builder.HasOne(c => c.Commenter)
                   .WithMany(cu => cu.Comments)
                   .HasForeignKey(c => c.CommenterID)
                   .OnDelete(DeleteBehavior.Restrict);

            // 关系三: Comment -> Comment (自引用，多对一)
            // 用于实现评论回复功能。一个评论可以回复另一个评论。
            builder.HasOne(c => c.ReplyToComment)
                   .WithMany(rc => rc.CommentReplies)
                   .HasForeignKey(c => c.ReplyToCommentID)
                   .OnDelete(DeleteBehavior.ClientSetNull); // 重要：防止级联删除导致问题

            // 关系四: Comment -> FoodOrder (多对一)
            builder.HasOne(c => c.FoodOrder)
                  .WithMany(fo => fo.Comments)
                  .HasForeignKey(c => c.FoodOrderID)
                  .OnDelete(DeleteBehavior.Restrict);

            // 关系五: Comment <-> Administrator (多对多，通过 Review_Comment)
            builder.HasMany(c => c.ReviewComments)
                   .WithOne(rc => rc.Comment)
                   .HasForeignKey(rc => rc.CommentID)
                   .OnDelete(DeleteBehavior.Cascade); // 当评论删除时，相关的审核记录也删除
        }
    }
}