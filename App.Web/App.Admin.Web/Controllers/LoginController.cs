using Microsoft.AspNetCore.Mvc;

namespace App.Admin.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
