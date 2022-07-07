using BlockcoreStatus.HostedServices;
using BlockcoreStatus.IocConfig;
using BlockcoreStatus.Services.Admin.Logger;
using BlockcoreStatus.ViewModels.Admin.Settings;
using Common.Web.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging(builder.Logging, builder.Environment, builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);
var webApp = builder.Build();
ConfigureMiddlewares(webApp, webApp.Environment);
ConfigureEndpoints(webApp);
ConfigureDatabase(webApp);
webApp.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<SiteSettings>(options => configuration.Bind(options));
    services.Configure<ContentSecurityPolicyConfig>(options =>
        configuration.GetSection("ContentSecurityPolicyConfig").Bind(options));

    // Adds all of the ASP.NET Core Identity related services and configurations at once.
    services.AddCustomIdentityServices(configuration);

    services.AddMvc(options => options.UseYeKeModelBinder());

    services.AddCommonWeb();
    services.AddCloudscribePagination();

    services.AddControllersWithViews(options => { options.Filters.Add(typeof(ApplyCorrectYeKeFilterAttribute)); });
    services.AddRazorPages();
    services.AddHostedService<UpdateGithubDataHostedService>();

    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("status", new OpenApiInfo
        {
            Version = "v1",
            Title = "Blockcore Status",
            Description = "Service Status API that monitors the different services and networks related to Blockcore.",
            Contact = new OpenApiContact
            {
                Name = "Blockcore Status",
            },
        });
    });

}

void ConfigureLogging(ILoggingBuilder logging, IHostEnvironment env, IConfiguration configuration)
{
    logging.ClearProviders();

    logging.AddDebug();

    if (env.IsDevelopment())
    {
        logging.AddConsole();
    }

    logging.AddDbLogger(); // You can change its Log Level using the `appsettings.json` file -> Logging -> LogLevel -> Default
    logging.AddConfiguration(configuration.GetSection("Logging"));
}

void ConfigureMiddlewares(IApplicationBuilder app, IHostEnvironment env)
{
    if (!env.IsDevelopment())
    {
        app.UseHsts();
    }
    app.UseHttpsRedirection();

    app.UseExceptionHandler("/error/index/500");
    app.UseStatusCodePagesWithReExecute("/error/index/{0}");

    app.UseContentSecurityPolicy();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger(c =>
    {
        c.RouteTemplate = "docs/{documentName}/openapi.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "docs";
        c.SwaggerEndpoint("/docs/status/openapi.json", "Blockcore Status API");
    });
}

void ConfigureEndpoints(IApplicationBuilder app)
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        endpoints.MapControllerRoute(
            "areaRoute",
            "{area:exists}/{controller=Account}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}");

        endpoints.MapRazorPages();
    });
}

void ConfigureDatabase(IApplicationBuilder app)
{
    app.ApplicationServices.InitializeDb();
}