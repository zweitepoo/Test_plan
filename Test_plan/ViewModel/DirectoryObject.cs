using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Test_plan;

namespace Test_plan.ViewModel
{
    internal class DirectoryObject : ExplorerObject
    {
        public DirectoryObject(string directoryPath)
        {
            this.ObjectName = FileFolderName.GetFileFolderName(directoryPath); 
            this.ObjectPath = directoryPath;
            this.ObjectIcon = SetIcon();
        }
        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetFolderImage();
            return image;
        }


    }
}
