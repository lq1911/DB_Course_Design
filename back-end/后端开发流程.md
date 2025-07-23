# 后端开发流程介绍文档

## 具体开发流程
### 一. 建表与关系设计
**说明**：此步骤用于在数据库中建表以及建立表之间的关系

**具体操作**：

**1**. **创建实体类(Models/)**
- 实体类的创建对应着数据库里表的创建，实体类与表属于映射关系。
- 在文件夹Models/下进行操作。
- 创建格式参考例：
  ```csharp
  // Users.cs
  using System.ComponentModel.DataAnnotations;//使用数据注释库来进行属性约束

  namespace back_end.Models
  {
    public class User
    { 
      [Key] // [Annotation]的方式为数据注释方式，key表示此属性为主键
      public int UserID { get; set; }

      [Required] // Require 表示此属性不能为空值
      [MaxLength(15)] // 最大长度约束
      public string Username { get; set; } = null!;

      [Required]
      [MaxLength(10)]
      public string Password { get; set; } = null!;

      [Required]
      public long PhoneNumber { get; set; }

      [Required]
      [MaxLength(30)]
      public string Email { get; set; } = null!;

      [MaxLength(2)]
      public string? Gender { get; set; }

      [MaxLength(6)]
      public string? FullName { get; set; }

      [MaxLength(255)]
      public string? Avatar { get; set; }

      public DateTime? Birthday { get; set; }

      [Required]
      public DateTime AccountCreationTime { get; set; }

      public int IsProfilePublic { get; set; } = 0;
    }
  }
  ```

**2**. **完善表的约束条件(Data/EntityConfigs/)**
- 创建实体类后需要到Data/EntityConfigs/文件夹里创建对应表的约束条件文件
- 实际上是数据注释约束的完善，称为*Fluent API*，会将数据注释的约束覆盖，它的功能比数据注释更强大。
- 创建格式参考例
  ```csharp
  // UserConfig.cs
  using Microsoft.EntityFrameworkCore; // 使用EFCore库，提供Fluent API
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using back_end.Models;

  namespace BackEnd.Data.EntityConfigs
  {
    public class UserConfig : IEntityTypeConfiguration<User>
    {
      public void Configure(EntityTypeBuilder<User> entity)
      {
        entity.ToTable("USERS");

        entity.HasKey(e => e.UserID); // 主键约
        entity.Property(e => e.UserID).HasColumnName("USERID"); // 类属性的列命名，特别注意命名要和数据库SQL中的属性命名一致

        entity.Property(e => e.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(15); // 类属性的列命名、IsRequired 属性不能为空值、有最大长度15
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
  更多需求请在https://learn.microsoft.com/zh-cn/ef/core/modeling/ 中查询

**3**. **DbContext注册(Data/)**
- 为每个实体类在Data/文件夹下创建注册文件
- 格式参考例
  ```csharp
  // UserDbContext.cs
  using Microsoft.EntityFrameworkCore; // 使用EFCore库
  using back_end.Models;
  using back_end.Data.EntityConfigs;

  namespace back_end.Data
  {
    public class AppDbContext : DbContext
    {
      // UserDbContext 类必须公开具有DbContextOptions<ApplicationDbContext> 参数的公共构造函数。
      // 这是将 AddDbContext 的上下文配置传递到 DbContext 的方式。
      // 这部分代码是固定的。
      public UserDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 


      // 为实体类型创建 DbSet<TEntity> 属性，在数据库上下文中注册实体
      // public DbSet<TEntity> TEntity { get; set; }
      public DbSet<User> Users { get; set; }


      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        // 为此实体注册约束
        // modelBuilder.ApplyConfiguration(new TEntityConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
      }
    }
  }
  ```

**4**. **实现表增删改查基本操作(Respositories/)**
- 为每个实体类在Respositories/文件夹下创建**仓储接口文件**和**仓储实现文件**
- 仓储层(Respositories)用于封装对数据库的**增删改查**操作，是对数据库直接进行操作的层次。
- 格式参考例
  ```csharp
  // IUserRespository 接口文件
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using back_end.Models; // 引入模型实体

  namespace back_end.Repositories.Interfaces
  {
    // 用户仓储接口，定义用户数据访问的抽象操作。
    // 实现类由 UserRepository 提供，接口用于解耦与服务层之间的依赖。
    public interface IUserRepository
    {
      // 获取所有用户。
      Task<IEnumerable<User>> GetAllUsersAsync();
      // 根据用户ID获取用户。
      Task<User?> GetUserByIdAsync(int id);
      // 添加一个新用户。
      Task<User> AddUserAsync(User user);
      // 更新指定用户信息。
      Task<bool> UpdateUserAsync(User user);
      // 删除指定ID的用户。
      Task<bool> DeleteUserAsync(int id);
      // 判断用户是否存在（通过 ID 查询）
      public async Task<bool> ExistsAsync(int id)
    }
  }

  // UserRespository 实现文件
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.EntityFrameworkCore;
  using back_end.Data;      // 引入 DbContext 所在命名空间
  using back_end.Models;    // 引入实体模型（如 User）所在命名空间
  using back_end.Repositories.Interfaces; // 引入接口定义所在命名空间

  namespace back_end.Repositories
  {
    // 实现用户仓储接口
    public class UserRepository : IUserRepository
    {
      private readonly ApplicationDbContext _context;

      // 构造函数：注入 ApplicationDbContext（数据库上下文），每个文件都必须
      public UserRepository(ApplicationDbContext context)
      {
        _context = context;
      }

      // 异步查询，获取所有用户的数据（查）
      public async Task<IEnumerable<User>> GetAllAsync()
      {
        // 使用 EF Core 的 ToListAsync 查询所有 User 表数据
        return await _context.Users.ToListAsync();
      }

      // 根据用户 ID 异步查询，获取单个用户对象（查）
      public async Task<User> GetByIdAsync(int id)
      {
        // 使用 EF Core 的 FindAsync 查询主键为 id 的用户
        return await _context.Users.FindAsync(id);
      }

      // 异步增加用户数据（增）
      public async Task AddAsync(User user)
      {
        // 添加用户到上下文
        await _context.Users.AddAsync(user);
        // 保存更改到数据库
        await _context.SaveChangesAsync();
      }

      // 更改用户数据（改）
      public async Task UpdateAsync(User user)
      {
        // 将该用户标记为“已修改”，EF 会在 SaveChanges 时生成 UPDATE SQL
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
      }

      // 删除用户（删）
       public async Task DeleteAsync(User user)
      {
        // 从上下文中删除用户
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
      }

      // 判断用户是否存在（通过 ID 查询）
      public async Task<bool> ExistsAsync(int id)
      {
        // 使用 AnyAsync 判断是否存在满足条件的记录
        return await _context.Users.AnyAsync(u => u.Id == id);
      }
    }
  }
  ```

**5**. **将DbContext和Respository注入运行项目(Program.cs)**
- 完成以上步骤之后，要将数据库上下文和仓储层在运行文件中注册
```csharp
// Program.cs

//...初始设置代码省略...

// 数据库上下文注册
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// 注册仓储层(接口文档和实现文档)
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ...省略...
  
```

### 二. 面向接口开发流程 

**1**. **创建DTO(DTOs/)**
- 对于每个前端数据需求，在DTOs/文件夹下创建契合前端数据需求的DTO类
- DTO类定义了**前后端传输用的数据结构**，能够避免直接传输数据库实体而暴露数据库实体，也能够明确响应前端数据需求以返回准确的数据
- 格式参考例：
  ```csharp
  // DTOs/UserDto.cs
  namespace back_end.DTOs
  {
    // 用于用户信息的读取（如返回给前端）
    public class UserDto
    {
        public int Id { get; set; }              // 用户唯一ID
        public string Username { get; set; }     // 用户名
        public string Email { get; set; }        // 邮箱
    }

    // 用于新增用户时提交的数据
    public class CreateUserDto
    {
        public string Username { get; set; }     // 用户名
        public string Email { get; set; }        // 邮箱
        public string Password { get; set; }     // 密码
    }

    // 用于更新用户数据
    public class UpdateUserDto
    {
        public string Email { get; set; }        // 可更新的字段：邮箱
        public string Password { get; set; }     // 可更新的字段：密码
    }
  }
  ```

**2**. **业务逻辑开发(Services/)**
- 对于每个业务逻辑需求，在Services/文件夹下创建**业务接口文件**和**业务实现文件**
- 业务逻辑层负责**处理与业务相关的操作**，即对数据进行业务条件筛选，并调用仓储层的**增删改查操作**对数据进行处理，然后将多个数据处理步骤封装成一个逻辑单元以供Controller调用。
- 比如：对好评率高于80%的商家进行随机首页推送，这就是一个业务逻辑。
- 格式参考例
  ```csharp
  // Services/IUserService.cs
  // 现在仅以实现最简单的增删改查业务逻辑为例
  using back_end.DTOs;

  namespace back_end.Services
  {
    public interface IUserService
    {
      Task<IEnumerable<UserDto>> GetAllUsersAsync();                 // 获取所有用户
      Task<UserDto> GetUserByIdAsync(int id);                        // 根据ID获取用户
      Task<UserDto> CreateUserAsync(CreateUserDto dto);             // 创建新用户
      Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);        // 更新用户
      Task<bool> DeleteUserAsync(int id);                           // 删除用户
    }
  }

  // Services/UserService.cs
  using back_end.DTOs;
  using back_end.Models;
  using back_end.Repositories;

  namespace back_end.Services
  {
    public class UserService : IUserService
    {
      private readonly IUserRepository _userRepository;

      // 通过构造函数注入仓储层，这是能调用仓储层操作的关键
      public UserService(IUserRepository userRepository)
      {
        _userRepository = userRepository;
      }

      public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
      {
        // 调用仓储层的获取所有用户数据的操作
        var users = await _userRepository.GetAllAsync();
        // 将实体类映射为 DTO，即获取需要传输至前端的数据
        return users.Select(u => new UserDto
        {
          Id = u.Id,
          Username = u.Username,
          Email = u.Email
        });
      }
      
      // 以下业务实现同理
      public async Task<UserDto> GetUserByIdAsync(int id)
      {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null; // 如果用户为空，则返回空

        return new UserDto
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email
        };
      }

      public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
      {
        var user = new User // 创建新的用户数据实例，将从前端接收的dto数据传入
        {
          Username = dto.Username,
          Email = dto.Email,
          Password = dto.Password
        };

        await _userRepository.AddAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
      }

      public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
      {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        user.Email = dto.Email ?? user.Email; // 如果 dto.Email 不为 null，则将其赋值给 user.Email。如果 dto.Email 为 null，则保持 user.Email 原来的值不变。
        user.Password = dto.Password ?? user.Password;

        return await _userRepository.UpdateAsync(user);
      }

      public async Task<bool> DeleteUserAsync(int id)
      {
        var user = await _userRepository.GetByIdAsync(id);// 寻找需要删除的用户
        if (user == null) return false;

        return await _userRepository.DeleteAsync(user);// 删除目标用户
      }
    }
  }
  ```
**3**. **创建响应请求控制器(Controllers/)**
- 对于每个前端请求和对应的业务逻辑，在Controller\文件夹下创建对应的控制器文件
- Controller 负责接收前端 HTTP 请求，调用 Service 层，返回 JSON 结果
- 返回方法表：
  | 方法                  | 说明            |
  | ------------------- | ------------- |
  | `Ok(object)`        | 返回 200 + 数据   |
  | `CreatedAtAction()` | 返回 201 + 路由信息 |
  | `NotFound()`        | 返回 404        |
  | `BadRequest()`      | 返回 400        |
  | `NoContent()`       | 返回 204（无内容）   |
- 格式参考例
  ```csharp
  // Controllers/UserController.cs
  using Microsoft.AspNetCore.Mvc;
  using MyApp.DTOs;
  using MyApp.Services;

  namespace MyApp.Controllers
  {
    [ApiController]
    [Route("api/[controller]")] // 路由：api/user
    public class UserController : ControllerBase
    {
      private readonly IUserService _userService;

      // 构造函数注入业务层，这是Controller能够调用业务层操作的关键
      public UserController(IUserService userService)
      {
        _userService = userService;
      }

      // GET: api/user/GetAll
      [HttpGet("GetAll")] // 配置API接口，这样前端调用接口的时候的格式应该是  '[地址]:[端口]/api/User/GetAll'，以下同理
      public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
      {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users); // 200 返回用户数据列表
      }

      // GET: api/user/{id}
      [HttpGet("{id}")] // '[地址]:[端口]/api/User/{id}'
      public async Task<ActionResult<UserDto>> GetUser(int id)
      {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound(); // 404
        return Ok(user); // 204 返回用户数据
      }

      // POST: api/user/Add
      [HttpPost("Add")] // '[地址]:[端口]/api/User/Add'
      public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
      {
        var user = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user); // 返回 201 + 路由信息
      }

      // PUT: api/user/{id}
      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
      {
        var result = await _userService.UpdateUserAsync(id, dto);
        if (!result) return NotFound(); // 404
        return NoContent(); // 204
      }

      // DELETE: api/user/{id}
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteUser(int id)
      {
        var result = await _userService.DeleteUserAsync(id);
        if (!result) return NotFound(); // 404
        return NoContent(); // 204
      }
    }
  }
  ```

**4**. **将注入运行项目(/Program.cs)**
- 完成以上步骤之后，要将在运行文件中注册
```csharp
// Program.cs

//...初始设置代码省略...

// 添加控制器服务，若有则无需添加
builder.Services.AddControllers();

// 注册业务层(接口文档和实现文档)
builder.Services.AddScoped<IUserService, UserService>();

// 映射控制器路由，若有则无需添加
app.MapControllers();

// ...省略...
  
```

**5**. **API地址**(前端开发人员参考)
- 在现有的Program.cs中，已经通过 Kestrel 服务器显示配置了监听端口：
  ```csharp
  builder.WebHost.ConfigureKestrel(options =>
  {
    options.ListenAnyIP(5250); // 让外网访问这个端口
  });
  ```
  即端口是:5250
  而访问地址是服务器外网地址:113.44.82.210
  则在`axios`中调用接口的方法是：
  ```javascript
  import axios from 'axios';

  // 获取用户数据
  axios.get('https://113.44.82.210:5250/api/User/GetAll')
    .then(response => {
      console.log(response.data);
    });

  // 添加用户
  axios.post('https://113.44.82.210:5250/api/User/Add', {
    name: '张三',
    email: 'zhangsan@example.com',
    website: 'https://zhangsan.cn'
  })
  .then(response => {
    console.log('添加成功');
  });
  ```



