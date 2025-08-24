using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class StoreViolationPenaltyConfig : IEntityTypeConfiguration<StoreViolationPenalty>
    {
        public void Configure(EntityTypeBuilder<StoreViolationPenalty> builder)
        {
            builder.ToTable("STORE_VIOLATION_PENALTIES");

            // --- 主键和基础属性配置 ---
            builder.HasKey(svp => svp.PenaltyID);
            builder.Property(svp => svp.PenaltyID).HasColumnName("PENALTYID").ValueGeneratedOnAdd();

            builder.Property(svp => svp.PenaltyReason).HasColumnName("PENALTYREASON").IsRequired().HasMaxLength(255);
            builder.Property(svp => svp.PenaltyTime).HasColumnName("PENALTYTIME").IsRequired();
            builder.Property(svp => svp.SellerPenalty).HasColumnName("SELLERPENALTY").HasMaxLength(50);
            builder.Property(svp => svp.StorePenalty).HasColumnName("STOREPENALTY").HasMaxLength(50);

            // --- 外键属性声明 ---
            builder.Property(svp => svp.StoreID).HasColumnName("STOREID").IsRequired();

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: StoreViolationPenalty -> Store (多对一)
            // 多个处罚记录可以关联到同一个店铺。
            builder.HasOne(svp => svp.Store)
                   .WithMany(s => s.StoreViolationPenalties)
                   .HasForeignKey(svp => svp.StoreID)
                   .OnDelete(DeleteBehavior.Restrict); // 不允许删除一个还有违规记录的店铺，以保护管理历史。这与 StoreConfig 中的配置保持一致。

            // 关系二: StoreViolationPenalty -> Supervise_ (一对多)
            // 这是实现与 Administrator 多对多关系的连接部分。一个处罚记录可以由多个管理员监管。
            builder.HasMany(svp => svp.Supervise_s)
                   .WithOne(s => s.Penalty) // 假设 Supervise_ 模型中有 public StoreViolationPenalty Penalty { get; set; }
                   .HasForeignKey(s => s.PenaltyID)
                   .OnDelete(DeleteBehavior.Cascade); // 当一个处罚记录被删除时，其所有与管理员的关联记录（在 Supervise_ 表中）也应被级联删除。
        }
    }
}
