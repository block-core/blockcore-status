using Microsoft.AspNetCore.Builder;

namespace blockcore.status.Services.Admin;

public static class NoBrowserCacheMiddlewareExtensions
{
    public static IApplicationBuilder UseNoBrowserCache(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<NoBrowserCacheMiddleware>();
    }
}