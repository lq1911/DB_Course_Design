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

        // public DbSet<ExampleEntity> ExampleEntities { get; set; }
    }
}