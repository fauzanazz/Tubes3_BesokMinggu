using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Tubes3_BesokMinggu
{
    public sealed class Database : DbContext
    {
        private List<string> processedBinary;
        public DbSet<Biodata> ResultData { get; set; }
        public string DBPath { get; private set; }
        
        public DbSet<sidik_jari> sidik_jari { get; set; }

        public Database()
        {
            DBPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),"databases.db");
    
            if (!System.IO.File.Exists(DBPath))
            {
                throw new Exception("Database file does not exist at the specified path: " + DBPath);
            }

            Database.EnsureCreated();

            processedBinary = new List<string>();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biodata>().ToTable("biodata");
            modelBuilder.Entity<sidik_jari>().ToTable("sidik_jari").HasKey(s => new { s.nama, s.berkas_citra });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DBPath}");

        public void seedSidikJari(string folderPath)
        {
            // drop all column if exists
            sidik_jari.RemoveRange(sidik_jari);
            SaveChanges();
            try
            {
                var names = getAllName();
                Random random = new Random();
                for (int i =0; i < 600; i++)
                {
                    string namaRandom = names[random.Next(names.Count)];
                    for (int j = 1; j <= 10; j++)
                    {
                        string namaAlay = StringMatching.toBahasaAlay(namaRandom);
                        sidik_jari.Add(new sidik_jari
                        {
                            nama = namaAlay,
                            berkas_citra = folderPath + "fingerprint (" + (i*10 + j) + ").BMP"
                        });
                    }
                    SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("Seeding sidik_jari table is done.");
        }
        
        public void SaveToTextProcessedSidikJari(string path)
        {
            // if file exists, image already processed
            if (System.IO.File.Exists(path + "fingerprint (1).txt")) return;

            for (int i = 0; i < 6000; i++)
            {
                string temp = Solver.BinaryToASCII(
                    Solver.ImageToByteArray(
                        Solver.ProcessImage(Path.Combine(path,"fingerprint (" + (i+1) + ").BMP"))
                    )
                );
                
                // if file doesn't exist, create file
                if (!System.IO.File.Exists(Path.Combine(path,"fingerprint (" + (i+1) + ").BMP")))
                {
                    System.IO.File.Create(Path.Combine(path,"fingerprint (" + (i+1) + ").BMP"));
                }
                
                // Save to txt file
                System.IO.File.WriteAllText(Path.Combine(path,"fingerprint (" + (i+1) + ").txt"), temp);
            }
        }
        
        private List<string> getAllName()
        {
            return ResultData.Select(x => x.nama).ToList();
        }
    }
}