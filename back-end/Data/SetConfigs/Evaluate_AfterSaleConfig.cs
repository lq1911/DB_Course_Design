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
            // 由于 Administrator 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(eas => eas.Admin)
                   .WithMany()
                   .HasForeignKey(eas => eas.AdminID);

            // 关系二: Evaluate_AfterSale -> AfterSaleApplication (多对一)
            // 由于 AfterSaleApplication 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(eas => eas.Application)
                   .WithMany()
                   .HasForeignKey(eas => eas.ApplicationID);
        }
    }
}