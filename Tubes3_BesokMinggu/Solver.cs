using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace Tubes3_BesokMinggu;

public static class Solver
{
    public static int SolveKMP(string text, string pattern)
    {
        var number =  Algorithm.KMPSearch(text, pattern);
        return number == -1 ? 0 : Algorithm.LevenshteinDistance(text, pattern);
    }
    
    public static int SolveBoyerMoore(string text, string pattern)
    {
        var number = Algorithm.BoyerMoore(text, pattern);
        return number == -1 ? 0 : Algorithm.LevenshteinDistance(text, pattern);
    }

    public static Bitmap ProcessImage(String path)
    {
        // Load an image from file
        Bitmap image = new Bitmap(path);
        
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
    public static Bitmap ConvertToSupportedFormat(Bitmap image)
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
    public static Bitmap Normalize(Bitmap image)
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
        
        // Save the binary value to file
        System.IO.File.WriteAllBytes("F:/VsCode/CSharp/Tubes3_BesokMinggu/Tubes3_BesokMinggu/BinaryValue.bin", rgbValues);

        return rgbValues;
    }
    private static string BinaryToASCII(byte[] binary)
    {
        return System.Text.Encoding.ASCII.GetString(binary);
    }
}