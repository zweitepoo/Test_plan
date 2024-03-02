using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Test_plan.QTest_Classes
{
    internal class QTestSystemBitMapImage
    {
        public static BitmapImage GetReleaseImage()
        {
            var imagePath = @"Graphics/Drive_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
        public static BitmapImage GetTestCaseImage()
        {
            var imagePath = @"Graphics/File_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
        public static BitmapImage GetCycleImage()
        {
            var imagePath = @"Graphics/Folder_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }

        public static BitmapImage GetTestSuiteImage()
        {
            var imagePath = @"Graphics/Html_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
        public static BitmapImage GetTestRunImage()
        {
            var imagePath = @"Graphics/Html_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;
        }
    }
}
