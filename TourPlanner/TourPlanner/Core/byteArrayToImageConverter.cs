using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourPlanner.Core
{
    public static class byteArrayToImageConverter
    {
        public static BitmapImage ConvertToImage(byte[] byteArray) 
        { 

            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }
    }
}
