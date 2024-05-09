using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Tubes3_BesokMinggu
{
    public sealed class Database : DbContext
    {
        public DbSet<ResultData> ResultData { get; set; }
        public string DBPath { get; private set; }

        public Database(string dbPath)
        {
            DBPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),dbPath);
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        public List<string> GetResultData()
        {
            return ResultData.Select(x => x.NIK).ToList();
        }

        public string GetDBPath()
        {
            return DBPath;
        }
    }
}