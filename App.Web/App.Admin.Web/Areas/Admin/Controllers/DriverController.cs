using Common.Core.Services;
using Common.Core.Services.Contracts;
using Common.Core.ViewModels;
using Common.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DriverController : Controller
    {
        private readonly IOrdersService _orderService;

        public DriverController(IOrdersService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult List()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetOrders()
        {
            try
            {
                var model = new DataTableModel();
                model.draw = Request.Form["draw"].FirstOrDefault();
                model.start = Request.Form["start"].FirstOrDefault();
                model.lenght = Request.Form["lenght"].FirstOrDefault();
                model.sortColumn = Request.Form["sortColumn"].FirstOrDefault();
                model.sortColumnDirection = Request.Form["sortColumnDirection "].FirstOrDefault();
                model.searchValue = Request.Form["searchValue"].FirstOrDefault();
                model.Id = Request.Form["Id"].FirstOrDefault();
                model.FullName = Request.Form["FullName"].FirstOrDefault();

                model.TotalAmount = Request.Form["TotalAmount"].FirstOrDefault();
                model.OrderStatus = Request.Form["OrderStatus"].FirstOrDefault();

                model.OrderID = Request.Form["OrderID"].FirstOrDefault();
                var createdOnString = Request.Form["CreatedOn"].FirstOrDefault();
                if (DateTime.TryParse(createdOnString, out DateTime createdOn))
                {
                    model.CreatedOn = createdOn;
                }
                model.pageSize = model.lenght != null ? Convert.ToInt32(model.lenght) : 0;
                model.skip = model.start != null ? Convert.ToInt32(model.start) : 0;
                var result = _orderService.GetDataTable(model);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}