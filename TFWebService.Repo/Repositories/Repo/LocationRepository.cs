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
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly DbContext _db;

        public LocationRepository(DbContext dbContext) : base(dbContext)
        {
            _db = _db ?? (TFDbContext)_db;
        }
    }
}
