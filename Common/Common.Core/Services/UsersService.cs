using Common.Core.Services.Contracts;
using Common.Core.ViewModels;
using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;

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
        public int CreateUser(UsersModel entity)
        {
            try
            {
                Users logistic = new Users();
                logistic.Active = true;
                logistic.Username = entity.Username;
                logistic.Mobile = entity.Mobile;
                logistic.Password = entity.Password;
                logistic.Role = entity.Role;
                _dbcontext.Users.Add(logistic);
                _dbcontext.SaveChanges();
                return logistic.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateUser(int id, UsersModel model)
        {
            try
            {
                var entity = _dbcontext.Users.Where(x => x.Id == id).SingleOrDefault();
                if (entity == null)
                {
                    return false;
                }
                entity.Active = model.Active;
                entity.Username = model.Username;
                entity.Mobile = model.Mobile;
                entity.Password = model.Password;
                entity.Role = model.Role;
                _dbcontext.Update(entity);
                _dbcontext.SaveChanges();
                return true;
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
                    result.Username = user.Username;
                    result.Mobile = user.Mobile;
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
