using System.Text;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.Controllers;

[BreadCrumb(Title = "Error", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "fas fa-warning")]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [BreadCrumb(Title = "Index", Order = 2, GlyphIcon = "fas fa-navicon")]
    public IActionResult Index(int? id)
    {
        var logBuilder = new StringBuilder();

        var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        logBuilder.Append("Error ").Append(id).Append(" for ").Append(Request.Method).Append(' ')
            .Append(statusCodeReExecuteFeature?.OriginalPath ?? Request.Path.Value).Append(Request.QueryString.Value)
            .AppendLine("\n");

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature?.Error != null)
        {
            var exception = exceptionHandlerFeature.Error;
            logBuilder.Append("<h1>Exception: ").Append(exception.Message).Append("</h1>")
                .AppendLine(exception.StackTrace);
        }

        foreach (var header in Request.Headers)
        {
            var headerValues = header.Value.ToString();
            logBuilder.Append(header.Key).Append(": ").AppendLine(headerValues);
        }

        _logger.LogErrorMessage(logBuilder.ToString());

        if (id == null)
        {
            return View("Error");
        }

        switch (id.Value)
        {
            case 401:
            case 403:
                return View("AccessDenied");
            case 404:
                return View("NotFound");

            default:
                return View("Error");
        }
    }
}