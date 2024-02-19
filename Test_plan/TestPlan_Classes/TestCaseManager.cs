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
        

      
        public ProjectSymbol ActiveProject { get;private set; }
        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
        private readonly string filePathOptixList;
        private readonly string filePathViewEList;



        public TestCaseManager(ProjectSymbol activeProject)
        {
            ActiveProject = activeProject;
            ActiveTCList = new ObservableCollection<TestCase>();
            ViewETCList = new List<TestCase>();
            OptixTCList = new List<TestCase>();
            FilteredTCList = new ObservableCollection<TestCase>();
            filePathOptixList = folder + @"\TestCaseListOptix.csv";
            filePathViewEList = folder + @"\TestCaseListViewE.csv";
            testCaseSearch = new TestCaseSearch();
    }

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
        public  void GenerateTestCaseListCsvFile()
        {

            if (ActiveProject== ProjectSymbol.Optix)
            {
                OptixTCList.Clear();
                OptixTCList.AddRange(ActiveTCList);
                TestCasesListCSV.GenerateOptixTestCaseListCSV(OptixTCList);
            }
            else if (ActiveProject == ProjectSymbol.ViewE)
            {
                ViewETCList.Clear();
                ViewETCList.AddRange(ActiveTCList);
                TestCasesListCSV.GenerateViewETestCaseListCSV(ViewETCList);
            }
               
        }

        public void LoadTestCaseListFromCsvFile()
        {
            TestCaseManager tempTestCaseManager;            
            if (File.Exists(filePathOptixList))
            {
                try
                {
                    var temOptixTCList = TestCasesListCSV.LoadOptixTestCaseListCSV();
                    OptixTCList.AddRange(temOptixTCList);
                }

                catch (Exception ex)
                {
                    return;
                }
            }               
            
            if (File.Exists(filePathViewEList))
            {
                try
                {
                    var tempViewETCList = TestCasesListCSV.LoadViewETestCaseListCSV();
                    ViewETCList.AddRange(tempViewETCList);
                }
                catch (Exception ex)
                {
                    return;
                }
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

        
        public  void ExportTCListToCSV()
        {

            if (ActiveProject == ProjectSymbol.Optix)
            {
                OptixTCList.Clear();
                OptixTCList.AddRange(ActiveTCList);
                TestCasesListCSV.ExportTCListToCSV(OptixTCList, ActiveProject);
            }
            else if (ActiveProject == ProjectSymbol.ViewE)
            {
                ViewETCList.Clear();
                ViewETCList.AddRange(ActiveTCList);
                TestCasesListCSV.ExportTCListToCSV(ViewETCList, ActiveProject);
            }

        }
        
        public void ImportTCList()
        {          
            ActiveTCList.Clear();
                        
           if (this.ActiveProject == ProjectSymbol.Optix)
           {
                var tempOptixTCList = TestCasesListCSV.ImportTCList(ProjectSymbol.Optix);
                foreach (TestCase testCase in tempOptixTCList)
                ActiveTCList.Add(testCase);
                testCaseSearch.SetNewSearchList(ActiveTCList);
                FilterList(String.Empty);
                OptixTCList.Clear();
                OptixTCList.AddRange(tempOptixTCList);                            
           }
           else if (this.ActiveProject == ProjectSymbol.ViewE)
           {
                var tempViewETCList = TestCasesListCSV.ImportTCList(ProjectSymbol.ViewE);
                foreach (TestCase testCase in tempViewETCList)
                ActiveTCList.Add(testCase);
                testCaseSearch.SetNewSearchList(ActiveTCList);
                FilterList(String.Empty);                       
                ViewETCList.Clear();                            
                ViewETCList.AddRange(tempViewETCList);
           }                      
           GenerateTestCaseListCsvFile();                
        }

    }
}
