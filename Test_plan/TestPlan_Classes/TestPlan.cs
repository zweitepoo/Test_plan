using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;
using Test_plan.Classes;




namespace Test_plan
{
    [Serializable]
    public class TestPlan : INotifyPropertyChanged
    {
        
        public string saveTestPlanName { get; set; }
        public TestbedConfiguration TestbedConfig { get; set; }

        public ObservableCollection<TestRun> TestRunSequence { get; set; }
        public string TestCaseNumberText { get; set; }
        public string TestRunNumberText { get; set; }
        public string TestRunName { get; set; }
        public string AlarmInstanceText { get; set; }
        public string FlashType { get; set; }
        public string ACD { get; set; }
        public string VPD { get; set; }
        public string BuildNumberText { get; set; }
        public string ControllerBuildText { get; set; }
        public string FlashWithPreviousBuild { get;  set; }
        public string IgnoreFlashFault { get;  set; }
        public string DatabaseSQL { get; set; }
        public string AddaBypass { get; set; }
        public ProjectSymbol ActiveProject { get; set; }

        public TestCaseManager testCaseManger;
        public TestPlan2JSON testPlan2JSON;
        public string TestPlanJSON { get; set; }
        private SaveFileDialog fileJSON = null;

        public string TCSearchText { get; set; }

        public ObservableCollection<TestCase> ActiveTCList { get { return testCaseManger.FilteredTCList; } set { } }


        public ObservableCollection<Controller> AvailableControllersList { get; private set; }

        public TBSymbol TestbedSelected { get { return TestbedConfig.TestbedSelected; } set { } }

        public Controller CLX_1 { get; set; }
        public Controller CLX_2 { get; set; }
        public Controller CLX_3 { get; set; }
        public Controller CLX_4 { get; set; }
       

        public Controller[] ControllersSet { get; private set; }
        public ObservableCollection<TBSymbol> TestbedList { get { return testbedList; } private set { } }


        private ObservableCollection<TBSymbol> testbedList = new ObservableCollection<TBSymbol>()
        { TBSymbol.VES01,
          TBSymbol.VES02,
          TBSymbol.VES11,
          TBSymbol.VES12,
          TBSymbol.VES21,
          TBSymbol.VES22,
          TBSymbol.VES31,
          TBSymbol.VES32,
          TBSymbol.VES41
        };

        public bool[] PythonScripts { get { return pythonScripts; } private set { } }
        private bool[] pythonScripts;       
        private UserDataExporter userDataExporter;
        private UserDataImporter userDataImporter;
        public PythonRun pythonRun;
        public string PythonExeFilePath { get { return pythonRun.PythonExeFilePath; } private set { } }
        public string PythonScriptsFolderPath { get { return pythonRun.PythonScriptsFolderPath; } private set { } }
        public string PythonScriptFilePath { get { return pythonRun.PythonScriptFilePath; } private set { } }

        //Constructor
        public TestPlan()
        {

            TestbedConfig = new TestbedConfiguration(TBSymbol.VES01);
            AlarmInstanceText = "0";
            FlashType = "ratools";
            ACD = "standard";
            VPD = "standard";
            ControllerBuildText = "35";
            BuildNumberText = "9.1.00000.01811";
            DatabaseSQL = "ViewE_SQL";
            TestRunNumberText = "0";
            AvailableControllersList = TestbedConfig.AvailableControllersList;
           
            TestRunSequence = new ObservableCollection<TestRun>();
            testCaseManger = new TestCaseManager(ActiveProject);           
            userDataExporter = new UserDataExporter();
            userDataImporter = new UserDataImporter();
            ActiveTCList = new ObservableCollection<TestCase>();
            pythonScripts = new bool[9] { false, false, false, false, false, false, false, false, false };
            pythonRun = new PythonRun();


        }

        //Event handler for data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        //Clear testrun sequence
        public void ClearTestPlan()
        {
            TestRunSequence.Clear();
        }


        //Adding new testrun to testrun sequence
        public void AddTestRun(int selectedIndex)
        {


            if (TestRunDataIsValid() && !(TestAlreadyExists()))
            {
                if (selectedIndex != -1)
                {                    
                    TestRunSequence.Insert(selectedIndex + 1, new TestRun(int.Parse(TestCaseNumberText), int.Parse(TestRunNumberText), int.Parse(AlarmInstanceText), TestRunName, FlashType, ACD, VPD,
                                            CLX_1, CLX_2, CLX_3, CLX_4, TestbedSelected, PythonScripts));
                }
                else
                {
                    TestRunSequence.Add(new TestRun(int.Parse(TestCaseNumberText), int.Parse(TestRunNumberText), int.Parse(AlarmInstanceText), TestRunName, FlashType, ACD, VPD,
                                        CLX_1, CLX_2, CLX_3, CLX_4, TestbedSelected, PythonScripts));
                }
                UpdateTestbedTBxx();
                OnPropertyChanged("TestRunSequence");
            }

        }

        //Remove selected testrun from the testrun sequence
        public void RemoveTestRun(int index)
        {
            TestRunSequence.RemoveAt(index);
        }

        //Shows values of selected testrun in testrun sequence
        public void ShowTestRunValues(int elementIndex)
        {
            TestRunName = TestRunSequence[elementIndex].TestName;
            TestCaseNumberText = TestRunSequence[elementIndex].TestCaseNumber.ToString();
            TestRunNumberText = TestRunSequence[elementIndex].TestRunNumber.ToString();
            AlarmInstanceText = TestRunSequence[elementIndex].AlarmInstance.ToString();
            FlashType = TestRunSequence[elementIndex].FlashType.ToString();
            ACD = TestRunSequence[elementIndex].ACD.ToString();
            VPD = TestRunSequence[elementIndex].VPD.ToString();
            for (int i = 0; i < TestRunSequence[elementIndex].PythonScripts.Length; i++)
                pythonScripts[i] = TestRunSequence[elementIndex].PythonScripts[i];
            CLX_1 = TestRunSequence[elementIndex].Slot_CLX1;
            CLX_2 = TestRunSequence[elementIndex].Slot_CLX2;
            CLX_3 = TestRunSequence[elementIndex].Slot_CLX3;
            CLX_4 = TestRunSequence[elementIndex].Slot_CLX4;
            OnPropertyChanged("PythonScripts");
            OnPropertyChanged("TestCaseNumberText");
            OnPropertyChanged("TestRunNumberText");
            OnPropertyChanged("AlarmInstanceText");
            OnPropertyChanged("TestRunName");
            OnPropertyChanged("FlashType");
            OnPropertyChanged("ACD");
            OnPropertyChanged("VPD");
            OnPropertyChanged("CLX_1");
            OnPropertyChanged("CLX_2");
            OnPropertyChanged("CLX_3");
            OnPropertyChanged("CLX_4");

        }

        //Updates values of selcted testrun properties in testrun sequence
        public void UpdateTestRunValue(TestRun testRun)
        {
            if (TestRunDataIsValid())
            {
                testRun.UpdateTestRun(int.Parse(TestCaseNumberText), TestRunName, FlashType, ACD, VPD, int.Parse(AlarmInstanceText), int.Parse(TestRunNumberText),
                                        CLX_1, CLX_2, CLX_3, CLX_4, TestbedSelected, PythonScripts);
                UpdateTestbedTBxx();
                OnPropertyChanged("TestRunSequence");
            }
        }

        //Move testrun up or down in testrun sequnce depending on direction set
        public void MoveTestInQueue(int selectedIndex, int direction)
        {
            TestRunSequence.Move(selectedIndex, (selectedIndex + direction));
            OnPropertyChanged("TestRunSequence");
        }

        //Update configuration of testbed -available controllers list, testbed 
        public void UpdateTestbedConfig(TBSymbol newTB, TBSymbol oldTB)
        {
            if (oldTB != newTB)
            {
                TestbedConfig.UpdateTestbedConfig(newTB);
                OnPropertyChanged("TestbedSelected");
                AvailableControllersList = TestbedConfig.AvailableControllersList;
                OnPropertyChanged("AvailableControllersList");
            }
        }

        //Set controllers in testrun
       
        public void SetSlotControllerCLX1(Controller newController)
        {
            CLX_1 = newController;
            OnPropertyChanged("CLX_1");
        }
        public void SetSlotControllerCLX2(Controller newController)
        {
            CLX_2 = newController;
            OnPropertyChanged("CLX_2");
        }
        public void SetSlotControllerCLX3(Controller newController)
        {
            CLX_3 = newController;
            OnPropertyChanged("CLX_3");
        }
        public void SetSlotControllerCLX4(Controller newController)
        {
            CLX_4 = newController;
            OnPropertyChanged("CLX_4");
        }
        //Testrun data validation
        public bool TestRunDataIsValid()
        {

            if (!TestCaseDataIsValid())
            {
                return false;
            }
            if (String.IsNullOrEmpty(VPD))
            {
                MessageBox.Show("VPD is null, enter valid VPD");
                return false;
            }
            if (String.IsNullOrEmpty(ACD))
            {
                MessageBox.Show("ACD is null, enter valid ACD");
                return false;
            }
            if ((String.IsNullOrEmpty(FlashType)))
            {
                MessageBox.Show("flash type is null, enetr valid flash type");
                return false;
            }

            //
            //try
            //{
            //    if ((String.IsNullOrEmpty(TestRunNumberText)) || int.Parse(TestRunNumberText) == 0)
            //    {
            //        MessageBox.Show("Wrong Testrun number: " + TestRunNumberText);
            //        return false;
            //    }

            //}
            //catch (FormatException)
            //{
            //    MessageBox.Show("Wrong Testrun number Format: " + TestRunNumberText);
            //    return false;
            //}

            if (CLX_1 == null)
            {
                MessageBox.Show("No CLX[1] set");
                return false;
            }
            if (CLX_2 == null)
            {
                MessageBox.Show("No CLX[2] set");
                return false;
            }
            if (CLX_3 == null)
            {
                MessageBox.Show("No CLX[3] set");
                return false;
            }
            if (CLX_4 == null)
            {
                MessageBox.Show("No CLX[4] set");
                return false;
            }


            if (Array.IndexOf<bool>(pythonScripts, true) == -1)
            {
                MessageBox.Show("At least one python script must be selected");
                return false;
            }


            return true;
        }

        //Check if testrun or test name is already in queue and displays relevant message
        public bool TestAlreadyExists()
        {
            foreach (TestRun tr in TestRunSequence)
            {
                if ((tr.TestRunNumber == int.Parse(TestRunNumberText)) && TestRunNumberText!="0")
                {
                    MessageBox.Show("Testrun number already exists in test sequence: " + TestRunNumberText);
                    return true;
                }
                else if (tr.TestName == TestRunName)
                {
                    MessageBox.Show("Test: \" " + TestRunName + "\" already exists in test sequence");
                    return true;
                }

            }
            return false;

        }

        //Updates Testbed in all testruns in testrun sequence
        public void UpdateTestbedTBxx()
        {
            foreach (var element in TestRunSequence)
            {
                element.UpdateTestbedTBxx(TestbedSelected);
            }

        }


        //Check if selcted controller is available on currently selected tesbed
        public bool CheckControllerIsAvailable(Controller controller)
        {
            return TestbedConfig.CheckControllerIsAvailable(controller);

        }


        //Update Active project
        public void UpdateActiveProject(ProjectSymbol newProject)
        {

            ActiveProject = newProject;

            testCaseManger.ReloadTestCaseList(ActiveProject);
        }

        //Update Flash with previous build
        public void UpdateFlashWithPrevBuild(bool setTrue)
        {
            if (setTrue)
                FlashWithPreviousBuild = "1";
            else
                FlashWithPreviousBuild = "0";
            OnPropertyChanged("FlashWithPreviousBuild");
        }
        //Update Ignore flash fault 
        public void UpdateIgnoreFlashFault(bool setTrue)
        {
            if (setTrue)
                IgnoreFlashFault = "1";
            else
                IgnoreFlashFault = "0";
            OnPropertyChanged("IgnoreFlashFault");

        }

        public void UpdateAddaBypass(bool setTrue)
        {
            if (setTrue)
                AddaBypass = "1";
            else
                AddaBypass = "0";
            OnPropertyChanged("AddaBypass");

        }
        //Serialize Test case lists
        public void SerializeTestCasesList()
        {
            testCaseManger.GenerateTestCaseListCsvFile();
            OnPropertyChanged("TCListFilePath");
        }

        //Open TestCase list
        public void LoadTestCasesList()
        {
            testCaseManger.LoadTestCaseListFromCsvFile();

        }

        //Import TestCase list
        public void ImportTCList()
        {
            testCaseManger.ImportTCList();

        }
        //Export TestCase list
        public void ExportTCList()
        {
            testCaseManger.ExportTCListToCSV();

        }

        //FIlter Test Case List
        public void FilterList(string searchText)
        {
            testCaseManger.FilterList(searchText);
        }


        public void ShowTestCaseValues(TestCase loadTestCase)
        {
            TestCaseNumberText = loadTestCase.TestCaseNumber.ToString();
            TestRunName = loadTestCase.TestName;
            AlarmInstanceText = loadTestCase.AlarmInstance.ToString();
            OnPropertyChanged("TestCaseNumberText");
            OnPropertyChanged("TestRunName");
            OnPropertyChanged("AlarmInstanceText");

        }

        //Check if data is valid
        public bool TestCaseDataIsValid()
        {
            
            try
            {
                if ((String.IsNullOrEmpty(TestCaseNumberText)) || int.Parse(TestCaseNumberText) == 0)
                {
                    MessageBox.Show("Wrong Test Case number: " + TestCaseNumberText);
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong Test Case number Format: " + TestCaseNumberText);
                return false;
            }
            try
            {
                if ((String.IsNullOrEmpty(AlarmInstanceText)) || int.Parse(AlarmInstanceText) < 0)
                {
                    MessageBox.Show("Enter valid alarm instance number: ");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong Alarm instance Format: " + AlarmInstanceText);
                return false;
            }

            if (String.IsNullOrEmpty(TestRunName))
            {
                MessageBox.Show("Test name is empty");
                return false;
            }

            return true;

        }

        //Add new Test Case to the list 

        public void AddTestCase()
        {      if(TestCaseDataIsValid())      
                testCaseManger.AddTestCase(int.Parse(TestCaseNumberText), TestRunName, int.Parse(AlarmInstanceText));            
        }

        //removes TC from the active list
        public void RemoveTestCase(object item)
        {
            TestCase testCaseToToRemove = item as TestCase;
            
            testCaseManger.RemoveTestCase(testCaseToToRemove);
        }

        //Save actual test plan
        public  void SerializeTestPlan()
        {
            TestRunSeqCSV.ExportTestRunSeqToCSV(TestRunSequence);

        }

        //Load a testplan to  TestplanSequnce

        public  void DeserializeTestPlan()
        {
            TestRunSequence.Clear();

            try
            {
                var testPlanList = TestRunSeqCSV.ImportTestRunSeqFromCSV();
                foreach (TestRun testRun in testPlanList)
                    TestRunSequence.Add(testRun);
            }
            catch (ImportCSVException ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        //Save user's paramterers from file
        public void SaveUserData()
        {
            userDataExporter.SaveUserData(this);
        }

        // Load user's parametrs from file
        public void LoadUserData()
        {
                userDataImporter.LoadUserData();
            if (userDataImporter.isDerserializeDone)
            { 
                BuildNumberText = userDataImporter.BuildNumberText;
                ControllerBuildText = userDataImporter.ControllerBuildText;
                FlashWithPreviousBuild = userDataImporter.FlashWithPreviousBuild;
                IgnoreFlashFault = userDataImporter.IgnoreFlashFault;
                AddaBypass = userDataImporter.AddaBypass;
                DatabaseSQL = userDataImporter.DatabaseSQL;                
                pythonRun.UpdatePaths(userDataImporter.PythonExeFilePath, userDataImporter.PythonScriptsFolderPath, userDataImporter.PythonScriptFilePath);
                ActiveProject = userDataImporter.ActiveProject;
                UpdateActiveProject(userDataImporter.ActiveProject);
                OnPropertyChanged("BuildNumberText");
                OnPropertyChanged("ControllerBuildText");
                OnPropertyChanged("FlashWithPreviousBuild");
                OnPropertyChanged("IgnoreFlashFault");
                OnPropertyChanged("DatabaseSQL");
            }
        }



        //Updates Scripts for Optix project 
        public void UpdateScripts(ProjectSymbol project)
        {
            if (project == ProjectSymbol.Optix)
            {
                pythonScripts[1] = false;
                pythonScripts[2] = false;
                pythonScripts[3] = false;
                pythonScripts[4] = false;
                pythonScripts[5] = false;
                pythonScripts[6] = false;
            }
          
            OnPropertyChanged("PythonScripts");
        }
        //Check if Test plan data is valid
        public bool TestPlanDataIsValid()
        {
            if (String.IsNullOrEmpty(BuildNumberText))
            {
                MessageBox.Show("Build number is null, enter valid Build number");
                return false;
            }
            if (String.IsNullOrEmpty(ControllerBuildText))
            {
                MessageBox.Show("Controller build version number is null, enter valid Controller build version");
                return false;
            }
            if (String.IsNullOrEmpty(DatabaseSQL))
            {
                MessageBox.Show("DatabaseSQL is null, enter valid DatabaseSQL");
                return false;
            }
            return true;
        }
        //Generate json text file, save user's data as well 
        public void GenerateJSON()
        {
            testPlan2JSON = new TestPlan2JSON(TestRunSequence, TestbedSelected, ActiveProject, BuildNumberText, ControllerBuildText,
                                              FlashWithPreviousBuild, IgnoreFlashFault, DatabaseSQL, AddaBypass);
            TestPlanJSON = testPlan2JSON.JSONtext;
            OnPropertyChanged("TestPlanJSON");
            SaveUserData();


        }
        //Saving Json to a file
        public void SaveJSON()
        /// <summary>
        /// Saving Jason to a file
        /// </summary>
        {
            if (fileJSON == null)
            {
                fileJSON = new SaveFileDialog();
                fileJSON.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                fileJSON.Filter = "Json Files (*.json)|*.json";
                fileJSON.FileName = "Test_plan";
                if (fileJSON.ShowDialog() == true)
                {
                    File.WriteAllText(fileJSON.FileName, TestPlanJSON);
                    MessageBox.Show("File saved");
                }           
            }
            else
            {
                File.WriteAllText(fileJSON.FileName, TestPlanJSON);
                MessageBox.Show("File saved");
            }              

        }
        //Select a file and save 
        public  void SaveAsJSON()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            saveFileDialog.Filter = "Json Files (*.json)|*.json";
            saveFileDialog.FileName = "Test_plan";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, TestPlanJSON);
                MessageBox.Show("File saved");
            }
            fileJSON = saveFileDialog;

        }
        //Open exising JSON file
        public void OpenJSON()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "Json Files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                TestPlanJSON = File.ReadAllText(openFileDialog.FileName);
                OnPropertyChanged("TestPlanJSON");
            }
        }

        //Set Python.exe file path 
        public void SetPythonExePath()
        {
            pythonRun.SetPythonExePath();
        }
        //Set Python scripts paths
        public void SetPythonScriptPath()
        {
            pythonRun.SetPythonScriptPath();
        }
        //Run python script redirect output text to input control
        public void RunPythonScript(TextBox textBox)
        {
            pythonRun.RunPythonScript(textBox);
        }
        //Close python run process
        public void ClosePythonRun()
        {
            pythonRun.ClosePythonRun();
        }


    }

}
