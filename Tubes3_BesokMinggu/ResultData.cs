using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tubes3_BesokMinggu;

[Table("biodata")]
public class ResultData : INotifyPropertyChanged
{
    
    [Key]
    public string NIK
    {
        get { return NIK; }
        set
        {
            if (NIK != value)
            {
                NIK = value;
                OnPropertyChanged("NIK");
            }
        }
    }
    
    public string nama
    {
        get { return nama; }
        set
        {
            if (nama != value)
            {
                nama = value;
                OnPropertyChanged("nama");
            }
        }
    }

    public string TempatLahir
    {
        get { return TempatLahir; }
        set
        {
            if (TempatLahir != value)
            {
                TempatLahir = value;
                OnPropertyChanged("TempatLahir");
            }
        }
    }
    
    public string TanggalLahir
    {
        get { return TanggalLahir; }
        set
        {
            if (TanggalLahir != value)
            {
                TanggalLahir = value;
                OnPropertyChanged("TanggalLahir");
            }
        }
    }
    
    public string JenisKelamin
    {
        get { return JenisKelamin; }
        set
        {
            if (JenisKelamin != value)
            {
                JenisKelamin = value;
                OnPropertyChanged("JenisKelamin");
            }
        }
    }
    
    public string GolonganDarah
    {
        get { return GolonganDarah; }
        set
        {
            if (GolonganDarah != value)
            {
                GolonganDarah = value;
                OnPropertyChanged("GolonganDarah");
            }
        }
    }
    
    public string Alamat
    {
        get { return Alamat; }
        set
        {
            if (Alamat != value)
            {
                Alamat = value;
                OnPropertyChanged("Alamat");
            }
        }
    }
    
    public string Agama
    {
        get { return Agama; }
        set
        {
            if (Agama != value)
            {
                Agama = value;
                OnPropertyChanged("Agama");
            }
        }
    }
    
    public string StatusPerkawinan
    {
        get { return StatusPerkawinan; }
        set
        {
            if (StatusPerkawinan != value)
            {
                StatusPerkawinan = value;
                OnPropertyChanged("StatusPerkawinan");
            }
        }
    }
    
    public string Pekerjaan
    {
        get { return Pekerjaan; }
        set
        {
            if (Pekerjaan != value)
            {
                Pekerjaan = value;
                OnPropertyChanged("Pekerjaan");
            }
        }
    }
    
    public string Kewarganegaraan
    {
        get { return Kewarganegaraan; }
        set
        {
            if (Kewarganegaraan != value)
            {
                Kewarganegaraan = value;
                OnPropertyChanged("Kewarganegaraan");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}