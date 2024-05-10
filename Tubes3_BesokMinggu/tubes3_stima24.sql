-- MySQL dump 10.13  Distrib 8.0.36, for Linux (x86_64)
--
-- Host: localhost    Database: tubes3_stima24
-- ------------------------------------------------------
-- Server version	8.0.36-0ubuntu0.22.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `biodata`
--

DROP TABLE IF EXISTS `biodata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `biodata` (
  `NIK` varchar(16) NOT NULL,
  `nama` varchar(100) DEFAULT NULL,
  `tempat_lahir` varchar(50) DEFAULT NULL,
  `tanggal_lahir` date DEFAULT NULL,
  `jenis_kelamin` TEXT DEFAULT NULL CHECK( `jenis_kelamin` IN ('Laki-Laki', 'Perempuan')),
  `golongan_darah` varchar(5) DEFAULT NULL,
  `alamat` varchar(255) DEFAULT NULL,
  `agama` varchar(50) DEFAULT NULL,
  `status_perkawinan` DEFAULT NULL CHECK( `status_perkawinan` IN ('Belum Menikah','Menikah','Cerai')),
  `pekerjaan` varchar(100) DEFAULT NULL,
  `kewarganegaraan` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`NIK`)
);
/*!40101 SET character_set_client = @saved_cs_client */;
LOCK TABLES `biodata` WRITE;
INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES 
('9332431607414245', 'Aisyah Zulkarnain', 'Tarakan', '2006-04-30', 'Perempuan', 'A', 'Jalan Rungkut Industri No. 2', 'Konghucu', 'Menikah', 'Research scientist (physical sciences)', 'Kepulauan Riau'),
('4860402409935442', 'Violet Anggriawan', 'Bima', '2004-11-22', 'Perempuan', 'A', 'Gang Merdeka No. 3', 'Islam', 'Belum Menikah', 'Air traffic controller', 'Riau'),
('8524551403298576', 'Hasna Farida', 'Bukittinggi', '1988-09-21', 'Perempuan', 'B', 'Jl. Merdeka No. 19', 'Konghucu', 'Belum Menikah', 'Dance movement psychotherapist', 'Maluku'),
('7540172308671530', 'Vanesa Handayani', 'Langsa', '2000-06-29', 'Laki-Laki', 'AB', 'Jalan Indragiri No. 4', 'Konghucu', 'Cerai', 'Advertising copywriter', 'Maluku Utara'),
('5059252909470110', 'Kamidin Hidayanto', 'Mojokerto', '1997-05-06', 'Perempuan', 'B', 'Gg. Laswi No. 15', 'Budha', 'Belum Menikah', 'Camera operator', 'Kepulauan Riau'),
('3587530711398807', 'Gading Marbun', 'Sabang', '1991-08-22', 'Laki-Laki', 'A', 'Jalan Laswi No. 2', 'Islam', 'Belum Menikah', 'Administrator, arts', 'Kalimantan Selatan'),
('3495401604574556', 'Jamal Prayoga', 'Malang', '2002-04-27', 'Laki-Laki', 'AB', 'Jalan Rajawali Timur No. 52', 'Konghucu', 'Belum Menikah', 'Learning mentor', 'Kalimantan Utara'),
('6569712710158490', 'Pranata Riyanti', 'Binjai', '2001-09-21', 'Perempuan', 'O', 'Gang Tebet Barat Dalam No. 25', 'Konghucu', 'Belum Menikah', 'Jewellery designer', 'Kalimantan Barat'),
('2926491403587671', 'Warta Prayoga', 'Mataram', '1985-02-08', 'Laki-Laki', 'AB', 'Jl. Sukajadi No. 88', 'Kristen', 'Cerai', 'Clothing/textile technologist', 'Sulawesi Utara'),
('6432932704475097', 'Adika Simanjuntak', 'Kota Administrasi Jakarta Utara', '1985-05-13', 'Laki-Laki', 'AB', 'Jalan Gegerkalong Hilir No. 4', 'Katolik', 'Cerai', 'Publishing copy', 'Aceh'),
('4027701712118154', 'Luis Napitupulu', 'Lhokseumawe', '1997-10-22', 'Laki-Laki', 'AB', 'Gang Indragiri No. 852', 'Islam', 'Menikah', 'Adult nurse', 'Kalimantan Tengah'),
('7135682409119688', 'Lasmono Utami', 'Bitung', '1987-04-14', 'Perempuan', 'A', 'Gg. Moch. Ramdan No. 27', 'Budha', 'Menikah', 'Textile designer', 'Kalimantan Utara'),
('2114951512630923', 'Tantri Waskita', 'Pagaralam', '1983-06-09', 'Laki-Laki', 'O', 'Jl. Setiabudhi No. 241', 'Islam', 'Cerai', 'Archaeologist', 'Kalimantan Utara'),
('8668061412975256', 'Ani Putra', 'Bekasi', '1985-03-03', 'Laki-Laki', 'AB', 'Jalan Cikapayang No. 5', 'Katolik', 'Belum Menikah', 'Producer, radio', 'Sulawesi Utara'),
('5584851203161893', 'Kiandra Haryanti', 'Solok', '1996-04-11', 'Laki-Laki', 'A', 'Jl. Pelajar Pejuang No. 588', 'Islam', 'Belum Menikah', 'Pensions consultant', 'Kepulauan Bangka Belitung'),
('8549202105347958', 'Harto Prasetya', 'Bitung', '1991-02-20', 'Laki-Laki', 'O', 'Gg. Jend. A. Yani No. 754', 'Hindu', 'Belum Menikah', 'Electrical engineer', 'Sulawesi Tengah'),
('2532272903894902', 'Daliono Suryatmi', 'Lubuklinggau', '2000-03-04', 'Perempuan', 'B', 'Gg. Ciumbuleuit No. 57', 'Hindu', 'Cerai', 'Careers adviser', 'Papua'),
('7416392008079495', 'Marsito Hutasoit', 'Tidore Kepulauan', '1996-07-08', 'Laki-Laki', 'A', 'Jalan M.T Haryono No. 962', 'Budha', 'Belum Menikah', 'Architectural technologist', 'Aceh'),
('6181860708090544', 'Paiman Ramadan', 'Malang', '1987-11-06', 'Perempuan', 'O', 'Jalan Rawamangun No. 677', 'Katolik', 'Menikah', 'Careers adviser', 'Sulawesi Tenggara'),
('4396073001033147', 'Devi Rahmawati', 'Purwokerto', '2006-06-17', 'Laki-Laki', 'A', 'Jl. Ronggowarsito No. 7', 'Hindu', 'Cerai', 'Surveyor, insurance', 'Sumatera Selatan'),
('9159731510382537', 'Cayadi Mandala', 'Salatiga', '1984-06-01', 'Laki-Laki', 'B', 'Jl. Bangka Raya No. 947', 'Katolik', 'Belum Menikah', 'Therapist, drama', 'Nusa Tenggara Barat'),
('9333090604030377', 'Aurora Palastri', 'Salatiga', '2006-03-27', 'Perempuan', 'O', 'Jl. Rajawali Timur No. 553', 'Islam', 'Belum Menikah', 'Radio broadcast assistant', 'Jawa Tengah'),
('3117330612074474', 'Olivia Sirait', 'Tangerang Selatan', '2005-01-22', 'Laki-Laki', 'AB', 'Gg. Sukajadi No. 915', 'Katolik', 'Cerai', 'Professor Emeritus', 'Jawa Tengah'),
('3656271507514967', 'Vanya Usada', 'Batam', '1991-04-14', 'Laki-Laki', 'B', 'Jalan Pasirkoja No. 978', 'Katolik', 'Belum Menikah', 'Ceramics designer', 'Aceh'),
('8997733010988432', 'Kariman Aryani', 'Tangerang Selatan', '1986-12-28', 'Perempuan', 'AB', 'Jalan Jayawijaya No. 646', 'Budha', 'Cerai', 'Heritage manager', 'Jawa Barat'),
('4045860608575788', 'Anggabaya Hartati', 'Sorong', '1997-02-05', 'Perempuan', 'B', 'Gg. Wonoayu No. 57', 'Katolik', 'Belum Menikah', 'Engineering geologist', 'Gorontalo'),
('5940100108722491', 'Satya Pradipta', 'Pangkalpinang', '2004-04-13', 'Laki-Laki', 'AB', 'Jl. Astana Anyar No. 04', 'Kristen', 'Belum Menikah', 'Exhibition designer', 'Riau'),
('4815100907332230', 'Aslijan Lestari', 'Kendari', '1987-08-20', 'Perempuan', 'O', 'Gang Jakarta No. 20', 'Islam', 'Cerai', 'Naval architect', 'Kalimantan Timur'),
('1983511105836302', 'Catur Wulandari', 'Cirebon', '2005-11-16', 'Laki-Laki', 'O', 'Jl. Gedebage Selatan No. 65', 'Hindu', 'Menikah', 'Investment banker, operational', 'Kalimantan Tengah'),
('7463691803523274', 'Jagaraga Rahayu', 'Tangerang Selatan', '1989-06-22', 'Laki-Laki', 'B', 'Jalan Ciumbuleuit No. 2', 'Islam', 'Menikah', 'Animal technologist', 'Maluku Utara'),
('8462260601265539', 'Ifa Prasetyo', 'Bogor', '1996-11-05', 'Perempuan', 'A', 'Jl. Laswi No. 6', 'Kristen', 'Menikah', 'IT sales professional', 'Jawa Barat'),
('7945471708709998', 'Rusman Nurdiyanti', 'Pekalongan', '1999-07-13', 'Perempuan', 'AB', 'Gang Siliwangi No. 14', 'Budha', 'Menikah', 'Media buyer', 'Papua'),
('1754202411785968', 'Cecep Permadi', 'Tangerang', '1993-04-13', 'Perempuan', 'O', 'Gang Dipenogoro No. 514', 'Kristen', 'Menikah', 'Public relations account executive', 'DKI Jakarta'),
('7905272006622827', 'Harimurti Santoso', 'Batam', '1985-02-07', 'Laki-Laki', 'O', 'Gg. Sadang Serang No. 123', 'Islam', 'Menikah', 'Garment/textile technologist', 'Maluku'),
('4488961107772496', 'Cornelia Nasyiah', 'Kediri', '2000-02-25', 'Perempuan', 'A', 'Gang R.E Martadinata No. 294', 'Konghucu', 'Belum Menikah', 'Office manager', 'Sulawesi Barat'),
('2062380910081440', 'Hadi Nuraini', 'Kota Administrasi Jakarta Timur', '1991-07-31', 'Perempuan', 'A', 'Gang PHH. Mustofa No. 588', 'Kristen', 'Belum Menikah', 'Psychologist, educational', 'Jawa Barat'),
('5324320609207482', 'Makara Mandasari', 'Parepare', '2005-04-14', 'Perempuan', 'AB', 'Gg. Raya Ujungberung No. 9', 'Katolik', 'Cerai', 'Financial trader', 'Kalimantan Barat'),
('8019490612926392', 'Zaenab Najmudin', 'Pekanbaru', '1996-12-06', 'Perempuan', 'A', 'Gang Jakarta No. 957', 'Islam', 'Cerai', 'Biomedical engineer', 'Sulawesi Utara'),
('5245292802110247', 'Lasmanto Kurniawan', 'Magelang', '2005-01-29', 'Perempuan', 'O', 'Gang Moch. Ramdan No. 26', 'Budha', 'Belum Menikah', 'IT trainer', 'Maluku'),
('3107950709477161', 'Sabrina Yulianti', 'Batu', '1994-01-31', 'Laki-Laki', 'O', 'Jl. Otto Iskandardinata No. 0', 'Hindu', 'Belum Menikah', 'Surveyor, minerals', 'Kalimantan Timur'),
('8654642011752216', 'Maryadi Sinaga', 'Lubuklinggau', '1992-05-15', 'Perempuan', 'AB', 'Jl. Sukajadi No. 6', 'Hindu', 'Cerai', 'Patent examiner', 'Sumatera Selatan'),
('1414981412722275', 'Pia Widodo', 'Yogyakarta', '1992-03-30', 'Laki-Laki', 'A', 'Gg. Cikapayang No. 41', 'Kristen', 'Cerai', 'Speech and language therapist', 'Jawa Barat'),
('4714340110506206', 'Galak Astuti', 'Semarang', '2007-01-01', 'Laki-Laki', 'A', 'Gg. Veteran No. 30', 'Kristen', 'Menikah', 'Engineer, site', 'Sumatera Barat'),
('7809421002980956', 'Karja Melani', 'Palopo', '1996-07-14', 'Perempuan', 'O', 'Jl. Siliwangi No. 8', 'Budha', 'Menikah', 'Engineer, petroleum', 'Jambi'),
('3233972601903997', 'Humaira Habibi', 'Manado', '1995-05-18', 'Laki-Laki', 'B', 'Jalan Tebet Barat Dalam No. 01', 'Hindu', 'Cerai', 'Civil engineer, contracting', 'Sulawesi Utara'),
('4510321502364196', 'Rendy Yolanda', 'Tangerang', '1996-11-11', 'Laki-Laki', 'A', 'Jl. Abdul Muis No. 7', 'Hindu', 'Menikah', 'Designer, blown glass/stained glass', 'Bengkulu'),
('5080861206904161', 'Usman Suryatmi', 'Bitung', '1992-08-02', 'Laki-Laki', 'AB', 'Jalan Pasteur No. 8', 'Kristen', 'Menikah', 'Social worker', 'Sulawesi Barat'),
('8326870109603519', 'Praba Gunawan', 'Palopo', '2000-05-31', 'Perempuan', 'B', 'Jalan Dr. Djunjunan No. 23', 'Katolik', 'Cerai', 'Immunologist', 'Kepulauan Riau'),
('1977982612335759', 'Rafid Nainggolan', 'Gorontalo', '1998-02-21', 'Laki-Laki', 'AB', 'Jalan M.H Thamrin No. 592', 'Katolik', 'Belum Menikah', 'Radiographer, diagnostic', 'Aceh'),
('9222452101414954', 'Indah Tampubolon', 'Palangkaraya', '1985-07-22', 'Perempuan', 'AB', 'Jl. Raya Setiabudhi No. 41', 'Kristen', 'Menikah', 'Herpetologist', 'Riau'),
('8035310812573766', 'Wulan Winarno', 'Magelang', '1983-08-25', 'Perempuan', 'A', 'Jalan BKR No. 491', 'Islam', 'Belum Menikah', 'Engineer, civil (contracting)', 'Kalimantan Utara'),
('4121322012800942', 'Marsito Mandala', 'Probolinggo', '1997-12-28', 'Laki-Laki', 'O', 'Gang Monginsidi No. 166', 'Budha', 'Menikah', 'Conference centre manager', 'Nusa Tenggara Barat'),
('3823030909567815', 'Edi Suryono', 'Cimahi', '1995-04-13', 'Perempuan', 'B', 'Gg. Suniaraja No. 1', 'Islam', 'Belum Menikah', 'Producer, television/film/video', 'Sulawesi Barat'),
('2554952909680342', 'Nrima Nuraini', 'Tanjungbalai', '1995-05-05', 'Laki-Laki', 'B', 'Jl. Kendalsari No. 6', 'Katolik', 'Belum Menikah', 'Scientist, research (medical)', 'Papua Barat'),
('3111491606964355', 'Karta Prayoga', 'Kendari', '1991-11-30', 'Laki-Laki', 'A', 'Gang Ciumbuleuit No. 37', 'Hindu', 'Menikah', 'Radio producer', 'Maluku'),
('7173021711552197', 'Harsanto Hartati', 'Depok', '2002-12-15', 'Laki-Laki', 'AB', 'Gg. Rajiman No. 08', 'Katolik', 'Belum Menikah', 'Scientist, research (physical sciences)', 'Sulawesi Barat'),
('3248850503209324', 'Warsa Yolanda', 'Tual', '2001-08-30', 'Laki-Laki', 'AB', 'Jalan Gardujati No. 11', 'Kristen', 'Menikah', 'Commercial art gallery manager', 'Aceh'),
('5055091312360491', 'Kuncara Susanti', 'Dumai', '1998-04-01', 'Laki-Laki', 'A', 'Jalan M.H Thamrin No. 3', 'Konghucu', 'Belum Menikah', 'Designer, television/film set', 'DI Yogyakarta'),
('1491322605081064', 'Ismail Wahyuni', 'Medan', '1992-01-12', 'Perempuan', 'O', 'Jalan Rajawali Timur No. 362', 'Katolik', 'Belum Menikah', 'Transport planner', 'Banten'),
('2395601802199941', 'Balangga Saefullah', 'Binjai', '2001-08-30', 'Perempuan', 'A', 'Gang Dr. Djunjunan No. 54', 'Konghucu', 'Belum Menikah', 'Data processing manager', 'Maluku'),
('1622761101773916', 'Rika Usamah', 'Tangerang Selatan', '2000-05-04', 'Laki-Laki', 'B', 'Jalan Cikutra Barat No. 299', 'Kristen', 'Menikah', 'Psychiatrist', 'Sumatera Utara'),
('6187651803264063', 'Emong Andriani', 'Gorontalo', '1992-08-28', 'Laki-Laki', 'O', 'Gg. Stasiun Wonokromo No. 556', 'Kristen', 'Belum Menikah', 'Accounting technician', 'Nusa Tenggara Barat'),
('6677842106706293', 'Laila Pranowo', 'Bau-Bau', '2002-03-04', 'Perempuan', 'A', 'Jl. R.E Martadinata No. 1', 'Budha', 'Cerai', 'Insurance risk surveyor', 'Kalimantan Utara'),
('4457991010222866', 'Vega Wahyuni', 'Subulussalam', '1994-04-04', 'Perempuan', 'AB', 'Gg. Setiabudhi No. 69', 'Islam', 'Menikah', 'Corporate investment banker', 'Sulawesi Tenggara'),
('3623371712620594', 'Laras Prastuti', 'Blitar', '2006-02-18', 'Perempuan', 'O', 'Jl. HOS. Cokroaminoto No. 03', 'Budha', 'Cerai', 'Neurosurgeon', 'Bengkulu'),
('2422923111266006', 'Adikara Suryatmi', 'Bau-Bau', '1985-07-15', 'Laki-Laki', 'O', 'Jl. Ronggowarsito No. 5', 'Budha', 'Menikah', 'Health visitor', 'Banten'),
('7956833010962511', 'Yani Natsir', 'Kota Administrasi Jakarta Selatan', '2000-10-23', 'Perempuan', 'B', 'Jalan Jayawijaya No. 779', 'Konghucu', 'Belum Menikah', 'Education officer, environmental', 'DKI Jakarta'),
('9479082110909432', 'Talia Wahyuni', 'Kediri', '1992-08-06', 'Laki-Laki', 'A', 'Jl. Raya Ujungberung No. 6', 'Islam', 'Menikah', 'Academic librarian', 'Sulawesi Selatan'),
('1547650812116918', 'Ajimat Usada', 'Parepare', '2001-09-15', 'Perempuan', 'A', 'Gang Kebonjati No. 6', 'Islam', 'Menikah', 'Leisure centre manager', 'Bali'),
('8511042511854811', 'Hartana Sihombing', 'Tangerang', '2004-07-05', 'Perempuan', 'B', 'Gg. Pelajar Pejuang No. 07', 'Kristen', 'Belum Menikah', 'Trading standards officer', 'Kepulauan Riau'),
('6782832006253966', 'Heru Mulyani', 'Serang', '1985-10-31', 'Perempuan', 'O', 'Jl. Pacuan Kuda No. 9', 'Islam', 'Cerai', 'Agricultural consultant', 'Sulawesi Tenggara'),
('8343922702730682', 'Kani Saptono', 'Pekalongan', '2007-04-05', 'Perempuan', 'A', 'Jalan Raya Setiabudhi No. 559', 'Konghucu', 'Cerai', 'Chartered legal executive (England and Wales)', 'Bali'),
('7132811907930098', 'Hasta Nurdiyanti', 'Bima', '2002-05-20', 'Laki-Laki', 'O', 'Jalan H.J Maemunah No. 4', 'Hindu', 'Menikah', 'Fast food restaurant manager', 'Riau'),
('5328410909568514', 'Reksa Narpati', 'Denpasar', '2000-10-21', 'Perempuan', 'AB', 'Gang Rungkut Industri No. 943', 'Kristen', 'Menikah', 'Paramedic', 'DKI Jakarta'),
('6010220604286240', 'Wardi Kurniawan', 'Tarakan', '2000-03-24', 'Laki-Laki', 'B', 'Gang Surapati No. 8', 'Islam', 'Cerai', 'Fast food restaurant manager', 'Gorontalo'),
('6621492304772053', 'Galang Maulana', 'Prabumulih', '1998-11-29', 'Laki-Laki', 'O', 'Jl. Rumah Sakit No. 041', 'Budha', 'Belum Menikah', 'Bonds trader', 'Aceh'),
('2246712207538365', 'Cecep Yulianti', 'Pariaman', '1987-12-04', 'Perempuan', 'AB', 'Jl. Jend. Sudirman No. 4', 'Katolik', 'Cerai', 'Psychologist, sport and exercise', 'Jawa Barat'),
('4569261006414721', 'Clara Widiastuti', 'Meulaboh', '1986-09-25', 'Perempuan', 'AB', 'Jl. Astana Anyar No. 33', 'Budha', 'Belum Menikah', 'Scientist, research (maths)', 'Bali'),
('8056492608360744', 'Saiful Pranowo', 'Pasuruan', '1998-02-11', 'Perempuan', 'A', 'Jl. Cempaka No. 131', 'Katolik', 'Cerai', 'Food technologist', 'Papua'),
('6143431506910584', 'Harsanto Namaga', 'Ternate', '1996-09-28', 'Laki-Laki', 'O', 'Jalan Cihampelas No. 079', 'Islam', 'Menikah', 'Advertising account planner', 'Jawa Tengah'),
('6222282706550750', 'Tirta Pudjiastuti', 'Binjai', '1986-03-07', 'Laki-Laki', 'AB', 'Jalan Cikutra Barat No. 47', 'Konghucu', 'Menikah', 'Engineer, electronics', 'Kalimantan Timur'),
('8666981004948806', 'Bala Haryanti', 'Kotamobagu', '2005-03-02', 'Laki-Laki', 'AB', 'Jalan Otto Iskandardinata No. 57', 'Islam', 'Cerai', 'Personnel officer', 'Riau'),
('4003140504550217', 'Leo Budiman', 'Batam', '1984-10-31', 'Perempuan', 'O', 'Jalan Kutisari Selatan No. 320', 'Katolik', 'Menikah', 'Theatre director', 'Bali'),
('8747972802783572', 'Keisha Handayani', 'Bima', '1998-10-05', 'Laki-Laki', 'A', 'Gg. Jayawijaya No. 767', 'Hindu', 'Cerai', 'Production engineer', 'Riau'),
('1208790210417543', 'Lanjar Yolanda', 'Cirebon', '1995-04-27', 'Laki-Laki', 'AB', 'Jl. BKR No. 560', 'Hindu', 'Cerai', 'Learning mentor', 'Lampung'),
('2279712707959735', 'Rangga Hardiansyah', 'Serang', '1993-09-20', 'Laki-Laki', 'AB', 'Gang Jayawijaya No. 92', 'Hindu', 'Belum Menikah', 'Surveyor, mining', 'Kepulauan Riau'),
('2040792910579421', 'Kadir Prakasa', 'Tasikmalaya', '1996-01-13', 'Laki-Laki', 'A', 'Gg. Jayawijaya No. 977', 'Budha', 'Cerai', 'Video editor', 'DKI Jakarta'),
('3735190702316574', 'Lanjar Halimah', 'Bau-Bau', '2005-12-07', 'Perempuan', 'B', 'Gg. Rungkut Industri No. 294', 'Islam', 'Belum Menikah', 'Diagnostic radiographer', 'Lampung'),
('5542100211330248', 'Ellis Mahendra', 'Padang Sidempuan', '1989-12-28', 'Laki-Laki', 'O', 'Gang Kebonjati No. 5', 'Islam', 'Cerai', 'Accounting technician', 'Sumatera Utara'),
('4287111405421259', 'Ian Palastri', 'Kupang', '1986-10-21', 'Perempuan', 'B', 'Jl. Medokan Ayu No. 3', 'Katolik', 'Cerai', 'Interior and spatial designer', 'Bali'),
('4253520709241100', 'Gada Hutasoit', 'Samarinda', '1989-11-30', 'Perempuan', 'O', 'Gang Cihampelas No. 0', 'Islam', 'Cerai', 'Community education officer', 'Aceh'),
('2531333104476282', 'Balamantri Nashiruddin', 'Bau-Bau', '1991-02-02', 'Laki-Laki', 'O', 'Gang Astana Anyar No. 50', 'Hindu', 'Belum Menikah', 'Surveyor, rural practice', 'Kalimantan Selatan'),
('2216742010309979', 'Bakijan Jailani', 'Serang', '1985-11-04', 'Laki-Laki', 'A', 'Jl. Ciwastra No. 83', 'Katolik', 'Cerai', 'Patent examiner', 'Bali'),
('2548323004587784', 'Salwa Sirait', 'Medan', '1991-12-21', 'Perempuan', 'B', 'Jl. KH Amin Jasuta No. 51', 'Kristen', 'Menikah', 'Lighting technician, broadcasting/film/video', 'Bali'),
('9064880206712948', 'Hasim Situmorang', 'Ambon', '1988-06-27', 'Perempuan', 'AB', 'Gg. Jakarta No. 9', 'Islam', 'Belum Menikah', 'Medical laboratory scientific officer', 'Kepulauan Bangka Belitung'),
('8943781411474418', 'Zamira Astuti', 'Tebingtinggi', '2005-11-13', 'Laki-Laki', 'A', 'Jalan Setiabudhi No. 084', 'Katolik', 'Cerai', 'Merchandiser, retail', 'Jambi'),
('7573173105716450', 'Cagak Sitompul', 'Palembang', '1999-11-21', 'Perempuan', 'B', 'Jl. Jend. Sudirman No. 403', 'Katolik', 'Menikah', 'Engineer, technical sales', 'Maluku Utara'),
('9461401408776336', 'Pandu Anggriawan', 'Singkawang', '1996-05-21', 'Perempuan', 'O', 'Gang M.T Haryono No. 1', 'Katolik', 'Belum Menikah', 'Scientist, research (life sciences)', 'Jawa Barat'),
('1932531001115005', 'Bagus Marpaung', 'Tanjungpinang', '2003-04-20', 'Laki-Laki', 'O', 'Gang Erlangga No. 0', 'Konghucu', 'Menikah', 'Scientist, research (medical)', 'Lampung'),
('2180361407250183', 'Icha Maulana', 'Bandung', '1992-05-30', 'Perempuan', 'A', 'Gg. Antapani Lama No. 2', 'Hindu', 'Cerai', 'Interpreter', 'DKI Jakarta');
UNLOCK TABLES;

--
-- Dumping data for table `biodata`
--


--
-- Table structure for table `sidik_jari`
--

DROP TABLE IF EXISTS `sidik_jari`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sidik_jari` (
  `berkas_citra` text,
  `nama` varchar(100) DEFAULT NULL
);
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sidik_jari`
--

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-04 15:57:34
