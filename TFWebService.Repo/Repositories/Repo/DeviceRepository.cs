using Microsoft.EntityFrameworkCore;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Models;
using TFWebService.Repo.Infrastructure;
using TFWebService.Repo.Repositories.Interface;

namespace TFWebService.Repo.Repositories.Repo
{
    public class DeviceRepository : Repository<Device>,IDeviceRepository
    {
        private readonly DbContext _db;

        public DeviceRepository(DbContext dbContext) : base(dbContext)
        {
            _db = _db ?? (TFDbContext)_db;
        }
    }
}
