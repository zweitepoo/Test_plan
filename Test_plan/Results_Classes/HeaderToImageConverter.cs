using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Test_plan
{
    /// <summary>
    /// Convert full path to an image of a file, folder, drive
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        
        
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {//Get full path

            var path = (string)value;
            if (path == null) 
                return null;
            var name = FileFolderInfo.GetFileFolderName(path);
            var imagePath = @"Graphics/Drive_64.png";

            if (string.IsNullOrEmpty(name))
                imagePath = @"Graphics/Drive_64.png";
            else if( new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory) )
                imagePath = @"Graphics/Folder_64.png";
            else 
                imagePath = @"Graphics/File_64.png";
            var image = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}"));
            return image;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
