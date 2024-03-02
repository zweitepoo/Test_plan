using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test_plan
{
    internal class QTestSuiteTreeObject : QTestTreeObject
    {
        public QTestSuiteTreeObject(string name, string pid, int id)
        {
            this.Name = name;
            this.Pid = pid;
            this.Id = id;
            this.ObjectIcon = SetIcon();
        }

        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetTestSuiteImage();
            return image;
        }
    }


}
