using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Data.SetConfigs;
using BackEnd.Data.EntityConfigs;

namespace BackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Accept_Task> Accept_Tasks { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AfterSaleApplication> AfterSaleApplications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Customer_Coupon> Customer_Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryComplaint> DeliveryComplaints { get; set; }
        public DbSet<DeliveryTask> DeliveryTasks { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Evaluate_AfterSale> Evaluate_AfterSales { get; set; }
        public DbSet<Evaluate_Complaint> Evaluate_Complaints { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
        public DbSet<FavoritesFolder> FavoritesFolders { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }
        public DbSet<Menu_Dish> Menu_Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Publish_Task> Publish_Tasks { get; set; }
        public DbSet<Review_Comment> Review_Comments { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreViolationPenalty> StoreViolationPenalties { get; set; }
        public DbSet<Supervise_> Supervise_s { get; set; } // 注意：建议修改类名 Supervise_ 为更规范的命名


        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    // 为每一个实体类应用对应的 Fluent API 配置类
            modelBuilder.ApplyConfiguration(new Accept_TaskConfig());
            modelBuilder.ApplyConfiguration(new AdministratorConfig());
            modelBuilder.ApplyConfiguration(new AfterSaleApplicationConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new CouponConfig());
            modelBuilder.ApplyConfiguration(new CourierConfig());
            modelBuilder.ApplyConfiguration(new Customer_CouponConfig());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new DeliveryComplaintConfig());
            modelBuilder.ApplyConfiguration(new DeliveryTaskConfig());
            modelBuilder.ApplyConfiguration(new DishConfig());
            modelBuilder.ApplyConfiguration(new Evaluate_AfterSaleConfig());
            modelBuilder.ApplyConfiguration(new Evaluate_ComplaintConfig());
            modelBuilder.ApplyConfiguration(new FavoriteItemConfig());
            modelBuilder.ApplyConfiguration(new FavoritesFolderConfig());
            modelBuilder.ApplyConfiguration(new FoodOrderConfig());
            modelBuilder.ApplyConfiguration(new Menu_DishConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new Publish_TaskConfig());
            modelBuilder.ApplyConfiguration(new Review_CommentConfig());
            modelBuilder.ApplyConfiguration(new SellerConfig());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfig());
            modelBuilder.ApplyConfiguration(new ShoppingCartItemConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());
            modelBuilder.ApplyConfiguration(new StoreViolationPenaltyConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(modelBuilder); // 不要忘记调用基类方法
        }

    }
}
