<a name="readme-top"></a>
<div align="center">
  <img src="img/logo.png" alt="mediQ Logo" />
</div>

<br />
<div align="center">

<h3 align="center">
IF2211 Strategi Algoritma</h3>

  <p align="center">
    Tugas Besar 2: Pemanfaatan Pattern Matching dalam Membangun Sistem Deteksi Individu Berbasis
 Biometrik Melalui Citra Sidik Jari
    <br />
    <a href="https://github.com/ValentinoTriadi/Tubes2_OOP"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/ValentinoTriadi/Tubes2_OOP">View Demo</a>
    ·
    <a href="https://github.com/ValentinoTriadi/Tubes2_OOP/issues">Report Bug</a>
    ·
    <a href="https://github.com/ValentinoTriadi/Tubes2_OOP/issues">Request Feature</a>
  </p>
</div>







## Table of Contents
* [General Info](#about-the-project)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Project Status](#project-status)
* [Acknowledgements](#acknowledgements)
* [Contact](#contact)



## About The Project
<p align = "center">This project is a C# Project of Fingerprint Comparison by String amtching with a graphical user interface (GUI) created using Wpf, designed to find the matched fingerprint located in database using both KMP and Boyer Moore to get the matched Fingerprints. When not found, we use a String distance algorithm of Levenshtein Distance to calculate similarity and  return the highest similarity as a result. At the end, the app present the biodata, similarity, and the time execution of the searched one</p>


## Technologies Used
- net6.0 - Windows
- WPF

## Features

- [x] Input Fingerprints : Insert a fingerprints photo to find the matched
- [x] Output Image : The image with the highest similarity or the matched one
- [x] Biodata : The data got from the database with the one of the highest similaritry
- [x] Refresh Button : Reseed the database when a new dataset is inserted
- [x] BoyerMoore Button : Do pattern matching using BoyerMoore technique
- [x] KMP Button : Do pattern matching using KMP technique

## Screenshots
### Main Menu
![Example screenshot](/img/MainMenu.png)

### Game Screen
![Example screenshot](/img/InGame1.png)
![Example screenshot](/img/InGame2.png)
![Example screenshot](/img/InGame3.png)


## Setup

1. Download Executable
  Jika Executable yang di unduh, maka buka aplikasi folder yang di unduh. Jalankan Aplikasi executable yang ada. Masukkan Dataset pada folder Dataset, lalu pencet tombol refresh untuk membuat database sidik jari. Setelah loading selesai, pencet tombol (+) yang ada di kiri tengah. Masukkan sidik jari yang ingin dicari, lalu pilih algoritma yang ingin digunakan untuk mencari.

3. Source code
  Jika Source code yang di unduh, maka buka project sln menggunakan IDE yang Visual Studio atau Rider. Lakukan build / compile. Masukkan Dataset pada folder Dataset, lalu pencet tombol refresh untuk membuat database sidik jari. Setelah loading selesai, pencet tombol (+) yang ada di kiri tengah. Masukkan sidik jari yang ingin dicari, lalu pilih algoritma yang ingin digunakan untuk mencari.

## Project Status
Project is: _completed_


## Acknowledgements
- This project was made as a Strategy Algoritma in Institute Technology Bandung


## Contact
Created by : 
- [Muhammad Fauzan Azhim - 13522153](https://github.com/fauzanazz)
- [Muhammad Davis Adhipramana - 13522157](https://github.com/Loxenary)
- [Valentino Chryslie Triadi - 13522164](https://github.com/ValentinoTriadi)

