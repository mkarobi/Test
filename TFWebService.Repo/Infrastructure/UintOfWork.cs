﻿using TFWebService.Repo.Repositories.Interface;
using TFWebService.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace TFWebService.Repo.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        #region ctor
        protected readonly DbContext _db;
        public UnitOfWork(TContext tContext)
        {
            _db = tContext;
        }
        #endregion

        #region privaterepository
        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_db);
                }
                return _userRepository;
            }
        }
        private ITrackDetailsRepository _trackDetailsRepository;
        public ITrackDetailsRepository TrackDetailsRepository
        {
            get
            {
                if (_trackDetailsRepository == null)
                {
                    _trackDetailsRepository = new TrackDetailsRepository(_db);
                }
                return _trackDetailsRepository;
            }
        }
        private IMainDetailsRepository _mainDetailsRepository;
        public IMainDetailsRepository MainDetailsRepository
        {
            get
            {
                if (_mainDetailsRepository == null)
                {
                    _mainDetailsRepository = new MainDetailsRepository(_db);
                }
                return _mainDetailsRepository;
            }
        }
        private IFoodsCaloriesRepository _foodsCaloriesRepository;
        public IFoodsCaloriesRepository FoodsCaloriesRepository
        {
            get
            {
                if (_foodsCaloriesRepository == null)
                {
                    _foodsCaloriesRepository = new FoodsCaloriesRepository(_db);
                }
                return _foodsCaloriesRepository;
            }
        }
        private IFitnessCaloriesRepository _fitnessCaloriesRepository;
        public IFitnessCaloriesRepository FitnessCaloriesRepository
        {
            get
            {
                if (_fitnessCaloriesRepository == null)
                {
                    _fitnessCaloriesRepository = new FitnessCaloriesRepository(_db);
                }
                return _fitnessCaloriesRepository;
            }
        }







        #endregion

        #region save
        public bool Save()
        {
            if (_db.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> SaveAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
