using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Shapes;
using AForge.Imaging.Filters;
using Rectangle = System.Drawing.Rectangle;

namespace Tubes3_BesokMinggu;

public static class Solver
{
    private static int SIZE = 50;
    public static Database DB = new Database();
    
    public static ResultData SolveBM(string path)
    {
        long start = DateTime.Now.Ticks;
        
        // Load the image
        Bitmap image = ProcessImage(path);
        
        // Convert the image to binary
        byte[] binary = ImageToByteArray(image);
        
        // Crop the middle of the binary image
        byte[] croppedBinary = CropMiddleBinary(binary);
        
        // Convert the binary to ASCII
        string ascii = BinaryToASCII(croppedBinary);
        
        // Make the full binary string for levenshtein distance
        string fullBinary = BinaryToASCII(ImageToByteArray(image));
        
        // Find the closest match in the database
        var result = new { nama = "", berkas_citra = "", Distance = 0.0 };
        foreach (var s in DB.sidik_jari)
        {
            var distance = SolveBoyerMoore(s.berkas_citra, ascii, fullBinary);
            if (distance == 100)
            {
                result = new { s.nama, s.berkas_citra, Distance = distance };
                break;
            }
            if (distance > result.Distance)
            {
                result = new { s.nama, s.berkas_citra, Distance = distance };
            }
        }
        
        // Find the biodata in the database based on the name
        string regexPattern = StringMatching.getBahasaAlayPattern(result.nama);
        var biodata = DB.ResultData
            .AsEnumerable()
            .FirstOrDefault(b => StringMatching.isMatch(b.nama, regexPattern));
        
        long end = DateTime.Now.Ticks; // Calculate the time taken
        
        return new ResultData(biodata, new sidik_jari(){nama = result.nama, berkas_citra = result.berkas_citra}, (int)((end - start) / TimeSpan.TicksPerMillisecond), result.Distance);
    }
    
    public static ResultData SolveKMP(string path)
    {
        long start = DateTime.Now.Ticks;
        
        // Load the image
        Bitmap image = ProcessImage(path);
        
        // Convert the image to binary
        byte[] binary = ImageToByteArray(image);
        
        // Crop the middle of the binary image
        byte[] croppedBinary = CropMiddleBinary(binary);
        
        // Convert the binary to ASCII
        string ascii = BinaryToASCII(croppedBinary);
        
        // Make the full binary string for levenshtein distance
        string fullBinary = BinaryToASCII(ImageToByteArray(image));
        
        // Find the closest match in the database
        var result = new { nama = "", berkas_citra = "", Distance = 0.0 };
        foreach (var s in DB.sidik_jari)
        {
            double distance = SolveKMP(s.berkas_citra, ascii, fullBinary);
            if (distance == 100)
            {
                result = new { s.nama, s.berkas_citra, Distance = distance };
                break;
            }
            if (distance > result.Distance)
            {
                result = new { s.nama, s.berkas_citra, Distance = distance };
            }
        }
        
        // Find the biodata in the database based on the name
        string regexPattern = StringMatching.getBahasaAlayPattern(result.nama);
        var biodata = DB.ResultData
            .AsEnumerable()
            .FirstOrDefault(b => StringMatching.isMatch(b.nama, regexPattern));
        
        long end = DateTime.Now.Ticks; // Calculate the time taken
        
        return new ResultData(biodata, new sidik_jari(){nama = result.nama, berkas_citra = result.berkas_citra}, (int)((end - start) / TimeSpan.TicksPerMillisecond), result.Distance);

    }

    private static double SolveKMP(string text, string pattern, string fullPattern)
    {
        // Load txt file named text
        string texts = System.IO.File.ReadAllText(text.Replace("BMP", "txt"));
        var number = Algorithm.BoyerMoore(texts, pattern);
        
        if (number == -1) // Jika tidak ditemukan
        {
            int levenshteinDistance = Algorithm.LevenshteinDistance(texts, fullPattern, SIZE);
            return (SIZE - levenshteinDistance);
        }
        
        return 100;
    }
    private static double SolveBoyerMoore(string text, string pattern, string fullPattern)
    {
        // Load txt file named text
        string texts = System.IO.File.ReadAllText(text.Replace("BMP", "txt"));
        var number = Algorithm.BoyerMoore(texts, pattern);
        
        if (number == -1) // Jika tidak ditemukan
        {
            int levenshteinDistance = Algorithm.LevenshteinDistance(texts, fullPattern, SIZE);
            return (SIZE - levenshteinDistance);
        }
        
        return 100;
    }
    public static Bitmap ProcessImage(String path)
    {
        // Load an image from file
        // Check if the file exists
        if (!System.IO.File.Exists(path))
        {
            throw new System.IO.FileNotFoundException("The specified file does not exist.", path);
        }

        // Load an image from file
        Bitmap image;
        try
        {
            image = new Bitmap(path);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("The specified file could not be loaded as a Bitmap. Ensure the file is a valid image file.", nameof(path));
        }
        
        // Histogram Equalization
        HistogramEqualization histogramEqualization = new HistogramEqualization();
        Bitmap equalizedImage = histogramEqualization.Apply(image);
        
        // Convert the image to a supported format
        equalizedImage = ConvertToSupportedFormat(equalizedImage);
        
        // Convert the image to grayscale
        Grayscale grayscale = new Grayscale(0.2125, 0.7154, 0.0721);
        Bitmap grayscaled = grayscale.Apply(equalizedImage);
        
        // Normalize the pixel values
        grayscaled = Normalize(grayscaled);
        
        // Apply thresholding
        Threshold threshold = new Threshold(127);
        threshold.ApplyInPlace(grayscaled);

        return grayscaled;
    }
    
    private static byte[] CropMiddleBinary(byte[] binary)
    {
        // Get the 50 pixel around the middle
        int x = binary.Length / 2;
        
        int startX = x - SIZE/2;
        
        return binary.Skip(startX).Take(SIZE).ToArray();
    }
    private static Bitmap ConvertToSupportedFormat(Bitmap image)
    {
        // Create a new Bitmap with a supported pixel format
        var newImage = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        // Draw the old image onto the new Bitmap
        using var g = Graphics.FromImage(newImage);
        g.DrawImage(image, 0, 0, image.Width, image.Height);

        return newImage;
    }
    private static Bitmap CropImage(Bitmap image, int x, int y, int width, int height)
    {
        Rectangle cropRect = new Rectangle(x, y, width, height);
        Bitmap croppedImage = image.Clone(cropRect, image.PixelFormat);
        return croppedImage;
    }
    private static Bitmap Normalize(Bitmap image)
    {
        BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

        // Get the address of the first line.
        IntPtr ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap.
        int bytes = Math.Abs(bmpData.Stride) * image.Height;
        byte[] rgbValues = new byte[bytes];

        // Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        // Find the minimum and maximum pixel values
        byte minPixelValue = 255;
        byte maxPixelValue = 0;
        for (int i = 0; i < rgbValues.Length; i++)
        {
            if (rgbValues[i] < minPixelValue)
                minPixelValue = rgbValues[i];
            if (rgbValues[i] > maxPixelValue)
                maxPixelValue = rgbValues[i];
        }

        // Normalize the pixel values
        for (int i = 0; i < rgbValues.Length; i++)
        {
            rgbValues[i] = (byte)((rgbValues[i] - minPixelValue) * 255 / (maxPixelValue - minPixelValue));
        }

        // Copy the RGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

        // Unlock the bits.
        image.UnlockBits(bmpData);
        
        return image;
    }
    public static byte[] ImageToByteArray(Bitmap image)
    {
        BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

        // Get the address of the first line.
        IntPtr ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap.
        int bytes = Math.Abs(bmpData.Stride) * image.Height;
        byte[] rgbValues = new byte[bytes];

        // Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        // Unlock the bits.
        image.UnlockBits(bmpData);

        // Apply threshold to convert pixel values to binary (1s and 0s)
        byte threshold = 127; // You can adjust this value as needed
        for (int i = 0; i < rgbValues.Length; i++)
        {
            rgbValues[i] = rgbValues[i] > threshold ? (byte)1 : (byte)0;
            rgbValues[i] += 70;
        }
        
        return rgbValues;
    }
    public static string BinaryToASCII(byte[] binary)
    {
        string ascii = System.Text.Encoding.ASCII.GetString(binary);
        return ascii;
    }
    public static string BinaryToString(byte[] binary)
    {
        string strings = "";
        foreach (var VARIABLE in binary)
        {
            strings += VARIABLE.ToString();
            
        }
        return strings;
    }
}