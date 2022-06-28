using TFWebService.Common.Helpers;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Models;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Site.Admin.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFWebService.Services.Site.Admin.Auth.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork<TFDbContext> _db;
        public AuthService(IUnitOfWork<TFDbContext> dbContext)
        {
            _db = dbContext;
        }

        public async Task<User> Login(string username, string password)
        {
            var users = await _db.UserRepository.GetManyAsync(p => p.UserName == username, null, "");
            var user = users.SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            if (!Utilities.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;


            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            Utilities.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _db.UserRepository.InserAsync(user);
            await _db.SaveAsync();

            return user;
        }

    }
}
