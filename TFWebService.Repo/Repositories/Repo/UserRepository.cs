using TFWebService.Data.DatabaseContext;
using TFWebService.Repo.Infrastructure;
using TFWebService.Data.Models;
using TFWebService.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TFWebService.Repo.Repositories.Repo
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext dbContext) : base (dbContext)
        {
            _db = _db ?? (TFDbContext)_db;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await GetAsync(p => p.UserName == username) != null)
                return true;
            return false;
        }
    }
}
