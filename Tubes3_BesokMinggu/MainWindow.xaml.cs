using System;
using System.Windows;
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
        
        public Biodata Biodata { get; set; }
        private string _path;
        private ResultData ResultData { get; set; }
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ResultData;
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
                _path = openFileDialog.FileName;
            }
        }
        
        private void LoadButton_Click(object sender, RoutedEventArgs e)
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

            this.DataContext = ResultData;
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, ResultText.Text);
            }
        }
        
        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            db.seedSidikJari("F:/Real/");
        }
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}