using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class BaseAdminAreaController : BaseController
    {
       
    }
}
