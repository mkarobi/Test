﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using TFWebService.Data.Models;

namespace TFWebService.Data.DatabaseContext
{
    public class TFDbContext : DbContext
    {
        //public TFDbContext(){}
        //public TFDbContext(DbContextOptions<TFDbContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source ={Path.Combine(Environment.CurrentDirectory, "TFDb.db")}").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<TrackDetails> TrackDetails { get; set; }
        public DbSet<MainDetails> MainDetails { get; set; }
        public DbSet<FoodsCalories> FoodCalories { get; set; }
        public DbSet<FitnessCalories> FitnessCalories { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
