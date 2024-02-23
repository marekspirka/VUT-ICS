using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ICS_project.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ICS_projectDbContext>
{
    public ICS_projectDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ICS_projectDbContext> builder = new();
        builder.UseSqlite($"Data Source=SQLite.db;Cache=Shared");
        return new ICS_projectDbContext(builder.Options);
    }
}
