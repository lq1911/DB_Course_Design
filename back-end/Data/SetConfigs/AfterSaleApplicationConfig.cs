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

            builder.Property(asa => asa.ApplicationID).HasColumnName("APPLICATIONID");

            builder.Property(asa => asa.Description).HasColumnName("DESCRIPTION").IsRequired().HasMaxLength(255);
            
            builder.Property(asa => asa.ApplicationTime).HasColumnName("APPLICATIONTIME").IsRequired();
            
            builder.Property(asa => asa.CustomerID).HasColumnName("CUSTOMERID").IsRequired();
            
            builder.Property(asa => asa.OrderID).HasColumnName("ORDERID").IsRequired();
            
            // 配置多对一关系: AfterSaleApplication -> Customer
            // 由于 Customer 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(asa => asa.Customer)
                   .WithMany()
                   .HasForeignKey(asa => asa.CustomerID);

            // 配置多对一关系: AfterSaleApplication -> Order
            // 由于 Order 类中没有反向导航属性，我们使用不带参数的 WithMany()
            builder.HasOne(asa => asa.Order)
                   .WithMany()
                   .HasForeignKey(asa => asa.OrderID);
        }
    }
}