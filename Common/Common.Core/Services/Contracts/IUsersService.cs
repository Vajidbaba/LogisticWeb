using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Models;

namespace Common.Core.Services.Contracts
{
    public interface IUsersService
    {
        Task<List<Users>> GetAllUsers();
        int CreateNewUser(Users users);
        Users GetUsersDetails(int? Id);
        bool UpdatePerson(int id, Users users);
    }
}
