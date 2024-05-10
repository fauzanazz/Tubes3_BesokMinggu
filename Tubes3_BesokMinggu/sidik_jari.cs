using System.ComponentModel;

namespace Tubes3_BesokMinggu;

public class sidik_jari : INotifyPropertyChanged
{
    public string berkas_citra
    {
        get { return berkas_citra; }
        set
        {
            if (berkas_citra != value)
            {
                berkas_citra = value;
                OnPropertyChanged("berkas_citra");
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
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}