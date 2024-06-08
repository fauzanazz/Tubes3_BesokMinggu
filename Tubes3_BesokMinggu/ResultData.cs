using System;
using System.ComponentModel;

namespace Tubes3_BesokMinggu;

public class ResultData : INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public ResultData(Biodata bio, SidikJari sidik, int lamaEksekusi, double kecocokan)
    {
        Bio = bio;
        Sidik = sidik;
        LamaEksekusi = lamaEksekusi;
        Kecocokan = kecocokan;
    }
    
    private Biodata _bio;
    public Biodata Bio
    {
        get { return _bio; }
        set
        {
            if (_bio != value)
            {
                _bio = value;
                OnPropertyChanged(nameof(Bio));
            }
        }
    }
    
    private SidikJari _sidik;
    
    public SidikJari Sidik
    {
        get { return _sidik; }
        set
        {
            if (_sidik != value)
            {
                _sidik = value;
                OnPropertyChanged(nameof(Sidik));
            }
        }
    }
    
    private int _lamaEksekusi;
    
    public int LamaEksekusi
    {
        get { return _lamaEksekusi; }
        set
        {
            if (_lamaEksekusi != value)
            {
                _lamaEksekusi = value;
                OnPropertyChanged(nameof(LamaEksekusi));
            }
        }
    }
    
    
    
    private double _kecocokan;
    
    public double Kecocokan
    {
        get { return _kecocokan; }
        set
        {
            double TOLERANCE = 0.0001;
            if (Math.Abs(_kecocokan - value) > TOLERANCE)
            {
                _kecocokan = value;
                OnPropertyChanged(nameof(Kecocokan));
            }
        }
    }
}