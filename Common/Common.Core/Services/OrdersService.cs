using Common.Core.Services.Contracts;
using Common.Core.ViewModels;
using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Common.Core.Services
{
    public interface IOrdersService
    {
        DataTableResultModel GetDataTable(DataTableModel model);
        string GetProcessingCount();
        string GetShippedCount();
        string GetDeliveredCount();

    }
    public class OrdersService : IOrdersService
    {
        private readonly IGenericRepository<Users> _repository;
        private readonly LogisticContext _dbcontext;
        private readonly IContextHelper _contextHelper;

        public OrdersService(IGenericRepository<Users> repository, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _repository = repository;
            _dbcontext = dbcontext;
            _contextHelper = contextHelper;
        }

        public string GetProcessingCount()
        {
            try
            {
                var data = _dbcontext.Orders.Where(x => x.OrderStatus == "Processing").Count();
                var result = data.ToString();

                if (result != null)
                {
                    return result;
                }
                return "Not Found";
            }
            catch
            {
                throw;
            }
        }
        public string GetShippedCount()
        {
            try
            {
                var data = _dbcontext.Orders.Where(x => x.OrderStatus == "Shipped").Count();

                var result = data.ToString();

                if (result != null)
                {
                    return result;
                }
                return "Not Found";
            }
            catch
            {
                throw;
            }
        }
        public string GetDeliveredCount()
        {
            try
            {
                var data = _dbcontext.Orders.Where(x => x.OrderStatus == "Delivered").Count();

                var result = data.ToString();

                if (result != null)
                {
                    return result;
                }
                return "Not Found";
            }
            catch
            {
                throw;
            }
        }

        public DataTableResultModel GetDataTable(DataTableModel model)
        {
            int recordsTotal = 0;
            var list = (from x in _dbcontext.Orders select x).AsQueryable();

            var listss = (from x in _dbcontext.Orders select x).AsQueryable().Count();
            var lists = (from x in _dbcontext.Users select x).AsQueryable();


            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                list = list.Where(x => (x.Id + "").Contains(model.Id));
            }
            if (!string.IsNullOrWhiteSpace(model.FullName))
            {
                list = list.Where(x => (x.FullName + "").Contains(model.FullName));
            }

            if (DateTime.TryParse(model.CreatedOn.ToString(), out DateTime createdOn))
            {
                list = list.Where(o => o.CreatedOn.HasValue && o.CreatedOn.Value.Date == createdOn.Date);
            }

            recordsTotal = list.Count();
            var data = list.Skip(model.skip).ToList();
            var result = new DataTableResultModel { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return result;
        }

    }
}
