using Common.Core.Services.Contracts;
using Common.Core.ViewModels;
using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployees();
        int GetUserCount();
        int CreateEmployee(EmployeeModel users);
        bool UpdateEmployee(int id, EmployeeModel users);
        EmployeeModel GetEmployeeDetails(int? Id);

    }


    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<EmployeeModel> _repository;
        private readonly LogisticContext _dbcontext;
        private readonly IContextHelper _contextHelper;

        public EmployeeService(IGenericRepository<EmployeeModel> repository, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _repository = repository;
            _dbcontext = dbcontext;
            _contextHelper = contextHelper;
        }
        public async Task<List<EmployeeModel>> GetEmployees()
        {
            try
            {
                return await _dbcontext.EmployeeList.Where(x => x.isActive).ToListAsync();
            }
            catch
            {
                throw;
            }
        }


        public int GetUserCount()
        {
            try
            {
                var lastUser = _dbcontext.EmployeeList.OrderByDescending(u => u.Id).Where(x => x.isActive == true).FirstOrDefault();

                if (lastUser != null)
                {
                    return lastUser.Id;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching last user ID", ex);
            }
        }


        public int CreateEmployee(EmployeeModel entity)
        {
            try
            {
                var userId = _contextHelper.GetUsername();
                EmployeeModel logistic = new EmployeeModel();
                logistic.isActive = true;
                logistic.AddedBy = userId;
                logistic.AddedOn = DateTime.Now;
                logistic.UpdatedBy = userId;
                logistic.UpdatedOn = DateTime.Now;
                logistic.Name = entity.Name;
                logistic.UserId = entity.UserId;
                logistic.Gender = entity.Gender;
                logistic.Mobile = entity.Mobile;
                logistic.Email = entity.Email;
                logistic.State = entity.State;
                logistic.City = entity.City;
                logistic.PinCode = entity.PinCode;
                logistic.Address = entity.Address;
                _dbcontext.EmployeeList.Add(logistic);
                _dbcontext.SaveChanges();
                return logistic.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateEmployee(int id, EmployeeModel model)
        {
            try
            {
                var userId = _contextHelper.GetUsername();
                var entity = _dbcontext.EmployeeList.Where(x => x.Id == id).SingleOrDefault();
                if (entity == null)
                {
                    return false;
                }
                entity.UpdatedBy = userId;
                entity.UpdatedOn = DateTime.Now;
                entity.isActive = model.isActive;
                entity.Name = model.Name;
                entity.UserId = model.UserId;
                entity.Gender = model.Gender;
                entity.Mobile = model.Mobile;
                entity.Email = model.Email;
                entity.State = model.State;
                entity.City = model.City;
                entity.PinCode = model.PinCode;
                entity.Address = model.Address;
                _dbcontext.Update(entity);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public EmployeeModel GetEmployeeDetails(int? Id)
        {
            try
            {
                var data = _dbcontext.EmployeeList.Where(x => x.Id == Id && x.isActive == true).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
