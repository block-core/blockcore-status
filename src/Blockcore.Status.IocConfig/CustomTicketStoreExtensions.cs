using BlockcoreStatus.DataLayer.MSSQL;
using BlockcoreStatus.Services.Admin;
using BlockcoreStatus.ViewModels.Admin.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace BlockcoreStatus.IocConfig;

public static class CustomTicketStoreExtensions
{
    public static IServiceCollection AddCustomTicketStore(
        this IServiceCollection services, SiteSettings siteSettings)
    {
        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        // To manage large identity cookies
        var cookieOptions = siteSettings.CookieOptions;
        if (!cookieOptions.UseDistributedCacheTicketStore)
        {
            return services;
        }

        switch (siteSettings.ActiveDatabase)
        {
            case ActiveDatabase.SQLite: //TODO:
            case ActiveDatabase.InMemoryDatabase:
                services.AddMemoryCache();
                services.AddScoped<ITicketStore, MemoryCacheTicketStore>();
                break;

            case ActiveDatabase.LocalDb:
            case ActiveDatabase.SqlServer:
                services.AddDistributedSqlServerCache(options =>
                {
                    var cacheOptions = cookieOptions.DistributedSqlServerCacheOptions;
                    options.ConnectionString = string.IsNullOrWhiteSpace(cacheOptions.ConnectionString)
                        ? siteSettings.GetMsSqlDbConnectionString()
                        : cacheOptions.ConnectionString;
                    options.SchemaName = cacheOptions.SchemaName;
                    options.TableName = cacheOptions.TableName;
                });
                services.AddScoped<ITicketStore, DistributedCacheTicketStore>();
                break;

            default:
                throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
        }

        return services;
    }
}