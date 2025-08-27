using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace BackEnd.Data.SetConfigs // 注意：你的原始文件命名空间是 EntityConfigs，我改回了 SetConfigs 以匹配你的项目结构
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

                     builder.Property(c => c.Rating).HasColumnName("RATING").IsRequired(false);

                     // ------------------- 这是核心修改部分 -------------------
                     // CommentImage 现在是一个普通的 string 属性，
                     // 我们只需要为它配置列名和最大长度即可。
                     // 之前所有复杂的 HasConversion 和 SetValueComparer 配置都已移除。
                     builder.Property(c => c.CommentImage)
                            .HasColumnName("COMMENTIMAGE")
                            .HasMaxLength(1000) // 使用 HasMaxLength 来生成 NVARCHAR2(1000)
                            .IsRequired(false);
                     // ------------------- 修改结束 -------------------

                     // 配置评论类型
                     builder.Property(c => c.CommentType)
                            .HasColumnName("COMMENTTYPE")
                            .IsRequired()
                            .HasConversion<int>(); // 枚举存储为整数

                     builder.Property(c => c.ReplyToCommentID).HasColumnName("REPLYTOCOMMENTID").IsRequired(false);

                     builder.Property(c => c.StoreID).HasColumnName("STOREID").IsRequired(false);

                     builder.Property(c => c.FoodOrderID).HasColumnName("FOODORDERID").IsRequired(false);

                     builder.Property(c => c.CommenterID).HasColumnName("COMMENTERID").IsRequired();

                     // ---------------------------------------------------------------
                     // 配置外键关系 (这部分保持不变)
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
                     builder.HasOne(c => c.ReplyToComment)
                            .WithMany(rc => rc.CommentReplies)
                            .HasForeignKey(c => c.ReplyToCommentID)
                            .OnDelete(DeleteBehavior.SetNull);

                     // 关系四: Comment -> FoodOrder (多对一)
                     builder.HasOne(c => c.FoodOrder)
                           .WithMany(fo => fo.Comments)
                           .HasForeignKey(c => c.FoodOrderID)
                           .OnDelete(DeleteBehavior.Cascade);
              }
       }
}