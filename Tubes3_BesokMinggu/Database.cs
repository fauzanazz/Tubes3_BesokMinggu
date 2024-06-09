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

        private List<string> list_nama = new List<string>
        {
            "Dirja Laksmiwati",
            "Kunthara Saputra",
            "Kanda Manullang",
            "Winda Sihombing",
            "Nilam Hardiansyah",
            "Gaiman Winarsih",
            "Jais Susanti",
            "Uda Maulana",
            "Olga Situmorang",
            "Maryanto Purwanti",
            "Wadi Safitri",
            "Diana Mahendra",
            "Makuta Saptono",
            "Vicky Santoso",
            "Kenari Agustina",
            "Titi Samosir",
            "Bahuraksa Pudjiastuti",
            "Edison Permata",
            "Kamal Situmorang",
            "Pranawa Lestari",
            "Nrima Winarsih",
            "Harsanto Suwarno",
            "Edi Kusmawati",
            "Ivan Dongoran",
            "Dinda Palastri",
            "Luhung Riyanti",
            "Gading Napitupulu",
            "Yuni Maryati",
            "Dipa Widodo",
            "Darijan Wijaya",
            "Hendra Purnawati",
            "Rahmi Latupono",
            "Suci Tampubolon",
            "Galih Novitasari",
            "Caket Pradana",
            "Gaman Sitorus",
            "Lega Winarsih",
            "Nilam Sirait",
            "Galang Winarno",
            "Jamal Budiyanto",
            "Tugiman Astuti",
            "Yulia Palastri",
            "Nalar Januar",
            "Farah Sitorus",
            "Bambang Sitorus",
            "Bagya Siregar",
            "Makara Puspita",
            "Dasa Hartati",
            "Zahra Widodo",
            "Tomi Purnawati",
            "Kartika Mandasari",
            "Bakiman Sihombing",
            "Irnanto Yolanda",
            "Radit Prasetyo",
            "Indah Anggraini",
            "Cinta Rajata",
            "Bajragin Sihotang",
            "Gabriella Prasetyo",
            "Yessi Thamrin",
            "Hardi Natsir",
            "Radit Natsir",
            "Balijan Hidayanto",
            "Elisa Utama",
            "Cawisadi Melani",
            "Saiful Mayasari",
            "Raden Hartati",
            "Dina Manullang",
            "Banawi Maryati",
            "Ega Astuti",
            "Teddy Sudiati",
            "Prabu Safitri",
            "Lantar Firmansyah",
            "Hendri Situmorang",
            "Dina Simanjuntak",
            "Jefri Mahendra",
            "Yulia Padmasari",
            "Drajat Latupono",
            "Karna Nababan",
            "Hendri Hardiansyah",
            "Wani Sihotang",
            "Sarah Damanik",
            "Waluyo Pratama",
            "Bahuraksa Sudiati",
            "Wardaya Laksita",
            "Darmanto Palastri",
            "Ghaliyati Pratama",
            "Lanang Tamba",
            "Sabri Pangestu",
            "Malika Pradana",
            "Rahmi Hasanah",
            "Upik Adriansyah",
            "Amelia Gunarto",
            "Paris Yolanda",
            "Rafid Halimah",
            "Banawa Hassanah",
            "Syahrini Kuswandari",
            "Yono Natsir",
            "Hafshah Pratama",
            "Sabar Hardiansyah",
            "Ulva Siregar",
            "Luthfi Halim",
            "Talia Saefullah",
            "Malika Oktaviani",
            "Gandi Kuswoyo",
            "Restu Hutasoit",
            "Heru Natsir",
            "Agus Latupono",
            "Suci Prasasta",
            "Vino Napitupulu",
            "Viman Kurniawan",
            "Cahyadi Samosir",
            "Praba Anggriawan",
            "Pangestu Suryatmi",
            "Mustika Hidayat",
            "Samiah Andriani",
            "Dian Rahayu",
            "Prayitna Megantara",
            "Yosef Prakasa",
            "Bahuwarna Nurdiyanti",
            "Talia Wibowo",
            "Azalea Prasetya",
            "Bancar Natsir",
            "Unjani Prakasa",
            "Usyi Santoso",
            "Raden Megantara",
            "Ami Fujiati",
            "Jagapati Rajata",
            "Darmanto Kusumo",
            "Uchita Wibowo",
            "Ilsa Yuliarti",
            "Yuliana Wijayanti",
            "Nyana Januar",
            "Balapati Hutapea",
            "Daru Nainggolan",
            "Jaya Nasyiah",
            "Iriana Iswahyudi",
            "Bala Suryono",
            "Julia Hutagalung",
            "Mahdi Wijayanti",
            "Ellis Sinaga",
            "Drajat Fujiati",
            "Pangestu Novitasari",
            "Yahya Kusumo",
            "Perkasa Mustofa",
            "Anita Pradipta",
            "Wahyu Yolanda",
            "Bakiman Hassanah",
            "Jarwadi Yolanda",
            "Tirtayasa Anggriawan",
            "Nyana Januar",
            "Tira Sitompul",
            "Almira Mulyani",
            "Ganep Napitupulu",
            "Kawaya Siregar",
            "Samiah Handayani",
            "Kawaca Puspita",
            "Kadir Sitompul",
            "Emas Saragih",
            "Karja Sihotang",
            "Daniswara Pangestu",
            "Martaka Laksita",
            "Tania Situmorang",
            "Kariman Yulianti",
            "Harto Nainggolan",
            "Juli Iswahyudi",
            "Prayogo Aryani",
            "Gandi Pranowo",
            "Dariati Maheswara",
            "Mumpuni Saragih",
            "Legawa Wahyuni",
            "Gaiman Rahimah",
            "Lurhur Wijayanti",
            "Yulia Haryanti",
            "Nalar Rahmawati",
            "Yuni Nashiruddin",
            "Bakiman Nababan",
            "Ayu Waluyo",
            "Cornelia Yuliarti",
            "Zaenab Natsir",
            "Ida Pradana",
            "Puti Mandasari",
            "Kasiran Habibi",
            "Aditya Lailasari",
            "Indah Simanjuntak",
            "Kasim Dongoran",
            "Janet Anggraini",
            "Arsipatra Hidayanto",
            "Putri Mardhiyah",
            "Maida Winarno",
            "Oni Jailani",
            "Najwa Simanjuntak",
            "Perkasa Hasanah",
            "Ganjaran Rajata",
            "Kawaya Gunawan",
            "Luhung Padmasari",
            "Johan Yulianti",
            "Violet Pranowo",
            "Garda Hardiansyah",
            "Wasis Yuniar",
            "Humaira Salahudin",
            "Kenes Rahayu",
            "Jessica Riyanti",
            "Lintang Ardianto",
            "Lukita Gunawan",
            "Vero Riyanti",
            "Rizki Uyainah",
            "Legawa Pratama",
            "Kartika Hidayanto",
            "Prayitna Padmasari",
            "Sabrina Nuraini",
            "Hafshah Hassanah",
            "Bala Hasanah",
            "Mutia Suartini",
            "Rahman Thamrin",
            "Lasmanto Prasetya",
            "Fitria Rahmawati",
            "Marwata Samosir",
            "Aurora Wacana",
            "Nalar Mustofa",
            "Kawaya Hastuti",
            "Cakrabuana Saefullah",
            "Kanda Prasetyo",
            "Hendra Jailani",
            "Vivi Utama",
            "Yunita Prabowo",
            "Tina Novitasari",
            "Bakiman Sudiati",
            "Purwanto Puspasari",
            "Mursita Wacana",
            "Yuni Prasetyo",
            "Ina Nugroho",
            "Martana Saputra",
            "Wirda Melani",
            "Jono Firgantoro",
            "Agus Wahyudin",
            "Nabila Rahimah",
            "Ida Susanti",
            "Gamani Sitorus",
            "Daliman Maheswara",
            "Olivia Iswahyudi",
            "Tasnim Iswahyudi",
            "Aswani Halimah",
            "Ibrahim Saefullah",
            "Irma Hastuti",
            "Hendri Hastuti",
            "Marwata Megantara",
            "Saadat Puspasari",
            "Galur Ardianto",
            "Umaya Laksmiwati",
            "Hendra Waskita",
            "Tirtayasa Haryanti",
            "Eva Wijaya",
            "Dagel Prayoga",
            "Tira Mansur",
            "Simon Yulianti",
            "Paris Widodo",
            "Vivi Pradana",
            "Kamaria Damanik",
            "Garan Saefullah",
            "Ibrani Hutapea",
            "Aditya Hidayat",
            "Purwanto Hartati",
            "Laswi Purnawati",
            "Wakiman Winarsih",
            "Ozy Simbolon",
            "Manah Hardiansyah",
            "Ghaliyati Uwais",
            "Maman Hardiansyah",
            "Kambali Susanti",
            "Kuncara Manullang",
            "Jelita Astuti",
            "Putu Damanik",
            "Kania Haryanto",
            "Widya Pratama",
            "Natalia Gunawan",
            "Prayogo Suartini",
            "Silvia Anggraini",
            "Parman Maheswara",
            "Saadat Prakasa",
            "Karta Farida",
            "Oliva Agustina",
            "Lidya Mandasari",
            "Edi Nainggolan",
            "Argono Saefullah",
            "Galih Sihombing",
            "Vinsen Nugroho",
            "Amalia Narpati",
            "Raden Winarno",
            "Cagak Wacana",
            "Vanesa Yuniar",
            "Ade Wijaya",
            "Adinata Pranowo",
            "Siti Nasyidah",
            "Cager Utama",
            "Parman Sitorus",
            "Ajeng Thamrin",
            "Taswir Sitompul",
            "Latika Safitri",
            "Yulia Pranowo",
            "Vero Kusmawati",
        };
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

        public static Biodata decodeBio(Biodata b)
        {
            Biodata newBio = new Biodata();
            newBio.NIK = b.NIK;
            newBio.nama = RSA.decoder(b.nama);
            newBio.tempat_lahir = RSA.decoder(b.tempat_lahir);
            newBio.tanggal_lahir = RSA.decoder(b.tanggal_lahir);
            newBio.jenis_kelamin = b.jenis_kelamin;
            newBio.golongan_darah = b.golongan_darah;
            newBio.alamat = RSA.decoder(b.alamat);
            newBio.agama = b.agama;
            newBio.status_perkawinan = b.status_perkawinan;
            newBio.pekerjaan = RSA.decoder(b.pekerjaan);
            newBio.kewarganegaraan = RSA.decoder(b.kewarganegaraan);
            return newBio;
        }
        
        public void seedBiodata()
        {
            try
            {
                var records = ResultData.ToList();
                
                // update db collumn name into name_alay
                for (int i = 0; i < records.Count; i++)
                {
                    records[i].nama = StringMatching.toBahasaAlay(list_nama[i]);
                    records[i].nama = RSA.encoder(records[i].nama);
                    // records[i].tempat_lahir = RSA.encoder(records[i].tempat_lahir);
                    // records[i].tanggal_lahir = RSA.encoder(records[i].tanggal_lahir);
                    // records[i].alamat = RSA.encoder(records[i].alamat);
                    // records[i].pekerjaan = RSA.encoder(records[i].pekerjaan);
                    // records[i].kewarganegaraan = RSA.encoder(records[i].kewarganegaraan);
                }
                
                ResultData.UpdateRange(records);
                
                SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("Seeding biodata table is done.");
        }
        
        private void seedSidikJari(string folderPath)
        {
            // drop all column if exists
            sidik_jari.RemoveRange(sidik_jari);
            SaveChanges();
            
            try
            {
                Random random = new Random();
                for (int i =0; i < files.Count; i++)
                {
                    string namaRandom = list_nama[random.Next(list_nama.Count)];
                    for (int j = 1; j <= 10; j++)
                    {
                        if (i*10 + j > files.Count) break;
                        string namaAlay = namaRandom;
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
        
        public List<string> getAllName()
        {
            return ResultData.Select(x => x.nama).ToList();
        }
    }
}