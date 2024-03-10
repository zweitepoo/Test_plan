using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public class QTestPostObject
    {
        public int parentId { get; set; }
        public string parentType { get; set; }
        public string name { get; set; }

        public QTestPostObject(int parentId, string parentType, string name) 
        {
            this.parentId = parentId;
            this.parentType = parentType;                
            this.name = name;
        }
    }
}
