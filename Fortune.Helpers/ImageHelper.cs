using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Fortune.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ResizeAndConvertToBlackAndWhite(this byte[] originalImageBytes, int newWidth, int newHeight)
        {
            if (originalImageBytes == null || originalImageBytes.Length == 0)
            {
                throw new ArgumentException("Image bytes cannot be null or empty.");
            }

            using (var inputStream = new MemoryStream(originalImageBytes))
            {
                using (var originalImage = Image.FromStream(inputStream))
                {
                    var resizedImage = new Bitmap(newWidth, newHeight);

                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        // Draw the resized image
                        graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                    }

                    //// Convert the resized image to black and white (grayscale)
                    //ConvertToBlackAndWhite(resizedImage);

                    //// Check if the image is mainly white
                    //bool isMainlyWhite = IsImageMainlyWhite(resizedImage);

                    //// Invert the image if it's mainly white
                    //if (isMainlyWhite)
                    //{
                    //    InvertImage(resizedImage);
                    //}

                    using (var outputStream = new MemoryStream())
                    {
                        resizedImage.Save(outputStream, ImageFormat.Png);
                        return outputStream.ToArray();
                    }
                }
            }
        }

        // Method to convert an image to black and white (grayscale)
        private static void ConvertToBlackAndWhite(Bitmap image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    // Get the pixel color
                    Color originalColor = image.GetPixel(x, y);

                    // Calculate the grayscale value using weighted formula
                    int grayValue = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);

                    // Create a new grayscale color (same value for R, G, B)
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    // Set the pixel color to the grayscale value
                    image.SetPixel(x, y, grayColor);
                }
            }
        }

        // Method to check if the image is mainly white
        private static bool IsImageMainlyWhite(Bitmap image)
        {
            long totalBrightness = 0;
            int pixelCount = image.Width * image.Height;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    totalBrightness += pixelColor.R; // Since the image is grayscale, R, G, B are the same
                }
            }

            // Calculate average brightness
            double averageBrightness = totalBrightness / (double)pixelCount;

            // Threshold for determining if the image is mainly white (0 = black, 255 = white)
            return averageBrightness > 200; // Adjust this threshold value as needed
        }

        // Method to invert a grayscale image
        private static void InvertImage(Bitmap image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color originalColor = image.GetPixel(x, y);

                    // Invert the grayscale value (255 - current value)
                    int invertedValue = 255 - originalColor.R; // R, G, B are the same in grayscale
                    Color invertedColor = Color.FromArgb(invertedValue, invertedValue, invertedValue);

                    image.SetPixel(x, y, invertedColor);
                }
            }
        }
    }
}
