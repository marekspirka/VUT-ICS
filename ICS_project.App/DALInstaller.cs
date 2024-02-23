using ICS_project.App.Options;
using ICS_project.DAL;
using ICS_project.DAL.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ICS_project.DAL.Factories;

namespace ICS_project.App;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        DALOptions dalOptions = new();
        configuration.GetSection("ICS_project:DAL").Bind(dalOptions);

        services.AddSingleton<DALOptions>(dalOptions);

        string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName!);
        services.AddSingleton<IDbContextFactory<ICS_projectDbContext>>(provider => new DbContextSqLiteFactory(databaseFilePath, dalOptions?.Sqlite?.SeedDemoData ?? false));
        services.AddSingleton<IDbMigrator, SqliteDbMigrator>();

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<ProjectUserEntityMapper>();
        services.AddSingleton<TagActivityEntityMapper>();
        services.AddSingleton<TagEntityMapper>();
        services.AddSingleton<UserEntityMapper>();

        return services;
    }
}
