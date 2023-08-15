using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Models;
using Common.Core.ViewModels;


namespace Common.Core.Services.Contracts
{
    public interface IUsersService
    {
        Task<List<Users>> GetAllUsers();
        int CreateUser(UsersModel users);
        bool UpdateUser(int id, UsersModel users);
        Users GetUsersDetails(int? Id);
    }
}
