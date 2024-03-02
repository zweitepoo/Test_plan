using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test_plan
{
    public abstract class QTestTreeObject
    {
        
        public  Image ObjectIcon { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pid { get; set; }


        public abstract Image SetIcon();
    }
}
