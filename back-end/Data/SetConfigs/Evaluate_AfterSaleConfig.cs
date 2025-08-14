using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class Evaluate_AfterSaleConfig : IEntityTypeConfiguration<Evaluate_AfterSale>
    {
        public void Configure(EntityTypeBuilder<Evaluate_AfterSale> builder)
        {
            builder.ToTable("EVALUATE_AFTER_SALE");

            // 设置复合主键
            builder.HasKey(eas => new { eas.AdminID, eas.ApplicationID });

            // 配置列属性和名称
            builder.Property(eas => eas.AdminID).HasColumnName("ADMINID");

            builder.Property(eas => eas.ApplicationID).HasColumnName("APPLICATIONID");

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: Evaluate_AfterSale -> Administrator (多对一)
            builder.HasOne(eas => eas.Admin)
                   .WithMany(a => a.EvaluateAfterSales)
                   .HasForeignKey(eas => eas.AdminID)
                   .OnDelete(DeleteBehavior.Restrict); // 防止直接通过中间实体删除主实体


            // 关系二: Evaluate_AfterSale -> AfterSaleApplication (多对一)
            builder.HasOne(eas => eas.Application)
                   .WithMany(asa => asa.EvaluateAfterSales)
                   .HasForeignKey(eas => eas.ApplicationID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}