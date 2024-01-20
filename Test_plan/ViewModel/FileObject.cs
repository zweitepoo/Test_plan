using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Test_plan;
using Test_plan.ViewModel;

namespace Test_plan.ViewModel
{
    internal class FileObject : ExplorerObject
    {
        public FileObject(string filePath)
        {
            this.ObjectName = FileFolderInfo.GetFileFolderName(filePath);
            this.ObjectPath = filePath;
            this.ObjectIcon = SetIcon();
            this.NameFontWeight = ReturnFontWeight();
    }
        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetFileImage();
            return image;
        }
        public virtual FontWeight ReturnFontWeight()
        {
            return FontWeights.Normal;
        }
    }
}
