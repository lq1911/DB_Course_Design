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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();

// 注册 Service 层
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();

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
