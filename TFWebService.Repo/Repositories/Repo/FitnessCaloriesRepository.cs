using Microsoft.EntityFrameworkCore;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Models;
using TFWebService.Repo.Infrastructure;
using TFWebService.Repo.Repositories.Interface;

namespace TFWebService.Repo.Repositories.Repo
{
    public class FitnessCaloriesRepository : Repository<FitnessCalories>, IFitnessCaloriesRepository
    {
        private readonly DbContext _db;

        public FitnessCaloriesRepository(DbContext dbContext) : base(dbContext)
        {
            _db = _db ?? (TFDbContext)_db;
        }
    }
}
