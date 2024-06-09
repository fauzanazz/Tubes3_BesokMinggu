using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Tubes3_BesokMinggu;

public partial class LoadingSpinner : UserControl
{
    public LoadingSpinner()
    {
        InitializeComponent();
    }
        
        public Duration Duration {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(LoadingSpinner), new PropertyMetadata(default(Duration)));

        public Brush SpinnerColor {
            get { return (Brush)GetValue(SpinnerColorProperty); }
            set { SetValue(SpinnerColorProperty, value); }
        }

        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(LoadingSpinner), new PropertyMetadata(Brushes.DodgerBlue));
}