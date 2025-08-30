using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class AfterSaleApplicationConfig : IEntityTypeConfiguration<AfterSaleApplication>
    {
        public void Configure(EntityTypeBuilder<AfterSaleApplication> builder)
        {
            builder.ToTable("AFTER_SALE_APPLICATIONS");

            builder.HasKey(asa => asa.ApplicationID);

            builder.Property(asa => asa.ApplicationID).HasColumnName("APPLICATIONID").ValueGeneratedOnAdd(); // 明确指定自增主键

            builder.Property(asa => asa.Description).HasColumnName("DESCRIPTION").IsRequired().HasMaxLength(255);

            builder.Property(asa => asa.ApplicationTime).HasColumnName("APPLICATIONTIME").IsRequired();

            builder.Property(asa => asa.AfterSaleState).HasColumnName("AFTERSALESTATE").IsRequired().HasConversion<string>().HasMaxLength(50);

            builder.Property(asa => asa.ProcessingResult).HasColumnName("PROCESSINGRESULT").IsRequired(false).HasMaxLength(255);

            builder.Property(asa => asa.ProcessingReason).HasColumnName("PROCESSINGREASON").IsRequired(false).HasMaxLength(255);

            builder.Property(asa => asa.ProcessingRemark).HasColumnName("PROCESSINGREMARK").IsRequired(false).HasMaxLength(255);

            builder.Property(asa => asa.OrderID).HasColumnName("ORDERID").IsRequired();

            // ---------------------------------------------------------------
            // 配置外键关系
            // ---------------------------------------------------------------

            // 关系一: AfterSaleApplication -> FoodOrder (多对一)
            builder.HasOne(asa => asa.Order)
                   .WithMany(fo => fo.AfterSaleApplications)
                   .HasForeignKey(asa => asa.OrderID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}