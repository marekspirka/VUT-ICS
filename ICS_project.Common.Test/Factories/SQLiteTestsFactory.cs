using ICS_project.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Factories;

public class DbContextSQLiteTestingFactory : IDbContextFactory<ICS_projectDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public ICS_projectDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ICS_projectDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");
        return new TestingDbContext(builder.Options);
    }
}