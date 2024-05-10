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

        public Database()
        {
            // DBPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),dbPath);
            DBPath = "databases.db";
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResultData>().ToTable("biodata");
            modelBuilder.Entity<sidik_jari>().ToTable("sidik_jari").HasNoKey();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DBPath}");

        public void seedSidikJari(string folderPath)
        {
            var names = getAllName();
            Random random = new Random();
            for (int i =0; i < 600; i++)
            {
                string namaAlay = StringMatching.toBahasaAlay(names[random.Next(0, names.Count)]);
                for (int j = 1; j <= 10; j++)
                {
                    sidik_jari.Add(new sidik_jari
                    {
                        nama = namaAlay,
                        berkas_citra = Solver.BinaryToASCII(
                                            Solver.ImageToByteArray(
                                                Solver.ProcessImage(folderPath + "fingerprint (" + (i*10 + j).ToString() + ").BMP")
                                            )
                                        )
                    });
                }
            }
        }
        
        public List<string> getAllName()
        {
            return ResultData.Select(x => x.nama).ToList();
        }
    }
}