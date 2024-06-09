using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tubes3_BesokMinggu;

[Table("biodata")]
public class Biodata : INotifyPropertyChanged
{
    
    private string _NIK;

    [Key]
    public string NIK {
        get { return _NIK; }
        set
        {
            if (_NIK != value)
            {
                _NIK = value;
                OnPropertyChanged("NIK");
            }
        }
    }
    
    private string _nama;
    public string nama
    {
        get { return _nama; }
        set
        {
            if (_nama != value)
            {
                _nama = value;
                OnPropertyChanged("nama");
            }
        }
    }
    
    private string _tempat_lahir;

    public string tempat_lahir
    {
        get { return _tempat_lahir; }
        set
        {
            if (_tempat_lahir != value)
            {
                _tempat_lahir = value;
                OnPropertyChanged("TempatLahir");
            }
        }
    }
    
    private string _tanggal_lahir;
    
    public string tanggal_lahir
    {
        get { return _tanggal_lahir; }
        set
        {
            if (_tanggal_lahir != value)
            {
                _tanggal_lahir = value;
                OnPropertyChanged("TanggalLahir");
            }
        }
    }
    
    private string _jenis_kelamin;
    
    public string jenis_kelamin
    {
        get { return _jenis_kelamin; }
        set
        {
            if (_jenis_kelamin != value)
            {
                _jenis_kelamin = value;
                OnPropertyChanged("JenisKelamin");
            }
        }
    }
    
    private string _golongan_darah;
    
    public string golongan_darah
    {
        get { return _golongan_darah; }
        set
        {
            if (_golongan_darah != value)
            {
                _golongan_darah = value;
                OnPropertyChanged("GolonganDarah");
            }
        }
    }
    
    private string _alamat;
    
    public string alamat
    {
        get { return _alamat; }
        set
        {
            if (_alamat != value)
            {
                _alamat = value;
                OnPropertyChanged("Alamat");
            }
        }
    }
    
    private string _agama;
    
    public string agama
    {
        get { return _agama; }
        set
        {
            if (_agama != value)
            {
                _agama = value;
                OnPropertyChanged("Agama");
            }
        }
    }
    
    private string _status_perkawinan;
    
    public string status_perkawinan
    {
        get { return _status_perkawinan; }
        set
        {
            if (_status_perkawinan != value)
            {
                _status_perkawinan = value;
                OnPropertyChanged("StatusPerkawinan");
            }
        }
    }
    
    private string _pekerjaan;
    
    public string pekerjaan
    {
        get { return _pekerjaan; }
        set
        {
            if (_pekerjaan != value)
            {
                _pekerjaan = value;
                OnPropertyChanged("Pekerjaan");
            }
        }
    }
    
    private string _kewarganegaraan;
    
    public string kewarganegaraan
    {
        get { return _kewarganegaraan; }
        set
        {
            if (_kewarganegaraan != value)
            {
                _kewarganegaraan = value;
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