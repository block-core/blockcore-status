using blockcore.status.DataLayer.Context;
using blockcore.status.ViewModels.Admin.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace blockcore.status.DataLayer.SQLite;

public class SQLiteContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
{
    public SQLiteDbContext CreateDbContext(string[] args)
    {
        var services = new ServiceCollection();
        services.AddOptions();
        services.AddLogging(cfg => cfg.AddConsole().AddDebug());
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        services.AddSingleton<AuditableEntitiesInterceptor>();

        var basePath = Directory.GetCurrentDirectory();
        WriteLine($"Using `{basePath}` as the ContentRootPath");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        services.AddSingleton(_ => configuration);
        services.Configure<SiteSettings>(options => configuration.Bind(options));

        var buildServiceProvider = services.BuildServiceProvider();
        var siteSettings = buildServiceProvider.GetRequiredService<IOptionsSnapshot<SiteSettings>>();
        siteSettings.Value.ActiveDatabase = ActiveDatabase.SQLite;

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseConfiguredSQLite(siteSettings.Value, buildServiceProvider);

        return new SQLiteDbContext(optionsBuilder.Options);
    }
}