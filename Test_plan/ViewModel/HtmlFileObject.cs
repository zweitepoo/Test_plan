using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test_plan.ViewModel
{
    internal class HtmlFileObject : FileObject
    {
        public HtmlFileObject(string filePath) : base(filePath) 
        {
        }

        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetHtmlFileImage();
            return image;
        }

        public override FontWeight ReturnFontWeight()
        {
            return FontWeights.Bold;
        }
    }
}
