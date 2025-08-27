using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.EntityConfigs
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("SELLERS");

            builder.HasKey(s => s.UserID);

            builder.Property(s => s.UserID).HasColumnName("USERID").ValueGeneratedNever();

            builder.Property(s => s.SellerRegistrationTime).HasColumnName("SELLERREGISTRATIONTIME").IsRequired();

            builder.Property(s => s.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);

            builder.Property(s => s.BanStatus).HasColumnName("BANSTATUS").IsRequired().HasConversion<string>().HasMaxLength(10);

            // ---------------------------------------------------------------
            // 关系配置
            // ---------------------------------------------------------------

            // 关系一: Seller -> User (一对一)
            builder.HasOne(s => s.User)
                   .WithOne(u => u.Seller)
                   .HasForeignKey<Seller>(s => s.UserID)
                   .OnDelete(DeleteBehavior.Cascade); // 当 User 被删除时，其 Seller 身份也应级联删除
        }
    }
}