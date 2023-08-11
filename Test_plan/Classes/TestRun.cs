using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;



namespace Test_plan
{
    [Serializable]
    internal class TestRun : TestCase, INotifyPropertyChanged
    {

       
        public int TestRunNumber { get;private set; }

       
        public Controller[] ControllersSet { get; private set; }

        public string ControllersNames { get { return ControllersNamesGen(); }private set { } }

       
        public TBSymbol TestbedSymbol { get; set; }
       
        public bool[] PythonScripts { get ; private set; }
        
        public string FlashType { get; set; }
       
        public string ACD { get; set; }
       
        public string VPD { get; set; }









        public TestRun(int testCaseNumber, int testRunNumber,int alarmInstance, string testName,string flashType,string acd,string vpd , Controller[] controllersSet,TBSymbol testbedSymbol, bool[] pythonScripts ):base(testCaseNumber, testName, alarmInstance)
        {
           TestRunNumber = testRunNumber;
            FlashType = flashType;
            ACD = acd;
            VPD = vpd;
            ControllersSet = new Controller[4] ;
            for (int i = 0; i < controllersSet.Length; i++) 
                ControllersSet[i] = controllersSet[i];
            PythonScripts = new bool[pythonScripts.Length];
            for (int i = 0; i < pythonScripts.Length; i++)
                PythonScripts[i] = pythonScripts[i];


           
            TestbedSymbol = testbedSymbol;
            OnPropertyChanged("TestbedSymbol");
            OnPropertyChanged("TestRunNumber");
            OnPropertyChanged("TestName");
            OnPropertyChanged("AlarmInstance");
            OnPropertyChanged("ControllersNames");
            OnPropertyChanged("PythonScripts");
            OnPropertyChanged("FlashType");
            OnPropertyChanged("ACD");
            OnPropertyChanged("VPD");


        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public override string ToString()
        {
            return TestbedSymbol +"_"+  TestName  + ControllersNamesGen();
        }

        //To do - Returns Controllers names for testname string
        public string ControllersNamesGen()
        {
            string controllersNames = string.Empty;

            for (int i = 0; i < ControllersSet.Length; i++)
            {
                if (i==0)
                controllersNames += "_" + ControllersSet[i].ControllerType.ToString();
                if (i>0)
                {
                    if (ControllersSet[i-1].ControllerType!= ControllersSet[i].ControllerType)
                        controllersNames += "_" + ControllersSet[i].ControllerType.ToString();
                }

            }
            return controllersNames;
        }

        public void UpdateTestRun(int testCaseNumber, string testName, string flashType, string acd, string vpd, int alarmInstance,  int testRunNumber, Controller[] controllersSet,  TBSymbol testbedSymbol, bool[] pythonScripts)
        {
          UpdateTestCase(testCaseNumber, testName, alarmInstance);
          TestRunNumber = testRunNumber; 
          FlashType = flashType;
            VPD = vpd;
            ACD = acd;
           for (int i = 0; i < controllersSet.Length; i++)
                ControllersSet[i] = controllersSet[i];          
          TestbedSymbol = testbedSymbol;
            for (int i = 0; i < pythonScripts.Length; i++)
                PythonScripts[i] = pythonScripts[i];

            OnPropertyChanged("TestbedSymbol");
            OnPropertyChanged("TestRunNumber");
            OnPropertyChanged("AlarmInstance");
            OnPropertyChanged("TestName");
            OnPropertyChanged("FlashType");
            OnPropertyChanged("ACD");
            OnPropertyChanged("VPD");
            OnPropertyChanged("ControllersNames");
            OnPropertyChanged("PythonScripts");
        }

        public void UpdateTestbedTBxx(TBSymbol testbedSymbol)
        {
            TestbedSymbol = testbedSymbol;
            OnPropertyChanged("TestbedSymbol");
            OnPropertyChanged("TestRunNumber");
            OnPropertyChanged("TestName");
            OnPropertyChanged("ControllersNames");
            OnPropertyChanged("PythonScripts");
        }


    }
}
