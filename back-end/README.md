# 后端接口文档（ASP.NET Core）

本项目是一个基于 ASP.NET Core 和 Entity Framework Core 构建的 RESTful Web API，提供用户管理接口，支持 Oracle 数据库。

---

## 🏗️ 技术栈

- ASP.NET Core 7+
- Entity Framework Core
- Oracle 数据库（Oracle.EntityFrameworkCore 提供支持）
- RESTful API 风格
- JSON 配置（`appsettings.json`）

---

## 📁 目录结构简介

| 目录/文件                     | 描述                                         |
|------------------------------|----------------------------------------------|
| `Controllers/`               | 控制器，定义 Web API 接口(如 UserController)   |
| `Models/`                    | 实体模型类，例如 `User.cs`                    |
| `Data/`                      | 数据库上下文 `AppDbContext` 和实体配置         |
| `Program.cs`                 | 应用程序入口，配置服务和中间件                  |
| `appsettings.json`           | 主配置文件，包含数据库连接字符串等信息          |
| `BackEnd.http`               | HTTP 测试脚本，可用于 VS Code 测试接口         |

---

## 如何运行项目
1. **运行项目**：

    ```bash
    dotnet run
    ```

2. **访问接口**：

    访问浏览器或使用 Postman 请求：

    ```
    http://<服务器IP>:5250/api/user
    ```

---

## 🔌 数据库配置

在 `appsettings.json` 中配置 Oracle 数据库连接：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=your_user;Password=your_password;Data Source=your_host:1521/your_service"
  }
}
```

## 开发流程
1. Models 模块：定义数据结构（实体模型）
定义与数据库表对应的数据实体类，字段名称需与数据库字段一一对应。
```cs
public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string FullName { get; set; }
    public string Avatar { get; set; }
    public DateTime? Birthday { get; set; }
    public DateTime AccountCreationTime { get; set; }
    public bool IsProfilePublic { get; set; }
}
```
2. Data 模块：数据库访问层
管理数据库连接、映射模型与数据表，提供对数据库操作的上下文入口。
在 AppDbContext.cs 中注册映射关系，下面代码为注册 Users 表和类的映射关系
```cs
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

public DbSet<User> Users { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
     modelBuilder.ApplyConfiguration(new UserConfig());
}
```
在 EntityConfigs 中实现具体的注册
```cs
namespace BackEnd.Data.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("USERS");

            entity.HasKey(e => e.UserID);

            entity.Property(e => e.UserID).HasColumnName("USERID");
            entity.Property(e => e.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15);
            entity.Property(e => e.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(10);
            entity.Property(e => e.PhoneNumber).HasColumnName("PHONENUMBER").IsRequired();
            entity.Property(e => e.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(30);
            entity.Property(e => e.Gender).HasColumnName("GENDER").HasMaxLength(2);
            entity.Property(e => e.FullName).HasColumnName("FULLNAME").HasMaxLength(6);
            entity.Property(e => e.Avatar).HasColumnName("AVATAR").HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnName("BIRTHDAY");
            entity.Property(e => e.AccountCreationTime).HasColumnName("ACCOUNTCREATIONTIME").IsRequired();
            entity.Property(e => e.IsProfilePublic).HasColumnName("ISPROFILEPUBLIC");
        }
    }
}
```
3. Controllers 模块：业务控制层（Web API 接口）
接收客户端请求，调用 DbContext 操作数据库，返回响应，实现增删改查基本功能。
```cs
namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        
        // 获取用户
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        
        // 获取指定id的用户
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
  
        // 创建用户
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
        }

        // 更新用户
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Email = updatedUser.Email;
            user.Gender = updatedUser.Gender;
            user.FullName = updatedUser.FullName;
            user.Avatar = updatedUser.Avatar;
            user.Birthday = updatedUser.Birthday;
            user.AccountCreationTime = updatedUser.AccountCreationTime;
            user.IsProfilePublic = updatedUser.IsProfilePublic;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // 删除用户
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
```