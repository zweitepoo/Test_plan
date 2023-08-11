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
    internal class TestbedConfiguration
    {
        public TBSymbol TestbedSelected { get; private set; }

        private NetType netTopology;
        public NetType NetTopology { get { return netTopology; } private set { } }

       

        public ObservableCollection<Controller> AvailableControllersList { get; private set; }

        

        //  collection describing all controllers 


        public ObservableCollection<Controller> AllControllersList { get { return allControllersList; } }


        private  ObservableCollection<Controller> allControllersList = new ObservableCollection<Controller>() 
        { 
            new Controller(ControllerNum.CLX1, ControllerSymbol.L7x, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX2, ControllerSymbol.L3y, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22}),
            new Controller(ControllerNum.CLX3, ControllerSymbol.L3z, new List<TBSymbol>(){TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES32}),
            new Controller(ControllerNum.CLX4, ControllerSymbol.L8z, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX5, ControllerSymbol.EPIC, new List<TBSymbol>(){TBSymbol.VES11,TBSymbol.VES12}),
            new Controller(ControllerNum.CLX6, ControllerSymbol.L8zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX7, ControllerSymbol.L3zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES31}),
            new Controller(ControllerNum.CLX8, ControllerSymbol.RedL7xL8z, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX9, ControllerSymbol.L7x, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX10, ControllerSymbol.L3y, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22}),
            new Controller(ControllerNum.CLX11, ControllerSymbol.L3z, new List<TBSymbol>(){TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES32}),
            new Controller(ControllerNum.CLX12, ControllerSymbol.L8z, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX13, ControllerSymbol.EPIC, new List<TBSymbol>(){TBSymbol.VES11,TBSymbol.VES12}),
            new Controller(ControllerNum.CLX14, ControllerSymbol.L8zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX15, ControllerSymbol.L3zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES31}),
            new Controller(ControllerNum.CLX16, ControllerSymbol.L7x, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX17, ControllerSymbol.L3y, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22}),
            new Controller(ControllerNum.CLX18, ControllerSymbol.L3z, new List<TBSymbol>(){TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES32}),
            new Controller(ControllerNum.CLX19, ControllerSymbol.L8z, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX20, ControllerSymbol.EPIC, new List<TBSymbol>(){TBSymbol.VES11,TBSymbol.VES12}),
            new Controller(ControllerNum.CLX21, ControllerSymbol.L8zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX22, ControllerSymbol.L3zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES31}),
            new Controller(ControllerNum.CLX23, ControllerSymbol.L7x, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX24, ControllerSymbol.L3y, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22}),
            new Controller(ControllerNum.CLX25, ControllerSymbol.L3z, new List<TBSymbol>(){TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES32}),
            new Controller(ControllerNum.CLX26, ControllerSymbol.L8z, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX27, ControllerSymbol.EPIC, new List<TBSymbol>(){TBSymbol.VES11,TBSymbol.VES12}),
            new Controller(ControllerNum.CLX28, ControllerSymbol.L8zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES11,TBSymbol.VES12,TBSymbol.VES21,TBSymbol.VES22,TBSymbol.VES31, TBSymbol.VES32}),
            new Controller(ControllerNum.CLX29, ControllerSymbol.L3zS, new List<TBSymbol>(){TBSymbol.VES01,TBSymbol.VES02,TBSymbol.VES31}),
        };
       

        public TestbedConfiguration(TBSymbol testbedSelected)
        {
            this.TestbedSelected = TestbedSelected;
            AvailableControllersList = CreateAvailableControllersList(TestbedSelected);
        }
        

        public ObservableCollection<Controller> CreateAvailableControllersList(TBSymbol thisTestbedSelected)
        {         

            ObservableCollection<Controller> clx_list = new ObservableCollection<Controller>();
            
            foreach (Controller controller in AllControllersList)
            {
                if (controller.PresentOnTestbed.Contains(thisTestbedSelected))
                    clx_list.Add(controller);
            }

            return clx_list;
        }

        public void UpdateTestbedConfig(TBSymbol newTB)
        {
            TestbedSelected = newTB;
            AvailableControllersList.Clear();
            AvailableControllersList = CreateAvailableControllersList(TestbedSelected);


        }

        public bool CheckControllerIsAvailable(Controller controller)
        {
            if (AvailableControllersList.Contains(controller))
                return true;
            else
                return false;

        }





    }
}
