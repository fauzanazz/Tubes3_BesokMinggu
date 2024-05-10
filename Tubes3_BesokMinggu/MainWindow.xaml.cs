using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Tubes3_BesokMinggu;

await using var db = new Database("databases.db");

namespace Tubes3_BesokMinggu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        public ResultData ResultData { get; set; }
        
        public MainWindow()
        {
            // StringMatching stringMatching = new StringMatching();
            // string text = "Hello World!";
            // string result = stringMatching.toBahasaAlay(text);
            // Console.WriteLine(result);
            //
            // string pattern = stringMatching.getBahasaAlayPattern(result);
            // Console.WriteLine(pattern);
            //
            // bool isMatch = stringMatching.isMatch(text, pattern);
            // Console.WriteLine(isMatch);
            
            InitializeComponent();
            ResultData = new ResultData();
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
            }
        }
        
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() != true) return;
            
            var text = System.IO.File.ReadAllText(openFileDialog.FileName);
            ResultText.Text = text;
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
            ResultText.ClearValue(System.Windows.Controls.TextBox.TextProperty);
        }
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}