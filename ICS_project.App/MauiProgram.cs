using ICS_project.App.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using ICS_project.BL;

namespace ICS_project.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            IServiceCollection services = new ServiceCollection();

            ConfigureAppSettings(builder);


            builder.Services
                .AddDALServices(builder.Configuration)
                .AddAppServices()
                .AddBLServices();

            foreach (var service in services)
            {
                builder.Services.Add(service);
            }

            var app = builder.Build();
            app.Services.GetRequiredService<IDbMigrator>().Migrate();
            RegisterRouting(app.Services.GetRequiredService<INavigationService>());
            return app;
        }

        private static void RegisterRouting(INavigationService navigationService)
        {
            foreach (var route in navigationService.Routes)
            {
                Routing.RegisterRoute(route.Route, route.ViewType);
            }
        }

        private static void ConfigureAppSettings(MauiAppBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            const string appSettingsFilePath = "ICS_project.App.appsettings.json";
            using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
            if (appSettingsStream is not null)
            {
                configurationBuilder.AddJsonStream(appSettingsStream);
            }
            else
            {
                string[] resources = assembly.GetManifestResourceNames();
                throw new Exception(assembly.GetManifestResourceNames()[0]);
            }

            var configuration = configurationBuilder.Build();
            builder.Configuration.AddConfiguration(configuration);
        }
    }
}