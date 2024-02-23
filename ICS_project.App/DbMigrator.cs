﻿using ICS_project.App.Options;
using ICS_project.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.App;

interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class NoneDbMigrator : IDbMigrator
{
    public void Migrate() { }
    public Task MigrateAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

public class SqliteDbMigrator : IDbMigrator 
{
    private readonly IDbContextFactory<ICS_projectDbContext> _dbContextFactory;
    private readonly SqliteOptions _sqliteOptions;

    public SqliteDbMigrator(IDbContextFactory<ICS_projectDbContext> dbContextFactory, DALOptions dalOptions)
    {
        _dbContextFactory = dbContextFactory;
        _sqliteOptions = dalOptions.Sqlite ?? throw new ArgumentNullException(nameof(dalOptions),$@"{nameof(DALOptions.Sqlite)} are not set");
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using ICS_projectDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        if(_sqliteOptions.RecreateDatabaseEachTime)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
