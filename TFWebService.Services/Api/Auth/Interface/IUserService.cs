using TFWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFWebService.Services.Site.Admin.Auth.Interface
{
    public interface IUserService
    {
        Task<User> GetUserForPassChange(string id, string password);
        Task<bool> UpdateUserPass(User user, string newPassword);
    }
}
