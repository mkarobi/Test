using TFWebService.Repo.Infrastructure;
using TFWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFWebService.Repo.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {       
        Task<bool> UserExists(string username);
    }
}
