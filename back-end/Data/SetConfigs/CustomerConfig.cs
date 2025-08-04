using BackEnd.Models; // 注意：你的 Customer 类在 Demo.Models 命名空间下，请确认是否需要调整
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// 注意：你的 Customer 类在 Demo.Models 命名空间下，请确认是否需要调整
namespace BackEnd.Data.EntityConfigs
{
    // 注意：你的 Customer 类在 Demo.Models 命名空间下，这里可能需要调整 using
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CUSTOMERS");

            builder.HasKey(c => c.UserID);

            builder.Property(c => c.UserID).HasColumnName("USERID");

            builder.Property(c => c.DefaultAddress).HasColumnName("DEFAULTADDRESS").HasMaxLength(100);
            
            builder.Property(c => c.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);
            
            builder.Property(c => c.IsMember).HasColumnName("ISMEMBER").HasDefaultValue(0);

            // 配置一对一关系: Customer -> User
            // 由于 User 类中没有反向导航属性 Customer，我们使用不带参数的 WithOne()
            builder.HasOne(c => c.User)
                   .WithOne() // 明确表示 User 端没有 Customer 导航属性
                   .HasForeignKey<Customer>(c => c.UserID);
        }
    }
}