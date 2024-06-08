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
        
        private int TRESHOLD = 60;
        private Database db = new Database(); // Temporary aja karena tidak tau gmn benerin db yg atas
        public Biodata Biodata { get; set; }
        private string _path;
        private ResultData ResultData { get; set; }
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ResultData;
            
            // test RSA
            string temp = RSA.encoder("Test Message");
            string res = RSA.decoder(temp);
            temp = RSA.encoder("5169441504764349");
            res = RSA.decoder(temp);
            temp = RSA.encoder("dRj lAK5MiWATi");
            res = RSA.decoder(temp);
            temp = RSA.encoder("Probolinggo");
            res = RSA.decoder(temp);
            temp = RSA.encoder("1984-10-03");
            res = RSA.decoder(temp);
            temp = RSA.encoder("Gg. Siliwangi No. 0");
            res = RSA.decoder(temp);
            temp = RSA.encoder("Airline pilot");
            res = RSA.decoder(temp);
            temp = RSA.encoder("Indonesia");
            res = RSA.decoder(temp);
            
            
            // db.seedBiodata(); // WARNING: Jangan di uncomment kecuali mau ngeinsert semua biodata ke database ulang
            string currentDirectory = Directory.GetCurrentDirectory();
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
            
            if (ResultData.Bio == null || ResultData.Kecocokan < TRESHOLD)
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
            
            if (ResultData.Bio == null || ResultData.Kecocokan < TRESHOLD)
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