using Common.Core.Services.Contracts;
using Common.Core.ViewModels;
using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Common.Core.Services
{
    public class UsresService : IUsersService
    {
        private readonly IGenericRepository<Users> _repository;
        private readonly LogisticContext _dbcontext;


        public UsresService(IGenericRepository<Users> repository, LogisticContext dbcontext)
        {
            _repository = repository;
            _dbcontext = dbcontext;

        }
        public async Task<List<Users>> GetAllUsers()
        {
            try
            {
                return await _repository.GetAllUsers();
            }
            catch
            {
                throw;
            }
        }
        public Users CreateNewUser(Users entity)
        {
            try
            {
                _dbcontext.Users.Add(entity);
                _dbcontext.SaveChanges();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Users UpdatePerson(Users entity)
        {
            try
            {
                _dbcontext.Update(entity);
                _dbcontext.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Users GetUsersDetails(int? Id)
        {
            try
            {
                var result = new Users();
                var data = _dbcontext.Users.Where(x => x.Id == Id);
                foreach (var user in data)
                {
                    result.Id = user.Id;
                    result.Active = user.Active;
                    result.FullName = user.FullName;
                    result.Email = user.Email;
                    result.Password = user.Password;
                    result.Role = user.Role;
                    _dbcontext.Add(result);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
