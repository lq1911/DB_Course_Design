using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class SuperviseConfig : IEntityTypeConfiguration<Supervise>
    {
        public void Configure(EntityTypeBuilder<Supervise> builder)
        {
            builder.ToTable("SUPERVISE");

            // --- 主键配置：复合主键 ---
            // Supervise 表使用 AdminID 和 PenaltyID 共同构成一个唯一的复合主键。
            // 这确保了同一个管理员不能重复关联到同一个处罚记录。
            builder.HasKey(s => new { s.AdminID, s.PenaltyID });

            // --- 属性配置 ---
            builder.Property(s => s.AdminID).HasColumnName("ADMINID");
            builder.Property(s => s.PenaltyID).HasColumnName("PENALTYID");

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: Supervise -> Administrator (多对一)
            // 多个监督记录可以指向同一个管理员。
            builder.HasOne(s => s.Admin)
                   .WithMany(a => a.Supervises) // 在 Administrator 模型中，反向导航属性名为 Supervises
                   .HasForeignKey(s => s.AdminID)
                   .OnDelete(DeleteBehavior.Cascade); // 关键：当管理员被删除时，其所有监督记录也应被级联删除。

            // 关系二: Supervise -> StoreViolationPenalty (多对一)
            // 多个监督记录可以指向同一个处罚记录。
            builder.HasOne(s => s.Penalty)
                   .WithMany(svp => svp.Supervises) // 在 StoreViolationPenalty 模型中，反向导航属性名为 Supervises
                   .HasForeignKey(s => s.PenaltyID)
                   .OnDelete(DeleteBehavior.Cascade); // 关键：当处罚记录被删除时，其所有监督记录也应被级联删除。
        }
    }
}
