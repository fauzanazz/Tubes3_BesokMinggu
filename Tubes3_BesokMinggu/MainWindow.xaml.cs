using System;
using System.IO;
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
            string currentDirectory = Directory.GetCurrentDirectory();
            db.seedSidikJari("D:\\VSCODE\\STIMA\\SOCOFing\\Real\\"); // ini buat ngeinsert semua sidik jari ke database
            
            db.SaveToTextProcessedSidikJari(Path.Combine(currentDirectory,"Dataset")); // ini buat ngebuat file txt
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
                
                _path = openFileDialog.FileName;
            }

        }
        
        private void KMPClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            ResultData = Solver.SolveKMP(_path);
            
            if (ResultData.Bio == null || ResultData.Kecocokan < 10)
            {
                MessageBox.Show("No match found.");
                return;
            }
            OutputImage.Source = new BitmapImage(new Uri(ResultData.ImageOutput));
            this.DataContext = ResultData;
        }

        private void BoyerMooreClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            ResultData = Solver.SolveBM(_path);
            
            if (ResultData.Bio == null || ResultData.Kecocokan < 10)
            {
                MessageBox.Show("No match found.");
                return;
            }
            OutputImage.Source = new BitmapImage(new Uri(ResultData.ImageOutput));
            this.DataContext = ResultData;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}