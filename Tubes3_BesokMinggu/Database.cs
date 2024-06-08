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
        private List<string> files;
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

            files = new List<string>();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biodata>().ToTable("biodata");
            modelBuilder.Entity<sidik_jari>().ToTable("sidik_jari").HasKey(s => new { s.nama, s.berkas_citra });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DBPath}");

        public void refreshSeed(string folderPath)
        {
            // get all file inside folderpath
            files = new List<string>(Directory.GetFiles(folderPath, "*.BMP"));
            if (files.Count == 0)
            {
                Debug.Print("No fingerprint image found in the specified folder path: " + folderPath);
            }
            seedSidikJari(folderPath);
            SaveToTextProcessedSidikJari(folderPath);
        }
        
        private void seedSidikJari(string folderPath)
        {
            // drop all column if exists
            sidik_jari.RemoveRange(sidik_jari);
            SaveChanges();
            
            try
            {
                var names = getAllName();
                Random random = new Random();
                for (int i =0; i < files.Count; i++)
                {
                    string namaRandom = names[random.Next(names.Count)];
                    for (int j = 1; j <= 10; j++)
                    {
                        if (i*10 + j > files.Count) break;
                        string namaAlay = StringMatching.toBahasaAlay(namaRandom);
                        sidik_jari.Add(new sidik_jari
                        {
                            nama = namaAlay,
                            berkas_citra = files[i*10 + j - 1]
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
        
        private void SaveToTextProcessedSidikJari(string path)
        {
            for (int i = 0; i < files.Count; i++)
            {
                string temp = Solver.BinaryToASCII(
                    Solver.ImageToByteArray(
                        Solver.ProcessImage(files[i])
                    )
                );
                
                // if file doesn't exist, create file
                if (!System.IO.File.Exists(files[i]))
                {
                    System.IO.File.Create(files[i]);
                }
                
                // Save to txt file
                System.IO.File.WriteAllText(files[i].Replace("BMP", "txt"), temp);
            }
        }
        
        private List<string> getAllName()
        {
            return ResultData.Select(x => x.nama).ToList();
        }
    }
}