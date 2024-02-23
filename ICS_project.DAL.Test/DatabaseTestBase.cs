using System;
using System.Threading.Tasks;
using ICS_project.Common.Test;
using ICS_project.Common.Test.Factories;
using ICS_project.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ICS_project.DAL.Test;

public class DatabaseTestsBase : IAsyncLifetime
{
    protected DatabaseTestsBase(ITestOutputHelper output)
    {
        TestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ICS_projectDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<ICS_projectDbContext> DbContextFactory { get; }
    protected ICS_projectDbContext ICS_projectDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await ICS_projectDbContextSUT.Database.EnsureDeletedAsync();
        await ICS_projectDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ICS_projectDbContextSUT.Database.EnsureDeletedAsync();
        await ICS_projectDbContextSUT.DisposeAsync();
    }
}
