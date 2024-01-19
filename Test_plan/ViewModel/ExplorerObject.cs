using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test_plan
{ 
    public abstract class ExplorerObject:ItemsControl
    {
        public string ObjectName { get;  set; }
        public Image ObjectIcon { get;   set; }
        public string  ObjectPath { get;  set; }

        public abstract Image SetIcon();
        
    }
}
