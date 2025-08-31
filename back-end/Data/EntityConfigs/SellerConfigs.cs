using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> entity)
        {
            entity.ToTable("SELLERS");
            entity.HasKey(s => s.UserID);
            
            // 映射列名到Oracle数据库中的大写列名，明确指定所有数据类型
            entity.Property(s => s.UserID).HasColumnName("USERID").HasColumnType("NUMBER(10,0)");
            
            // 明确指定数据类型以避免转换错误
            entity.Property(s => s.SellerRegistrationTime).HasColumnName("SELLERREGISTRATIONTIME").IsRequired().HasColumnType("TIMESTAMP(7)");
            
            entity.Property(s => s.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasColumnType("NUMBER(10,0)").HasDefaultValue(0);
            entity.Property(s => s.BanStatus).HasColumnName("BANSTATUS").IsRequired().HasMaxLength(10).HasDefaultValue("Normal");
            // 暂时注释掉，因为数据库中没有这个字段
            // entity.Property(s => s.AfterSaleApplicationID).HasColumnName("AFTERSALEAPPLICATIONID");
            
            entity.HasOne(s => s.User).WithOne().HasForeignKey<Seller>(s => s.UserID);
            // entity.HasOne(s => s.AfterSaleApplication)
            //        .WithMany()
            //        .HasForeignKey(s => s.AfterSaleApplicationID)
            //        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}