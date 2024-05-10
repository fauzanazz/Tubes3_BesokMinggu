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
        
        public DbSet<sidik_jari> sidik_jari { get; set; }

        public Database(string dbPath)
        {
            DBPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),dbPath);
            // Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResultData>().ToTable("biodata");
            modelBuilder.Entity<sidik_jari>().ToTable("sidik_jari").HasNoKey();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DBPath}");

        public void seedSidikJari()
        {
            var names = getAllName();
            // foreach (var name in names)
            // {
            //     sidik_jari.Add(new sidik_jari()
            //     {
            //         nama = name,
            //         sidik_jari = "sidik jari"
            //     });
            // }
        }
        
        public List<string> getAllName()
        {
            return ResultData.Select(x => x.nama).ToList();
        }
    }
}