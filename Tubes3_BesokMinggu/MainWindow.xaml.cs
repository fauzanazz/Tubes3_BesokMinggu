using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tubes3_BesokMinggu;

// await using var db = new Database();

namespace Tubes3_BesokMinggu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        private Database db = new Database(); // Temporary aja karena tidak tau gmn benerin db yg atas
        public Biodata Biodata { get; set; }
        private string _path;
        private ResultData ResultData { get; set; }
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ResultData;
            
            
            // db.refreshSeed(Path.Combine(currentDirectory,"Dataset")); // ini buat ngeinsert semua sidik jari ke database
        }
        
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                MyImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                MyImage.Stretch = Stretch.Uniform;
                MyImage.Width = 300;
                MyImage.Height = 360;
                
                _path = openFileDialog.FileName;
            }

        }
        
        private async void KMPClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }
            LoadingBar.Visibility = Visibility.Visible;
            ResultData = await Task.Run(() => Solver.SolveKMP(_path));
            
            Dispatcher.Invoke(() =>
            {
                if (ResultData.Bio == null || ResultData.Kecocokan < 10)
                {
                    MessageBox.Show("No match found.");
                    return;
                }
                OutputImage.Source = new BitmapImage(new Uri(ResultData.ImageOutput));
                OutputImage.Width = 300;
                OutputImage.Height = 360;
                this.DataContext = ResultData;
                LoadingBar.Visibility = Visibility.Collapsed;
            });
        }

        private async void BoyerMooreClick(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            LoadingBar.Visibility = Visibility.Visible;

            ResultData = await Task.Run(() => Solver.SolveBM(_path));
            
            Dispatcher.Invoke(() =>
            {
                if (ResultData.Bio == null || ResultData.Kecocokan < 10)
                {
                    MessageBox.Show("No match found.");
                    return;
                }
                OutputImage.Source = new BitmapImage(new Uri(ResultData.ImageOutput));
                OutputImage.Width = 300;
                OutputImage.Height = 360;
                DataContext = ResultData;
                LoadingBar.Visibility = Visibility.Collapsed;
            });
        }

        private async void RefreshClick(object sender, RoutedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Visible;
            await Task.Run(() =>
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                db.refreshSeed(Path.Combine(currentDirectory, "Dataset"));
            });
            Dispatcher.Invoke(() =>
            {
                LoadingBar.Visibility = Visibility.Collapsed;
            });
        }
    }
}