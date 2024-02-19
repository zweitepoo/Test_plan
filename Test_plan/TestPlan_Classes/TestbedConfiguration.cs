using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Test_plan
{
    [Serializable]
    public class TestbedConfiguration
    {
        public TBSymbol TestbedSelected { get; private set; }

        public ObservableCollection<Controller> AvailableControllersList;

        public TestbedConfiguration(TBSymbol testbedSelected)
        {
            this.TestbedSelected = testbedSelected;
            AvailableControllersList = GenerateAvailableControllers(TestbedSelected);
        }
        

        private ObservableCollection<Controller> GenerateAvailableControllers(TBSymbol TestbedSelected)
        {
            var allControllers = TestbedInfo.AllControllers;
            var testbedsAvailableControllers = TestbedInfo.AllTestbedsAvailableControllers;
            var controllersOnThisTestbed = testbedsAvailableControllers[TestbedSelected];
            var clx_list = new ObservableCollection<Controller>();
                
            
            foreach (Controller controller in allControllers)
            {                 
                if (controllersOnThisTestbed.Contains(controller.ControllerType))
                    clx_list.Add(controller);
            }
            return clx_list;
        }

        public void UpdateTestbedConfig(TBSymbol newTB)
        {
            TestbedSelected = newTB;
            AvailableControllersList.Clear();
            AvailableControllersList = GenerateAvailableControllers(TestbedSelected);
        }

        public bool CheckControllerIsAvailable(Controller controller)
        {
            if (AvailableControllersList.Contains(controller))
                return true;
            else
                return false;
        }

        public static Controller GenerateSingleControllerByNumber(ControllerCodeName controllerShort)
        {
            Controller result = null;
            foreach(var controller in TestbedInfo.AllControllers)
            {
                if (controller.ControllerCode == controllerShort)
                {
                    result = controller;
                }                                    
            }

            if (result != null)
                return result;
            else
                throw new NoDataForControllerException("No controller data in existing configuration: " + controllerShort.ToString());            
        }
        
    }
}
