using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Test_plan
{
    [Serializable]
    internal class Controller
    {
        
       public ControllerNum ControllerShort { get; private set; }
        
       public ControllerSymbol ControllerType { get; private set; }
       
       public List<TBSymbol> PresentOnTestbed { get; private set; }

       public Controller(ControllerNum controllerShort, ControllerSymbol controllerType, List<TBSymbol> presentOnTestbed)
        {
            ControllerShort = controllerShort;
            ControllerType = controllerType;
            PresentOnTestbed = presentOnTestbed;
        }

        public override string ToString()
        {

            return ControllerShort.ToString() + '_'+ ControllerType.ToString();
        }


    }
}
