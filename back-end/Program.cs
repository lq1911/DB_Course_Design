using BackEnd.Data;
using BackEnd.Repositories;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration; // 应用程序所有配置信息的集合

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
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // 配置 JSON 序列化为驼峰命名（小驼峰）
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // 验证用于签名 Token 的密钥
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT密钥 'Jwt:Key' 未在配置中设置"))),

        // 不验证发行人 (Issuer)
        ValidateIssuer = false,

        // 不验证接收方 (Audience)
        ValidateAudience = false,

        // 验证Token的生命周期
        ValidateLifetime = true,

        // 允许的服务器时间偏移量，设置为零表示不容忍任何时间误差
        ClockSkew = TimeSpan.Zero
    };
});

// 添加授权服务
builder.Services.AddAuthorization();

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
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();

// 注册 Service 层
builder.Services.AddScoped<IUserInStoreService, UserInStoreService>();
builder.Services.AddScoped<IUserCheckoutService, UserCheckoutService>();
builder.Services.AddScoped<IUserHomepageService, UserHomepageService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IUserDebugService, UserDebugService>();
builder.Services.AddScoped<IUserPlaceOrderService, UserPlaceOrderService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IEvaluate_AfterSaleService, Evaluate_AfterSaleService>();
builder.Services.AddScoped<IEvaluate_DeliveryComplaintService, Evaluate_DeliveryComplaintService>();
builder.Services.AddScoped<ISupervise_Service, Supervise_Service>();
builder.Services.AddScoped<IReview_CommentService, Review_CommentService>();
builder.Services.AddScoped<IMerchantService, MerchantService>(); ;
builder.Services.AddHostedService<MonthlyCommissionResetService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IMerchantInformationService, MerchantInformationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IDeliveryTaskService, DeliveryTaskService>();
builder.Services.AddScoped<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAfterSaleService, AfterSaleService>();
builder.Services.AddScoped<ICreateApplicationService, CreateApplicationService>();
builder.Services.AddScoped<ICreateComplaintService, CreateComplaintService>();
builder.Services.AddScoped<IGeoHelper, GeoHelper>();
var app = builder.Build();

// 如果是开发环境，启用 Swagger UI 来浏览 API 接口文档
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 启用 CORS
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
