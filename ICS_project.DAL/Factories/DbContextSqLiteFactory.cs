using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ICS_projectDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedDemoData;

    public DbContextSqLiteFactory(string databaseName, bool seedDemoData = false)
    {
        _databaseName = databaseName;
        _seedDemoData = seedDemoData;
    }

    public ICS_projectDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ICS_projectDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");
        return new ICS_projectDbContext(builder.Options, _seedDemoData);
    }
}