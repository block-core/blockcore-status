using blockcore.status.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace blockcore.status.IocConfig;

public static class AddDynamicPermissionsExtensions
{
    public static IServiceCollection AddDynamicPermissions(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
        services.AddAuthorization(opts =>
        {
            opts.AddPolicy(
                name: ConstantPolicies.DynamicPermission,
                configurePolicy: policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new DynamicPermissionRequirement());
                });
        });

        return services;
    }
}