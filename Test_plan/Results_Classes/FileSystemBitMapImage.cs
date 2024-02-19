using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Test_plan.ViewModel
{
    internal class FileSystemBitMapImage
    {
        public static BitmapImage GetDriveImage()
        {
            var imagePath = @"Graphics/Drive_64.png"; 
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
        public static BitmapImage GetFileImage()
        {
            var imagePath = @"Graphics/File_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
        public static BitmapImage GetFolderImage()
        {
            var imagePath = @"Graphics/Folder_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }

        public static BitmapImage GetHtmlFileImage()
        {
            var imagePath = @"Graphics/Html_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
    }
}
