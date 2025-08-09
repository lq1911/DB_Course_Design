using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.SetConfigs
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("SELLER");

            builder.HasKey(s => s.UserID);  

            builder.Property(s => s.UserID).HasColumnName("USERID").ValueGeneratedNever();

            builder.Property(s => s.SellerRegistrationTime).HasColumnName("SELLERREGISTRATIONTIME").IsRequired();

            builder.Property(s => s.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);

            builder.Property(s => s.BanStatus).HasColumnName("BANSTATUS").IsRequired().HasMaxLength(10);

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: Seller 与 User (一对一继承关系)
            builder.HasOne(s => s.User)
                   .WithOne(u => u.Seller)
                   .HasForeignKey<Seller>(s => s.UserID)
                   .OnDelete(DeleteBehavior.Cascade); // 当 User 被删除时，其 Seller 身份也应级联删除

            // 关系二: Seller 与 Store (一对一可选关系)
            builder.HasOne(s => s.Store)
                   .WithOne(st => st.Seller)
                   .HasForeignKey<Store>(st => st.SellerID)
                   .OnDelete(DeleteBehavior.Cascade); // 当 Seller 被删除时，其拥有的 Store 也应级联删除
        }
    }
}