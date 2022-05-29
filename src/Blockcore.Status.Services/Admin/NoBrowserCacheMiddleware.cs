using Common.Web.Core;
using Microsoft.AspNetCore.Http;

namespace BlockcoreStatus.Services.Admin;

public class NoBrowserCacheMiddleware
{
    private readonly RequestDelegate _next;

    public NoBrowserCacheMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        context.DisableBrowserCache();
        return _next(context);
    }
}