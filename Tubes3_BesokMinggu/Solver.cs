using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using AForge.Imaging.Filters;
namespace Tubes3_BesokMinggu;

public static class Solver
{
    public static Database DB = new Database();
    
    public static ResultData SolveBM(string path)
    {
        long start = DateTime.Now.Ticks;
        
        // Load the image
        Bitmap image = ProcessImage(path);
        
        // Crop the middle of the image
        Bitmap croppedImage = CropMiddleImage(image);
        
        // Convert the cropped image to binary
        byte[] binary = ImageToByteArray(croppedImage);
        
        // Convert the binary to ASCII
        string ascii = BinaryToASCII(binary);
        
        // Make the full binary string for levenshtein distance
        string fullBinary = BinaryToASCII(ImageToByteArray(image));
        
        // Find the closest match in the database
        var result = DB.sidik_jari
            .AsEnumerable() // Add this line to switch to client-side evaluation
            .Select(s => new {s.nama, s.berkas_citra, Distance = SolveBoyerMoore(s.berkas_citra, ascii, fullBinary)}).MaxBy(s => s.Distance);
        
        // Find the biodata in the database based on the name
        var biodata = DB.ResultData
            .AsEnumerable()
            .FirstOrDefault(b => StringMatching.isMatch(b.nama,StringMatching.getBahasaAlayPattern(result.nama)));
        
        long end = DateTime.Now.Ticks; // Calculate the time taken
        
        return new ResultData(biodata, null, (int)((end - start) / TimeSpan.TicksPerMillisecond), result.Distance);
    }
    
    public static ResultData SolveKMP(string path)
    {
        long start = DateTime.Now.Ticks;
        
        // Load the image
        Bitmap image = ProcessImage(path);
        
        // Crop the middle of the image
        Bitmap croppedImage = CropMiddleImage(image);
        
        // Convert the cropped image to binary
        byte[] binary = ImageToByteArray(croppedImage);
        
        // Convert the binary to ASCII
        string ascii = BinaryToASCII(binary);
        
        // Make the full binary string for levenshtein distance
        string fullBinary = BinaryToASCII(ImageToByteArray(image));
        
        // Find the closest match in the database
        var result = DB.sidik_jari
            .AsEnumerable() // Add this line to switch to client-side evaluation
            .Select(s => new {s.nama, s.berkas_citra, Distance = SolveKMP(s.berkas_citra, ascii, fullBinary )}).MinBy(s => s.Distance);
        
        if (result != null && result.Distance < 10)
        {
            long ends = DateTime.Now.Ticks;
            return new ResultData(null, null, (int)((ends - start) / TimeSpan.TicksPerMillisecond), 0);
        }
        
        // Find the biodata in the database based on the name
        var biodata = DB.ResultData
            .FirstOrDefault(b => StringMatching.isMatch(b.nama,StringMatching.getBahasaAlayPattern(result.nama)));
        
        if (biodata == null)
        {
            long ended = DateTime.Now.Ticks;
            return new ResultData(null, null, (int)((ended - start) / TimeSpan.TicksPerMillisecond), 0);
        }
        
        long end = DateTime.Now.Ticks;
        return new ResultData(biodata, null, (int)((end - start) / TimeSpan.TicksPerMillisecond), result.Distance);
    }

    private static double SolveKMP(string text, string pattern, string fullPattern)
    {
        var number =  Algorithm.KMPSearch(text, pattern);
        
        if (number == -1) // Jika tidak ditemukan
        {
            int levenshteinDistance = Algorithm.LevenshteinDistance(text, fullPattern);
           double similarity = (double)(text.Length - levenshteinDistance) / text.Length;
           return similarity < 0.0 ? 0 : similarity;
        }
        
        return 100;
    }
    private static double SolveBoyerMoore(string text, string pattern, string fullPattern)
    {
        var number = Algorithm.BoyerMoore(text, pattern);
        
        if (number == -1) // Jika tidak ditemukan
        {
            int levenshteinDistance = Algorithm.LevenshteinDistance(text, fullPattern);
            double similarity = (double)(text.Length - levenshteinDistance) / text.Length;
            return similarity < 0.0 ? 0 : similarity;
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
    private static Bitmap CropMiddleImage(Bitmap image)
    {
        // Get the 36 pixel around the middle
        int x = image.Width / 2;
        int y = image.Height / 2;
        
        int startX = x - 3;
        int startY = y - 3;
        
        return CropImage(image, startX, startY, 6, 6);
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