using ICS_project.Common.Test.Seeds;
using ICS_project.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test;

public class TestingDbContext : ICS_projectDbContext
{
    private readonly bool _seedTestingData;

    public TestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData: false)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            ActivityTestSeeds.Seed(modelBuilder);
            ProjectTestSeeds.Seed(modelBuilder);
            ProjectUserTestSeeds.Seed(modelBuilder);
            TagActivitySeeds.Seed(modelBuilder);
            TagTestSeeds.Seed(modelBuilder);
            UserTestSeeds.Seed(modelBuilder);
        }
    }
}