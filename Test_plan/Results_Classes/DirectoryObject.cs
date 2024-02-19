using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Test_plan;

namespace Test_plan.ViewModel
{
    public class DirectoryObject : ExplorerObject
    {
        public DirectoryObject(string directoryPath)
        {
            this.ObjectName = FileFolderInfo.GetFileFolderName(directoryPath); 
            this.ObjectPath = directoryPath;
            this.ObjectIcon = SetIcon();
            this.NameFontWeight = FontWeights.Normal;
        }
        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetFolderImage();
            return image;
        }


    }
}
