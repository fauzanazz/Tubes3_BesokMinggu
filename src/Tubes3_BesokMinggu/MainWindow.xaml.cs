using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tubes3_BesokMinggu;
using Color = System.Windows.Media.Color;

// await using var db = new Database();

namespace Tubes3_BesokMinggu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        private int TRESHOLD = 50;
        private Database db = new Database(); // Temporary aja karena tidak tau gmn benerin db yg atas
        public Biodata Biodata { get; set; }
        private string _path;
        private ResultData ResultData { get; set; }
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ResultData;
            RSA rsa = new RSA();
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
                HandleResultData(ResultData.Kecocokan);
                HandleButtonReColor(true, KMP);
                HandleButtonReColor(false, BM);
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
                HandleResultData(ResultData.Kecocokan);
                HandleButtonReColor(true, BM);
                HandleButtonReColor(false, KMP);
            });
        }

        private void HandleButtonReColor(bool isActive, object button)
        {
            

            if (isActive && (ResultData.Bio == null || ResultData.Kecocokan < TRESHOLD))
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 1),
                    SpreadMethod = GradientSpreadMethod.Reflect
                };
                
                gradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#44ADF9"), 0)); // White
                gradientBrush.GradientStops.Add(new GradientStop(Colors.Beige, 1)); // Light gray
                ((Button)button).Background = gradientBrush;
                ((Button)button).Foreground = Brushes.Black;
            }
            else
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 1),
                    SpreadMethod = GradientSpreadMethod.Reflect
                };
                
                gradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#F5F5F5"), 0)); // White
                gradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#878787"), 1)); // Light gray
                ((Button)button).Background = gradientBrush;
                ((Button)button).Foreground = Brushes.Black;
                
            }
            
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
                string currentDirectory = Directory.GetCurrentDirectory();
                LoadingBar.Visibility = Visibility.Collapsed;
                MyImage.Source = new BitmapImage(new Uri(Path.Combine(currentDirectory, "UIAsset", "PlusButton.png"))); // Reset Input Images
                OutputImage.Source = null; // Reset Output Images
                DataContext = null;
                MyImage.Height = 80;
                MyImage.Width = 80;
                DataLogging.Visibility = Visibility.Collapsed;
                MessageBox.Show("Refresh Success");
            });
        }
        private Color ConvertHexStringToColor(string hexString)
        {
            // Remove '#' if present
            if (hexString.StartsWith("#"))
            {
                hexString = hexString.Substring(1);
            }
            hexString = "#" + hexString;

            // Convert hexadecimal string to a Color object
            return (Color)ColorConverter.ConvertFromString(hexString);
        }

        private void HandleResultData(double similarity)
        {
            if (similarity < 60)
            {
                SimilarityPercentage.Text = "Match Not Found";
                SimilarityPercentage.Foreground = Brushes.Red;
                DataLogging.Visibility = Visibility.Visible;
                TimeExecution.Visibility = Visibility.Collapsed;
                OutputImage.Source = null;
                DataContext = null;
            }
            else
            {
                OutputImage.Source = new BitmapImage(new Uri(ResultData.ImageOutput));
                OutputImage.Width = 300;
                OutputImage.Height = 360;
                SimilarityPercentage.Text = similarity + "%";
                HandleSimilarityNumber(similarity);
                DataLogging.Visibility = Visibility.Visible;
                TimeExecution.Visibility = Visibility.Visible;
                DataContext = ResultData;
            }
            LoadingBar.Visibility = Visibility.Collapsed;
        }
        
        // Assume the Similarity never goes < 60
        private void HandleSimilarityNumber(double similarity)
        {
            
            double lowerBound = 60;
            
            double range = 100 - lowerBound;

            // Calculate the position of the similarity within the range
            double position = (similarity - lowerBound) / range;

            // Define colors
            Color color;

            // Decide the color based on the position within the range
            if (position < 0.25) // Red to Yellow
            {
                color = ConvertHexStringToColor("#F94444");
            }
            else if (position < 0.5) // Yellow to Green
            {
                color = ConvertHexStringToColor("#F99B44");
            }
            else if (position < 0.75) // Green to Blue
            {
                color = ConvertHexStringToColor("#ECE52C");
            }
            else if(position < 1)
            {
                color = ConvertHexStringToColor("#89F944");
            }
            else
            {
                color = ConvertHexStringToColor("#44ADF9");
            }
            
            SolidColorBrush brush = new SolidColorBrush(color);
            SimilarityPercentage.Foreground = brush;
        }
    }
}