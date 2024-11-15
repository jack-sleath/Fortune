using Fortune.Models.Enums;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Fortune.Helpers
{
    public static class ImageHelper
    {
        public static byte[] Resize(this byte[] originalImageBytes, int newWidth, int newHeight)
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

                    using (var outputStream = new MemoryStream())
                    {
                        resizedImage.Save(outputStream, ImageFormat.Png);
                        return outputStream.ToArray();
                    }
                }
            }
        }

        // Method to convert an image to black and white (grayscale)
        public static byte[] ConvertToBlackAndWhiteTransparency(this byte[] originalImageBytes)
        {
            using (MemoryStream inputStream = new MemoryStream(originalImageBytes))
            {
                // Load the image from the byte array
                using (Bitmap bitmap = new Bitmap(inputStream))
                {
                    // Process the image
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Get the pixel color
                            Color pixelColor = bitmap.GetPixel(x, y);

                            // Calculate the luminance (grayscale value)
                            int luminance = (int)(0.3 * pixelColor.R + 0.59 * pixelColor.G + 0.11 * pixelColor.B);

                            // Determine alpha: 0 (white) to 255 (black)
                            int alpha = 255 - luminance; // Invert luminance for transparency (white = 0 alpha, black = 255 alpha)

                            // Ensure alpha stays within 0-255 range
                            alpha = Math.Clamp(alpha, 0, 255);

                            // Set the pixel to grayscale with adjusted alpha
                            Color transparentGrayColor = Color.FromArgb(alpha, luminance, luminance, luminance);
                            bitmap.SetPixel(x, y, transparentGrayColor);
                        }
                    }

                    // Convert the processed Bitmap back to a byte array
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        bitmap.Save(outputStream, ImageFormat.Png); // Save as PNG to retain transparency
                        return outputStream.ToArray();
                    }
                }
            }
        }

        public static byte[] CropToAspectRatio(this byte[] originalImageBytes, EAspectRatio aspectRatio)
        {
            using (MemoryStream inputStream = new MemoryStream(originalImageBytes))
            {
                using (Bitmap image = new Bitmap(inputStream))
                {
                    // Determine the desired aspect ratio
                    double targetAspectRatio = aspectRatio switch
                    {
                        EAspectRatio.SixteenByNine => 16.0 / 9.0,
                        EAspectRatio.Square => 1.0, // 1:1 aspect ratio
                        EAspectRatio.Ultrawide => 21.0 / 9.0,
                        _ => throw new ArgumentOutOfRangeException(nameof(aspectRatio), "Invalid aspect ratio")
                    };

                    // Get the current image dimensions
                    int originalWidth = image.Width;
                    int originalHeight = image.Height;

                    // Calculate the new dimensions for cropping
                    int cropWidth, cropHeight;

                    if ((double)originalWidth / originalHeight > targetAspectRatio)
                    {
                        // Image is wider than the target aspect ratio; crop horizontally
                        cropHeight = originalHeight;
                        cropWidth = (int)(cropHeight * targetAspectRatio);
                    }
                    else
                    {
                        // Image is taller than the target aspect ratio; crop vertically
                        cropWidth = originalWidth;
                        cropHeight = (int)(cropWidth / targetAspectRatio);
                    }

                    // Calculate the crop area centered on the image
                    int cropX = (originalWidth - cropWidth) / 2;
                    int cropY = (originalHeight - cropHeight) / 2;

                    // Perform the crop
                    Rectangle cropArea = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                    using (Bitmap croppedImage = image.Clone(cropArea, image.PixelFormat))
                    {
                        // Convert the cropped image back to a byte array
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            croppedImage.Save(outputStream, ImageFormat.Png); // Save as PNG or another desired format
                            return outputStream.ToArray();
                        }
                    }
                }
            }
        }
    }
}
