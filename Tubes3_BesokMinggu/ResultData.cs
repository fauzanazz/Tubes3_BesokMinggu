namespace Tubes3_BesokMinggu;

using System.ComponentModel;

public class ResultData : INotifyPropertyChanged
{
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}