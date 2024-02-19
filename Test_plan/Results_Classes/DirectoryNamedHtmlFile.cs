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
    internal class DirectoryNamedHtmlFile:ExplorerObject
    {
        public DirectoryNamedHtmlFile(string filePath)
        {
            this.ObjectName = GenerateFileName(filePath);
            this.ObjectPath = filePath;
            this.ObjectIcon = SetIcon();
            this.NameFontWeight = ReturnFontWeight();
        }
        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetHtmlFileImage();
            return image;
        }
        public virtual FontWeight ReturnFontWeight()
        {
            return FontWeights.Normal;
        }

        private string GenerateFileName(string filePath)
        {
            var dirInfo = new DirectoryInfo(filePath);
            var parentDir = dirInfo.Parent;
            if (parentDir != null)
            {
                var dirName = FileFolderInfo.GetFileFolderName(parentDir.Name);
                var pathName = FileFolderInfo.GetFileFolderName(filePath);
                return String.Format("{0}_{1}",dirName, pathName);
            }
            else
            {
                return FileFolderInfo.GetFileFolderName(filePath);
            }

        }
    }
}
