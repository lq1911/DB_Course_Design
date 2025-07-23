var builder = WebApplication.CreateBuilder(args);

// ===============================
// 注册服务（Service）、数据库上下文（DbContext）、仓储（Repository）
// ===============================

// 示例依赖注入（使用 Oracle 数据库连接）
// builder.Services.AddDbContext<ExampleContext>(options =>
//     options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// 注入业务逻辑和数据访问服务
// builder.Services.AddScoped<IExampleRepository, ExampleRepository>();
// builder.Services.AddScoped<IExampleService, ExampleService>();

// 注册控制器和 Swagger 文档
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===============================
// 配置中间件管道
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
