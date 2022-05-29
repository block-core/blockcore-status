using Microsoft.AspNetCore.Builder;

namespace BlockcoreStatus.Services.Admin;

public static class NoBrowserCacheMiddlewareExtensions
{
    public static IApplicationBuilder UseNoBrowserCache(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<NoBrowserCacheMiddleware>();
    }
}