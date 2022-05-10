﻿using BreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.Areas.Identity.Controllers;

[Area(AreaConstants.AdminArea), Authorize, BreadCrumb(Title = "Admin", UseDefaultRouteUrl = true, Order = 0)]
public class HomeController : Controller
{
    [BreadCrumb(Title = "Index", Order = 1)]
    public IActionResult Index()
    {
        return View();
    }
}