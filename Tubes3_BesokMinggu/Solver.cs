using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using Image = AForge.Imaging.Image;

namespace Tubes3_BesokMinggu;

public static class Solver
{
    public static int SolveKMP(string text, string pattern)
    {
        int number =  Algorithm.KMPSearch(text, pattern);
        if (number == -1)
        {
            return 0;
        }
        return Algorithm.LevenshteinDistance(text, pattern);
    }
    
    public static int SolveBoyerMoore(string text, string pattern)
    {
        int number = Algorithm.BoyerMoore(text, pattern);
        if (number == -1)
        {
            return 0;
        }
        return Algorithm.LevenshteinDistance(text, pattern);
    }

    public static byte[] FileToProcessing(string filePath)
    {
        // Load the BMP
        Bitmap image = (Bitmap)Image.FromFile(filePath);
        
        // Apply the texture filter to the image
        Texturer texturer = new Texturer(new TextileTexture(), 1.0, 0.5);
        Bitmap textureImage = texturer.Apply(image);
    
        // Apply the threshold filter to the image
        Threshold thresholdFilter = new Threshold(127);
        Bitmap thresholdImage = thresholdFilter.Apply(textureImage);
    
        // Apply the edge filter to the image
        SobelEdgeDetector edgeFilter = new SobelEdgeDetector();
        Bitmap edgeImage = edgeFilter.Apply(thresholdImage);
    
        // Apply the hough line transform to the image
        HoughLineTransformation lineTransform = new HoughLineTransformation();
        lineTransform.ProcessImage(edgeImage);
    
        // Get the lines from the hough line transform
        HoughLine[] lines = lineTransform.GetLinesByRelativeIntensity(0.5);
    
        // Draw the lines on the image
        BitmapData data = edgeImage.LockBits(new Rectangle(0, 0, edgeImage.Width, edgeImage.Height), ImageLockMode.ReadWrite, edgeImage.PixelFormat);
        for (int i = 0; i < lines.Length - 1; i++)
        {
            // Convert the Theta values to IntPoint objects
            int x1 = (int)(edgeImage.Width / 2 + lines[i].Radius * Math.Cos(lines[i].Theta * Math.PI / 180));
            int y1 = (int)(edgeImage.Height / 2 + lines[i].Radius * Math.Sin(lines[i].Theta * Math.PI / 180));
            int x2 = (int)(edgeImage.Width / 2 + lines[i+1].Radius * Math.Cos(lines[i+1].Theta * Math.PI / 180));
            int y2 = (int)(edgeImage.Height / 2 + lines[i+1].Radius * Math.Sin(lines[i+1].Theta * Math.PI / 180));

            AForge.IntPoint start = new AForge.IntPoint(x1, y1);
            AForge.IntPoint end = new AForge.IntPoint(x2, y2);

            // Draw the line
            Drawing.Line(data, start, end, Color.White);
        }
    
        edgeImage.UnlockBits(data);
    
        // Show the image
        edgeImage.Save("output.png", ImageFormat.Png);
        
        // Convert the image to a byte array
        byte[] binary = ImageToBinary(edgeImage);
        
        return binary;
    }

    private static byte[] ImageToBinary(Bitmap edgeImage)
    {
        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(edgeImage, typeof(byte[]));
    }

    public static string BinaryToASCII(byte[] binary)
    {
        return System.Text.Encoding.ASCII.GetString(binary);
    }
}