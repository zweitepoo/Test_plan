using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Test_plan
{
    public class FilePathToFileName : IValueConverter
    {
        public static FilePathToFileName Instance= new FilePathToFileName();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FileFolderName.GetFileFolderName((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
