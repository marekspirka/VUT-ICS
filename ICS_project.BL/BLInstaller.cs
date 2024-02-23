using ICS_project.BL.Facades;
using ICS_project.BL.Mappers;
using ICS_project.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace ICS_project.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        //adding all facades to services
        services.AddSingleton<IProjectFacade, ProjectFacade>();
        services.AddSingleton<ITagFacade, TagFacade>();
        services.AddSingleton<IFilterFacade, FilterFacade>();
        services.AddSingleton<IUserFacade, UserFacade>();
        services.AddSingleton<IActivityFacade, ActivityFacade>();

        //adding all model mappers to service
        services.AddSingleton<IActivityDetailModelMapper, ActivityDetailModelMapper>();
        services.AddSingleton<IFilterModelMapper, FilterModelMapper>();
        services.AddSingleton<IUserDetailModelMapper, UserDetailModelMapper>();
        services.AddSingleton<IProjectListModelMapper, ProjectListModelMapper>();
        services.AddSingleton<IProjectDetailModelMapper, ProjectDetailModelMapper>();
        services.AddSingleton<ITagModelMapper, TagModelMapper>();

        return services;
    }
}
