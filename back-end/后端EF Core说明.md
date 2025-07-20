# Entity Framework Core使用说明
本项目中选择使用了**Entity Framework Core**的ORM框架来进行数据访问。其使用 LINQ 查询，内部可以自动生成 SQL（即可以**不需要后端人员手写SQL**），但**也支持手写SQL** 。

**具体操作流程为**：
### 1. 安装必要的 NuGet 包(此步骤已完成，即obj文件夹中内容)

### 2.  创建你的 DbContext 类（放在 Data/ 文件夹下）
例：
```csharp
// Data/ExampleDbContext.cs
using Microsoft.EntityFrameworkCore;
using exampleProject.Models;

namespace exampleProject.Data
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options) {}

        // 为每个模型创建 DbSet 属性
        public DbSet<ExampleModel> ExampleModels { get; set; }
    }
}
```
> 具体代码参考data/文件夹下的example。

### 3. 修改 appsettings.json，添加 Oracle 数据库连接字符串
例：
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=your_user;Password=your_password;Data Source=your_oracle_source"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
> 具体代码参考现已有的appsettings.json文件。

Oracle 的 Data Source 格式示例：
```nginx
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))
```
### 4. 在 Program.cs 中注册 DbContext
例：
```csharp
builder.Services.AddDbContext<ExampleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));
```
> 参考现有的Program.cs中的注释
### 5. 创建 Model 类（放在 Models 文件夹中）
例：
```csharp
// Models/ExampleModel.cs
namespace exampleProject.Models
{
    public class ExampleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
```
> 具体参考Models/ 文件夹下的example
### 6. 创建并应用数据库迁移
如果你希望 EF Core 自动创建数据库结构，你可以使用 Migration（迁移）机制。
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
> 这会生成并运行建表 SQL，Oracle 会创建对应的数据表。前提是你已连接到 Oracle 并具有权限。

### 7. 使用 DbContext 进行数据库操作（在 Repository 或 Service 中）
例：
```csharp
// Repositories/ExampleRepository.cs
public class ExampleRepository
{
    private readonly ExampleDbContext _context;

    public ExampleRepository(ExampleDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExampleModel>> GetAllAsync()
    {
        return await _context.ExampleModels.ToListAsync();
    }
}
```

