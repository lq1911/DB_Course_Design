using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Data.SetConfigs;

namespace BackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 应用所有实体配置
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new SellerConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfig());
            
            // 为Customer配置表名和列名映射（匹配Oracle数据库中的大写命名）
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");
                entity.HasKey(c => c.UserID);
                entity.Property(c => c.UserID).HasColumnName("USERID");
                entity.Property(c => c.DefaultAddress).HasColumnName("DEFAULTADDRESS").HasMaxLength(100);
                entity.Property(c => c.ReputationPoints).HasColumnName("REPUTATIONPOINTS").HasDefaultValue(0);
                entity.Property(c => c.IsMember).HasColumnName("ISMEMBER").HasDefaultValue(0);
                entity.HasOne(c => c.User).WithOne().HasForeignKey<Customer>(c => c.UserID);
            });
            
        }
    }
}
