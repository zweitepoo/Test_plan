using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Test_plan
{
    [Serializable]
    public class Controller
    {
        
       public ControllerCodeName ControllerCode { get; private set; }
        
       public ControllerFamilyType ControllerType { get; private set; }
       
       

       public Controller(ControllerCodeName controllerCode, ControllerFamilyType controllerType)
        {
            ControllerCode = controllerCode;
            ControllerType = controllerType;
            
        }

        public override string ToString()
        {

            return ControllerCode.ToString() + '_'+ ControllerType.ToString();
        }


    }
}
