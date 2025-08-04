using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> entity)
        {
            entity.ToTable("SELLERS");
            entity.HasKey(s => s.UserID);
            
            // 映射列名到Oracle数据库中的大写列名
            entity.Property(s => s.UserID).HasColumnName("USERID");
            
            // 移除DATE类型指定，让Entity Framework自动处理
            entity.Property(s => s.SellerRegistrationTime).HasColumnName("SELLERREGISTRATIONTIME").IsRequired();
            
            entity.Property(s => s.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);
            entity.Property(s => s.BanStatus).HasColumnName("BANSTATUS").IsRequired().HasMaxLength(10).HasDefaultValue("Normal");
            entity.Property(s => s.AfterSaleApplicationID).HasColumnName("AFTERSALEAPPLICATIONID");
            
            entity.HasOne(s => s.User).WithOne().HasForeignKey<Seller>(s => s.UserID);
            entity.HasOne(s => s.AfterSaleApplication)
                   .WithMany()
                   .HasForeignKey(s => s.AfterSaleApplicationID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}