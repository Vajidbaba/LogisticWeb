﻿using Microsoft.AspNetCore.Mvc;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}
