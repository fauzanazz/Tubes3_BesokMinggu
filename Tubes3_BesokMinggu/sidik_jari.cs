using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Tubes3_BesokMinggu;

public class sidik_jari : INotifyPropertyChanged
{
    [Key]
    private string _berkas_citra;
    public string berkas_citra
    {
        get { return _berkas_citra; }
        set
        {
            if (_berkas_citra != value)
            {
                _berkas_citra = value;
                OnPropertyChanged("berkas_citra");
            }
        }
    }
    
    [Key]
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
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}