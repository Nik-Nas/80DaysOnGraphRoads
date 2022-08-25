using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Drawing
{
    public static class DrawingUtils
    {
        private static float sin;
        private static float cos;
        private static Bitmap rotatedBitmap;
        /// <summary>
        /// Rotates image by given angle and resizing bitmap to avoid clipping
        /// </summary>
        /// <param name="image">image to rotate</param>
        /// <param name="angle">angle</param>
        /// <returns></returns>
        public static Bitmap RotateImage(Bitmap image, float angle)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            sin = (float) Math.Abs(Math.Sin(angle * Math.PI / 180D));
            cos = (float) Math.Abs(Math.Cos(angle * Math.PI / 180D));
            //create a new empty bitmap to hold rotated image
            rotatedBitmap = new Bitmap((int) Math.Round(image.Width * cos + image.Height * sin), 
                (int) Math.Round(image.Width * sin + image.Height * cos));
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBitmap);
            
            //Put the rotation point in the center of the image
            g.TranslateTransform(rotatedBitmap.Width >> 1, rotatedBitmap.Height >> 1);

            //rotate the image
            g.RotateTransform(angle);

            //draw passed in image onto graphics object
            g.DrawImage(image, -image.Width >> 1, -image.Height >> 1);
            g.Dispose();
            return rotatedBitmap;
        }
        
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}