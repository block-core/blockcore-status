using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities.Admin;
using BlockcoreStatus.IocConfig;
using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin.Settings;
using Common.Web.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockcoreStatus.MsTests;

[TestClass]
public class CoreTests
{
    private readonly IServiceProvider _serviceProvider;

    public CoreTests()
    {
        var services = new ServiceCollection();
        services.AddOptions();
        services.AddLogging(cfg => cfg.AddConsole().AddDebug());
        services.AddScoped<IWebHostEnvironment, TestHostingEnvironment>();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        services.Configure<SiteSettings>(options => configuration.Bind(options))
            .PostConfigure<SiteSettings>(x => { x.ActiveDatabase = ActiveDatabase.InMemoryDatabase; });
        services.AddSingleton(provider => configuration);

        services.AddCustomIdentityServices(configuration);
        services.AddCommonWeb();
        services.AddCloudscribePagination();

        var siteSettings = configuration.GetSiteSettings();
        services.AddConfiguredDbContext(siteSettings);
        _serviceProvider = services.BuildServiceProvider();

        var identityDbInitialize = _serviceProvider.GetRequiredService<IIdentityDbInitializer>();
        identityDbInitialize.Initialize();
        identityDbInitialize.SeedData();
    }

    [TestMethod]
    public void TestUserAdminExists()
    {
        _serviceProvider.RunScopedService<IUnitOfWork>(context =>
        {
            var users = context.Set<User>();
            Assert.IsTrue(users.Any(x => x.UserName == "Admin"));
        });
    }
}