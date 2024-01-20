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

namespace Test_plan
{
    public class DriveObject : ExplorerObject
      {
        public DriveObject(string drivePath)
        {
            this.ObjectName = drivePath;
            this.ObjectPath = drivePath;
            this.ObjectIcon = SetIcon();
            this.NameFontWeight = FontWeights.Normal;
        }
        public override Image SetIcon()
        {
           var image = new Image();
           image.Source = FileSystemBitMapImage.GetDriveImage();
           return image;
        }
        public static DriveObject[] GetDrives()
        {
            string[] logicalDrives = Directory.GetLogicalDrives();
            if (logicalDrives.Length == 0)
                return Array.Empty<DriveObject>();

            DriveObject[] drives = new DriveObject[logicalDrives.Length];
            for (int i = 0; i < logicalDrives.Length; i++)
            {
                drives[i] = new DriveObject(logicalDrives[i]);
            } 
            return drives;
        }
        

    }
}
