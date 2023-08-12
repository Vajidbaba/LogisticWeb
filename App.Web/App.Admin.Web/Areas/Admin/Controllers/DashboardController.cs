using Microsoft.AspNetCore.Mvc;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DashboardController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
