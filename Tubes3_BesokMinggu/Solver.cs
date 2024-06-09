using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
using Rectangle = System.Drawing.Rectangle;

namespace Tubes3_BesokMinggu;

public static class Solver
{
    private const int SIZE = 64;
    public static Database DB = new Database();
    
    public static ResultData Solve(string path, Func<string, string, string, double> calculateSimilarity)
    {
        long start = DateTime.Now.Ticks;
        Bitmap image = ProcessImage(path);
        byte[] binary = ImageToByteArray(image);
        byte[] croppedBinary = CropMiddleBinary(binary);
        string ascii = BinaryToASCII(croppedBinary);
        string fullBinary = BinaryToASCII(ImageToByteArray(image));

        var result = new { nama = "", berkas_citra = "", Distance = 0.0 };
        var highestsimsimage = "";
        
        var lockObject = new object();
        bool stop = false;
        
        ParallelOptions parallelOptions = new ParallelOptions();
        parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

        Parallel.ForEach(DB.sidik_jari, parallelOptions, (s, state) =>
        {
            if (stop) state.Stop();
            double distance = calculateSimilarity(s.berkas_citra, ascii, fullBinary);
            if (distance >= 100 || distance > result.Distance)
            {
                lock (lockObject)
                {
                    result = new { s.nama, s.berkas_citra, Distance = distance };
                    highestsimsimage = s.berkas_citra;
                }
                
                if (distance == 100)
                {
                    stop = true;
                    state.Stop();
                }
            }
        });

        var biodata = DB.ResultData
            .AsEnumerable()
            .FirstOrDefault(b => StringMatching.isMatch(result.nama, StringMatching.getBahasaAlayPattern(RSA.decoder(b.nama))));

        long end = DateTime.Now.Ticks;
        return new ResultData(Database.decodeBio(biodata), new sidik_jari(){nama = result.nama, berkas_citra = result.berkas_citra}, (int)((end - start) / TimeSpan.TicksPerMillisecond), result.Distance, highestsimsimage);
    }
    public static ResultData SolveBM(string path)
    {
        return Solve(path, CalculateBoyerMooreSimilarity);
    }
    public static ResultData SolveKMP(string path)
    {
        return Solve(path, CalculateKMPSimilarity);
    }
    private static double CalculateKMPSimilarity(string text, string pattern, string fullPattern)
    {
        // Load txt file named text
        string texts = System.IO.File.ReadAllText(text.Replace("BMP", "txt"));
        var number = Algorithm.KMPSearch(texts, pattern);
        
        if (number == -1) // Jika tidak ditemukan
        {
            return CalculateLevenshteinDistance(texts, fullPattern);
        }
        
        return 100;
    }
    private static double CalculateBoyerMooreSimilarity(string text, string pattern, string fullPattern)
    {
        // Load txt file named text
        string texts = System.IO.File.ReadAllText(text.Replace("BMP", "txt"));
        var number = Algorithm.BoyerMoore(texts, pattern);
        
        if (number == -1) // Jika tidak ditemukan
        {
            return CalculateLevenshteinDistance(texts, fullPattern);
        }
        
        return 100;
    }
    private static double CalculateLevenshteinDistance(string texts, string pattern)
    {
        int levenshteinDistance = Algorithm.LevenshteinDistance(texts, pattern, SIZE);
        return ((double)(SIZE - levenshteinDistance) / SIZE) * 100;
    }
    
    public static Bitmap ProcessImage(String path)
    {
        // Load an image from file
        if (!System.IO.File.Exists(path))// Check if the file exists
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
        
        // If image is not 90x103, resize it
        if (image.Width != 96 || image.Height != 103)
        {
            image = new ResizeBilinear(SIZE, SIZE).Apply(image);
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
            // rgbValues[i] += 70;
        }
        
        return rgbValues;
    }
    public static string BinaryToASCII(byte[] binary)
    {
        byte[] binary2 = new byte[(int) Math.Ceiling((double)(binary.Length) / 8)];
        for (int i = 0; i < binary.Length; i += 8)
        {
            byte b = 0;
            int max = binary.Length - i < 8 ? binary.Length - i : 8;
            for (int j = 0; j < max; j++)
            {
                b <<= 1;
                b |= binary[i + j];
            }
            binary2[i / 8] = b;
        }
        string ascii = System.Text.Encoding.ASCII.GetString(binary2);
        return ascii;
    }
}