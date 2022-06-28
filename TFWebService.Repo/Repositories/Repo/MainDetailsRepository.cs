using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Models;
using TFWebService.Repo.Infrastructure;
using TFWebService.Repo.Repositories.Interface;

namespace TFWebService.Repo.Repositories.Repo
{
    public class MainDetailsRepository : Repository<MainDetails> ,IMainDetailsRepository
    {
        private readonly DbContext _db;

        public MainDetailsRepository(DbContext dbContext) : base(dbContext)
        {
            _db = _db ?? (TFDbContext)_db;

        }
    }
}
