using TFWebService.Repo.Repositories.Interface;
using TFWebService.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFWebService.Repo.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext:DbContext
    {
        IUserRepository UserRepository { get; }
        ITrackDetailsRepository TrackDetailsRepository { get; }
        IMainDetailsRepository MainDetailsRepository { get; }
        IFoodsCaloriesRepository FoodsCaloriesRepository { get; }
        IFitnessCaloriesRepository FitnessCaloriesRepository { get; }
        bool Save();
        Task<bool> SaveAsync();
    }
}
