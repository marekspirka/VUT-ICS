using ICS_project.DAL.Mappers;
using ICS_project.DAL;
using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using ICS_project.Common.Test;
using ICS_project.Common.Test.Factories;
using ICS_project.BL.Mappers;
using ICS_project.DAL.UnitOfWork;
namespace ICS_project.BL.Tests.FacadeTests;
public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        TestOutputConverter converter = new(output);
        Console.SetOut(converter);
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ActivityEntityMapper = new ActivityEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        ProjectUserEntityMapper = new ProjectUserEntityMapper();
        TagActivityEntityMapper = new TagActivityEntityMapper();
        TagEntityMapper = new TagEntityMapper();
        UserEntityMapper = new UserEntityMapper();

        ActivityDetailModelMapper = new ActivityDetailModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new UserDetailModelMapper(), new TagModelMapper());
        FilterModelMapper = new FilterModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new TagModelMapper(), new UserDetailModelMapper());
        ProjectDetailModelMapper = new ProjectDetailModelMapper(new UserDetailModelMapper());
        ProjectListModelMapper = new ProjectListModelMapper();
        TagModelMapper = new TagModelMapper();
        UserDetailModelMapper = new UserDetailModelMapper();
        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<ICS_projectDbContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected ProjectUserEntityMapper ProjectUserEntityMapper { get; }
    protected TagActivityEntityMapper TagActivityEntityMapper { get; }
    protected TagEntityMapper TagEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }

    protected IActivityDetailModelMapper ActivityDetailModelMapper { get; }
    protected IFilterModelMapper FilterModelMapper { get; }
    protected IProjectDetailModelMapper ProjectDetailModelMapper { get; }
    protected IProjectListModelMapper ProjectListModelMapper { get; }
    protected ITagModelMapper TagModelMapper { get; }
    protected IUserDetailModelMapper UserDetailModelMapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }


    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }
    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}