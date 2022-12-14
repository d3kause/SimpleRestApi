using Microsoft.EntityFrameworkCore;
using SimpleRestApi.Common.Databases.CodeValue.Models;

namespace SimpleRestApi.Common.Databases.CodeValue;

public sealed class CodeValueDbContext : DbContext
{
    public DbSet<CodeValueType> CodeValueTypes { get; set; }

    public CodeValueDbContext(DbContextOptions<CodeValueDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CodeValueType>().HasKey(valueType => new { valueType.Code, valueType.Value });
    }
}