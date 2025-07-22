using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Repositories;
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
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 注册 Service 层
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// 如果是开发环境，启用 Swagger UI 来浏览 API 接口文档
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 使用 Controllers 路由
app.MapControllers();

app.Run();
