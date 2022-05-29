using System.Security.Claims;
using System.Security.Principal;
using blockcore.status.DataLayer.Context;
using blockcore.status.Entities.Admin;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.Services.Admin;
using blockcore.status.Services.Admin.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using blockcore.status.Services.Contracts;
using blockcore.status.Services;

namespace blockcore.status.IocConfig;

public static class AddCustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IPrincipal>(provider =>
            provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);

        services.AddScoped<ILookupNormalizer, CustomNormalizer>();

        services.AddScoped<ISecurityStampValidator, CustomSecurityStampValidator>();
        services.AddScoped<SecurityStampValidator<User>, CustomSecurityStampValidator>();

        services.AddScoped<IPasswordValidator<User>, CustomPasswordValidator>();
        services.AddScoped<PasswordValidator<User>, CustomPasswordValidator>();

        services.AddScoped<IUserValidator<User>, CustomUserValidator>();
        services.AddScoped<UserValidator<User>, CustomUserValidator>();

        services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationClaimsPrincipalFactory>();
        services.AddScoped<UserClaimsPrincipalFactory<User, Role>, ApplicationClaimsPrincipalFactory>();

        services.AddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

        services.AddScoped<IApplicationUserStore, ApplicationUserStore>();
        services.AddScoped<UserStore<User, Role, ApplicationDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>, ApplicationUserStore>();

        services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
        services.AddScoped<UserManager<User>, ApplicationUserManager>();

        services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
        services.AddScoped<RoleManager<Role>, ApplicationRoleManager>();

        services.AddScoped<IApplicationSignInManager, ApplicationSignInManager>();
        services.AddScoped<SignInManager<User>, ApplicationSignInManager>();

        services.AddScoped<IApplicationRoleStore, ApplicationRoleStore>();
        services.AddScoped<RoleStore<Role, ApplicationDbContext, int, UserRole, RoleClaim>, ApplicationRoleStore>();

        services.AddScoped<IEmailSender, AuthMessageSender>();
        services.AddScoped<ISmsSender, AuthMessageSender>();

        services.AddScoped<IIdentityDbInitializer, IdentityDbInitializer>();
        services.AddScoped<IUsedPasswordsService, UsedPasswordsService>();
        services.AddScoped<ISiteStatService, SiteStatService>();
        services.AddScoped<IUsersPhotoService, UsersPhotoService>();
        services.AddScoped<ISecurityTrimmingService, SecurityTrimmingService>();
        services.AddScoped<IAppLogItemsService, AppLogItemsService>();
        
        services.AddScoped<IGithubService, EfGithubService>();       
        services.AddScoped<IBlockcoreChainsService, EfBlockcoreChainsService>();       
        services.AddScoped<IBlockcoreIndexersService, EfBlockcoreIndexersService>();


        return services;
    }
}