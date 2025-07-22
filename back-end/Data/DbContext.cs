// ================================
// Data/ExampleContext.cs
// ================================

using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
    // EF Core DbContext 类
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options) { }

        // 为每个模型创建 DbSet 属性
        public DbSet<ExampleEntity> ExampleEntities { get; set; }
    }
}