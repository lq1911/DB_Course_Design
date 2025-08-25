using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using BackEnd.Repositories;
using BackEnd.Services.Interfaces;
using BackEnd.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5250); // 修改为前端期望的端口
});

// 数据库上下文注册
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
    .LogTo(Console.WriteLine, LogLevel.Information)); // 添加SQL日志

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 启用 MVC 控制器支持
builder.Services.AddControllers();

// 添加 CORS 支持
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 注册 Repository 层
builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IAfterSaleApplicationRepository, AfterSaleApplicationRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<ICourierRepository, CourierRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDeliveryComplaintRepository, DeliveryComplaintRepository>();
builder.Services.AddScoped<IDeliveryTaskRepository, DeliveryTaskRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IEvaluate_AfterSaleRepository, Evaluate_AfterSaleRepository>();
builder.Services.AddScoped<IEvaluate_ComplaintRepository, Evaluate_ComplaintRepository>();
builder.Services.AddScoped<IFavoriteItemRepository, FavoriteItemRepository>();
builder.Services.AddScoped<IFavoritesFolderRepository, FavoritesFolderRepository>();
builder.Services.AddScoped<IFoodOrderRepository, FoodOrderRepository>();
builder.Services.AddScoped<IMenu_DishRepository, Menu_DishRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IReview_CommentRepository, Review_CommentRepository>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreViolationPenaltyRepository, StoreViolationPenaltyRepository>();
builder.Services.AddScoped<ISupervise_Repository, Supervise_Repository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 注册 Service 层
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IMerchantInformationService, MerchantInformationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IDeliveryTaskService, DeliveryTaskService>();
builder.Services.AddScoped<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAfterSaleService, AfterSaleService>();

var app = builder.Build();

// 如果是开发环境，启用 Swagger UI 来浏览 API 接口文档
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // 注释掉HTTPS重定向，因为前端使用HTTP

// 启用 CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
