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
using Test_plan.Classes;

namespace Test_plan
{
    [Serializable]
    public class TestCaseManager
    {
        private TestCaseSearch testCaseSearch;
        public ObservableCollection<TestCase> ActiveTCList { get; private set; }
       
        private List<TestCase> OptixTCList;
       
        private List<TestCase> ViewETCList;  
        public ObservableCollection<TestCase> FilteredTCList {  get; private set; } 

       // public string SearchText = "";
        public ProjectSymbol ActiveProject { get;private set; }
        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
        private string filePath;

        

        public TestCaseManager(ProjectSymbol activeProject)
        {
            ActiveProject = activeProject;
            ActiveTCList = new ObservableCollection<TestCase>();
            ViewETCList = new List<TestCase>();
            OptixTCList = new List<TestCase>();
            FilteredTCList = new ObservableCollection<TestCase>();
            filePath = folder + @"\TC_List.dat";
            testCaseSearch = new TestCaseSearch();
           // ActiveTCList.CollectionChanged += ActiveTCList_CollectionChanged;
            
        }

        //private void ActiveTCList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{           
        //    testCaseSearch.SetNewSearchList(ActiveTCList);
        //    FilterList(String.Empty);
        //}

        public void FilterList(string searchText) 
        {
            FilteredTCList.Clear();
            foreach (TestCase tc in testCaseSearch.FilterList(searchText, ActiveTCList))
            FilteredTCList.Add(tc);              
        }


        //Check if possible and add new TC
        public  void AddTestCase( int testCaseNumber, string testRunName, int alarmInstance)
        {

            if (ActiveTCList == null  )
            ActiveTCList.Add(new TestCase(testCaseNumber, testRunName, alarmInstance));
            else
            {
                if (!TC_AlreadyExists(testCaseNumber, testRunName)) 
                { 
                    ActiveTCList.Add(new TestCase(testCaseNumber, testRunName, alarmInstance));
                    testCaseSearch.SetNewSearchList(ActiveTCList);
                    FilterList(String.Empty);
                }
                else
                    MessageTC_Exists( testCaseNumber,  testRunName);

            } 
        }

        public void UpdateTestCase(int testCaseNumber, string testRunName, int alarmInstance)
        {
            
        }
        //Reloads TC list for Oprix or ViewE
        public void ReloadTestCaseList(ProjectSymbol activeProject)
        {
            
         switch (activeProject)
                {
                    case ProjectSymbol.Optix:
                        ViewETCList.Clear();
                        ViewETCList.AddRange(ActiveTCList);
                        ActiveTCList.Clear();
                        foreach (TestCase tc in OptixTCList)
                            ActiveTCList.Add(tc);
                        testCaseSearch.SetNewSearchList(ActiveTCList);
                        FilterList(String.Empty);

                    break;

                    case ProjectSymbol.ViewE:
                        OptixTCList.Clear();
                        OptixTCList.AddRange(ActiveTCList);
                        ActiveTCList.Clear();
                        foreach (TestCase tc in ViewETCList)
                            ActiveTCList.Add(tc);
                        testCaseSearch.SetNewSearchList(ActiveTCList);
                        FilterList(String.Empty);
                    break;
         }
            
         this.ActiveProject = activeProject;
           
        }

        //Checks if TC already exists in Active TC List 
        public  bool TC_AlreadyExists(int testCaseNumber, string testRunName)
        {
            bool result = false;
            foreach(TestCase tc in ActiveTCList)
            {
                if (tc.TestCaseNumber == testCaseNumber || tc.TestName == testRunName)
                {
                    result = true;                   
                }                              
                
            }
            return result;
           
        }

        //Finds which value is duplicated in TC List and display relevant message
        public  void MessageTC_Exists(int testCaseNumber, string testRunName)
        {
            foreach (TestCase tc in ActiveTCList)
            {
                if (tc.TestCaseNumber == testCaseNumber )
                {
                   MessageBox.Show("Testcase number " + tc.TestCaseNumber.ToString() + " already exists");
                }
                else if (tc.TestName == testRunName)
                {
                   MessageBox.Show("Test with this name \"" + tc.TestName + "\"  already exists ");
                }
            }
        }

        //Removes selected TC from active list
        public void RemoveTestCase(TestCase testCaseToRemove)
        {
            ActiveTCList.Remove(testCaseToRemove);
            testCaseSearch.SetNewSearchList(ActiveTCList);
            FilterList(String.Empty);
        }

        //Serializing Testcase 
        public  void SerializeTestCasesList()
        {  
                         

            if (ActiveProject== ProjectSymbol.Optix)
            {
                OptixTCList.Clear();
                OptixTCList.AddRange(ActiveTCList);
            }
            else if (ActiveProject == ProjectSymbol.ViewE)
            {
                ViewETCList.Clear();
                ViewETCList.AddRange(ActiveTCList);
            }
            if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

            using (Stream output  = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, this );
                MessageBox.Show("file saved: " + filePath);
            }        
        }


        //Deserilize - load Optix TC list and ViewE TC List
        public  void DeserializeTestCaseList()
        {            
            TestCaseManager tempTestCaseManager;            
            if (!File.Exists(filePath))
                return;

            try
            {
               
                using (Stream input = File.OpenRead(filePath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    tempTestCaseManager = (TestCaseManager)formatter.Deserialize(input);
                    OptixTCList.AddRange(tempTestCaseManager.OptixTCList);
                    ViewETCList.AddRange(tempTestCaseManager.ViewETCList);

                }
                ActiveTCList.Clear();
                if (ActiveProject == ProjectSymbol.Optix)
                    foreach (var item in OptixTCList)
                        ActiveTCList.Add(item);                
                else if (ActiveProject == ProjectSymbol.ViewE)
                    foreach (var item in ViewETCList)
                        ActiveTCList.Add(item);
                testCaseSearch.SetNewSearchList(ActiveTCList);
                FilterList(String.Empty);
            }
            catch (SerializationException)
            {
                MessageBox.Show("Invalid Test case list file: " + filePath);
            }
            
        }
        //Export test cases list. Only saved TC list will be exported
        public  void ExportTCList()
        {

            if (ActiveProject == ProjectSymbol.Optix)
            {
                OptixTCList.Clear();
                OptixTCList.AddRange(ActiveTCList);
            }
            else if (ActiveProject == ProjectSymbol.ViewE)
            {
                ViewETCList.Clear();
                ViewETCList.AddRange(ActiveTCList);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            saveFileDialog.Filter = "Test case list data file (*.dat)|*.dat";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream output = saveFileDialog.OpenFile())
                {                    
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(output, this);
                }
                MessageBox.Show("Test case list data file export done: " + saveFileDialog.ToString());
            } 
        }

        //Imports and replaces default TC list, 
        public void ImportTCList()
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "Test case list data file (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() == true)
            {
                using (Stream input = openFileDialog.OpenFile())
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    try
                    {
                        TestCaseManager tempTestCaseManager = (TestCaseManager)formatter.Deserialize(input);
                        ActiveTCList.Clear();
                        
                        if (this.ActiveProject == ProjectSymbol.Optix)
                        {
                            foreach (TestCase testCase in tempTestCaseManager.OptixTCList)
                                ActiveTCList.Add(testCase);

                        }
                        else if (this.ActiveProject == ProjectSymbol.ViewE)
                        {
                            foreach (TestCase testCase in tempTestCaseManager.ViewETCList)
                                ActiveTCList.Add(testCase);
                        }
                        testCaseSearch.SetNewSearchList(ActiveTCList);
                        FilterList(String.Empty);

                        OptixTCList.Clear();                        
                        ViewETCList.Clear();

                        OptixTCList.AddRange(tempTestCaseManager.OptixTCList);
                        ViewETCList.AddRange(tempTestCaseManager.ViewETCList);
                        
                        SerializeTestCasesList();

                    }
                    catch (SerializationException)
                    {
                        MessageBox.Show("Invalid Testplan file: " + openFileDialog.ToString());
                        return;
                    } 
                }
            }
        }

    }
}
