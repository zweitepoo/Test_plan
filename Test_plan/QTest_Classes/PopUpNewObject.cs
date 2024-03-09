using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public class PopUpNewObject
    {
        public string Name { get; private set; }
        public string MessageText {  get;private set; }
       

       
        public void SetObjectName(string name)
        {
              Name = name;
        }
      
        public void ClearName()
        {
            Name = null; 
        }
        public void SetMessage(string message)
        {
            MessageText = message;    
        }

       
    }
}
