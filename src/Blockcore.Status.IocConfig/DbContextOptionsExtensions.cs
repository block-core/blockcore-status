using blockcore.status.DataLayer.Context;
using blockcore.status.DataLayer.InMemoryDatabase;
using blockcore.status.DataLayer.MSSQL;
using blockcore.status.DataLayer.SQLite;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.ViewModels.Admin.Settings;
using Common.Web.Core;
using Microsoft.Extensions.DependencyInjection;

namespace blockcore.status.IocConfig;

public static class DbContextOptionsExtensions
{
    public static IServiceCollection AddConfiguredDbContext(
        this IServiceCollection serviceCollection, SiteSettings siteSettings)
    {
        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        serviceCollection.AddInterceptors();

        switch (siteSettings.ActiveDatabase)
        {
            case ActiveDatabase.InMemoryDatabase:
                serviceCollection.AddConfiguredInMemoryDbContext(siteSettings);
                break;

            case ActiveDatabase.LocalDb:
            case ActiveDatabase.SqlServer:
                serviceCollection.AddConfiguredMsSqlDbContext(siteSettings);
                break;

            case ActiveDatabase.SQLite:
                serviceCollection.AddConfiguredSQLiteDbContext(siteSettings);
                break;

            default:
                throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
        }

        return serviceCollection;
    }

    /// <summary>
    ///     Creates and seeds the database.
    /// </summary>
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        serviceProvider.RunScopedService<IIdentityDbInitializer>(identityDbInitialize =>
        {
            identityDbInitialize.Initialize();
            identityDbInitialize.SeedData();
        });
    }

    private static void AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<AuditableEntitiesInterceptor>();
    }
}