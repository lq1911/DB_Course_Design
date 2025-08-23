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
    options.ListenAnyIP(5250); // 让外网访问这个端口
});

// 数据库上下文注册
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 启用 MVC 控制器支持
builder.Services.AddControllers();

// 注册 Repository 层
// Repository 层注入，接口在前，实现类在后
builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IAfterSaleApplicationRepository, AfterSaleApplicationRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICouponManagerRepository, CouponManagerRepository>();
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
builder.Services.AddScoped<IUserInStoreService, UserInStoreService>();

// 注册 Service 层
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();


// 添加 CORS 服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080") // Vue 前端地址
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 如果是开发环境，启用 Swagger UI 来浏览 API 接口文档
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 使用 CORS
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//骑手服务注入
builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourierRepository, CourierRepository>();

