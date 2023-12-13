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
    public class TestRun : TestCase, INotifyPropertyChanged
    {

       
        public int TestRunNumber { get;private set; }

       
        public Controller[] ControllersSet { get; private set; }

        public string ControllersNames { get { return ControllersNamesGen(); }private set { } }

       
        public TBSymbol TestbedSymbol { get; set; }
       
        public bool[] PythonScripts { get ; private set; }
        
        public string FlashType { get; set; }
       
        public string ACD { get; set; }
       
        public string VPD { get; set; }

        public Controller Slot_CLX1 { get; private set; }
        public Controller Slot_CLX2 { get; private set; }
        public Controller Slot_CLX3 { get; private set; }
        public Controller Slot_CLX4 { get; private set; }










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
            Slot_CLX1 = ControllersSet[0];
            Slot_CLX2 = ControllersSet[1];
            Slot_CLX3 = ControllersSet[2];
            Slot_CLX4 = ControllersSet[3];



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
            string controllersNames = Slot_CLX1.ControllerType.ToString();
            if (Slot_CLX2.ControllerType != Slot_CLX1.ControllerType)
                controllersNames += $"_{Slot_CLX2.ControllerType}";
            if (Slot_CLX3.ControllerType != Slot_CLX2.ControllerType)
                controllersNames += $"_{Slot_CLX3.ControllerType}";
            if (Slot_CLX4.ControllerType != Slot_CLX3.ControllerType)
                controllersNames += $"_{Slot_CLX4.ControllerType}";
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
            Slot_CLX1 = ControllersSet[0];
            Slot_CLX2 = ControllersSet[1];
            Slot_CLX3 = ControllersSet[2];
            Slot_CLX4 = ControllersSet[3];

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
