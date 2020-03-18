using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace KenCommonDLL
{
    public static class BitmapImageExtensions
    {
        /// <summary>
        /// The size limit of the image
        /// </summary>
        public const int ImageLimit = 40;

        /// <summary>
        /// Read the name of the function and you know what it does
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();

            return image;
        }

        /// <summary>
        /// Get a bitmapImage from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static BitmapImage TransferToBitmapImage(string filename)
        {
            BitmapImage bi = new BitmapImage();

            Image img = Bitmap.FromFile(filename);

            // Begin initialization.
            bi.BeginInit();

            bi.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);

            // Set properties.
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.CreateOptions = BitmapCreateOptions.DelayCreation;

            if (img.Width >= img.Height)
            {
                bi.DecodePixelWidth = ImageLimit;
                bi.DecodePixelHeight = Convert.ToInt32(img.Height / img.Width * ImageLimit);
            }
            else
            {
                bi.DecodePixelHeight = ImageLimit;
                bi.DecodePixelWidth = Convert.ToInt32(img.Width / img.Height * ImageLimit);
            }

            switch (readPictureDegree(filename))
            {
                case 90:
                    bi.Rotation = Rotation.Rotate90;
                    break;
                case 180:
                    bi.Rotation = Rotation.Rotate180;
                    break;
                case 270:
                    bi.Rotation = Rotation.Rotate270;
                    break;
                default:
                    break;
            }

            // End initialization.
            bi.EndInit();

            return bi;
        }

        private const string _orientationQuery = "System.Photo.Orientation";
        public static int readPictureDegree(String fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                BitmapFrame bitmapFrame = BitmapFrame.Create(fileStream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                    BitmapMetadata bitmapMetadata = bitmapFrame.Metadata as BitmapMetadata;

                if ((bitmapMetadata != null) && (bitmapMetadata.ContainsQuery(_orientationQuery)))
                {
                    object o = bitmapMetadata.GetQuery(_orientationQuery);

                    if (o != null)
                    {
                        //refer to http://www.impulseadventure.com/photo/exif-orientation.html for details on orientation values
                        switch ((ushort) o)
                        {
                            case 6:
                                return 90;
                            case 3:
                                return 180;
                            case 8:
                                return 270;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
