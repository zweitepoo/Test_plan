using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test_plan.Interfaces;
using Test_plan.QTest_Classes;
using Test_plan.View;


namespace Test_plan
{
    /// <summary>
    /// Interaction logic for QTest.xaml
    /// </summary>
    public partial class QTest : Page
    {
        MainWindow MainWindow;
        ObservableCollection<QTestInputTestRun> QTestList;
        TestPlan testPlan;
        TestPlanDataForQTest testDataForQTest;
        QTestClient qTestClient;
        QTestGetReleases QTestGetReleases; 
        QTestGetCycles QTestGetCycles;
        QTestGetSuites QTestGetSuites;
        PopUpNewCycle PopUpNewCycle;
        PopUpNewSuite PopUpNewSuite;
        QTestSystemTreeInfo QTestExplorerObject;
       


        #region TestsObjects
        QTestGetReleases Test_GetReleasesInstance;
        List<QTestGetObject> Test_Releases;

       
        #endregion

        public QTest(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            testPlan = MainWindow.TestPlanPage.Resources["testPlan"] as TestPlan;        
            QTestList = new ObservableCollection<QTestInputTestRun>();

            InitializeComponent();
            InitializeDataGrid();
            InitializeLog();            
            InitializeTreeView();
            InitializeButtons();

            
           // RunTests();

        }

       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateQTestInputList();
            InitializeQTestClient();
            InitializeQTestCalls();            
            ReloadTreeView();

        }

       

        private void ReloadTreeView()
        {
            QTestExplorerView.Items.Clear();
            var qtestReleasesTreeObjects = QTestReleaseTreeObject.GetReleases(QTestGetReleases);
            qtestReleasesTreeObjects.ForEach(item =>
            {
                QTestExplorerView.Items.Add(new QTestSystemTreeInfo(item, qTestClient.Client, testDataForQTest.ProjectId));
            });

            var qtestCyclesTreeObjects = QTestCycleTreeObject.GetCycles(QTestGetCycles);
            qtestCyclesTreeObjects.ForEach(item =>
            {
                QTestExplorerView.Items.Add(new QTestSystemTreeInfo(item, qTestClient.Client, testDataForQTest.ProjectId));
            });

            var qtestSuitesTreeObjects = QTestSuiteTreeObject.GetSuites(QTestGetSuites);
            qtestSuitesTreeObjects.ForEach(item =>
            {
                QTestExplorerView.Items.Add(new QTestSystemTreeInfo(item, qTestClient.Client, testDataForQTest.ProjectId));
            });

        }
        private void InitializeButtons()
        {
            btNewTestSuite.IsEnabled = false;
            btNewTestCycle.IsEnabled = false;
            btDeleteObject.IsEnabled = false;
        }
        private void InitializeQTestCalls()
        {
            QTestGetReleases = new QTestGetReleases(qTestClient.Client, testDataForQTest.ProjectId);
            QTestGetCycles = new QTestGetCycles(qTestClient.Client, testDataForQTest.ProjectId,0,QTestParentType.Root);
            QTestGetSuites = new QTestGetSuites(qTestClient.Client, testDataForQTest.ProjectId, 0, QTestParentType.Root);
        }
        private void InitializeQTestClient()
        {
            qTestClient = new QTestClient("https://ra.qtestnet.com/");
            qTestClient.SetBearerToken("Bearer 3b63fc6c-fd1e-48a9-bafe-0900ea9fe8e3");
        }
        private void InitializeTreeView()
        {
            QTestExplorerView.SelectedItemChanged += QTestExplorerView_SelectedItemChanged;       

        }
        private void InitializeLog()
        {
            Log.SetOutputTextBox(LogField);
            Log.Info("Logger Initialize");
        }
        private void InitializeDataGrid()
        {
            
            testDataForQTest = new TestPlanDataForQTest();
            TestsGrid.ItemsSource = testDataForQTest.GetTestList();

        }

        private void CreateQTestInputList()
        {
            var qTestProjectId = QTestProjectIdMap.GetProjectId(testPlan.ActiveProject);
            testDataForQTest.SetQTestProjectId(qTestProjectId);

            testDataForQTest.Clear();
            foreach (ITestRunData testRun in testPlan.TestRunSequence)
            {
                testDataForQTest.AddTest(new QTestInputTestRun(testRun));
            }
        }
        private void CreateNewCycle(string cycleName)
        {
            if (QTestExplorerObject != null)
            {
                var newCycle = new QTestPostCycle(qTestClient.Client, QTestExplorerObject, cycleName);
                newCycle.PostCycle();
                QTestExplorerObject.RefreshTreeView();
            }
            else
            {
                MessageBox.Show("No QTest parent object selected for test cycle POST");
                Log.Info("No QTest parent object selected for test cycle POST");
            }
        }
        private void CreateNewSuite(string suiteName)
        {
            if (QTestExplorerObject != null)
            {
                var newSuite = new QTestPostSuite(qTestClient.Client, QTestExplorerObject, suiteName);
                newSuite.PostSuite();
                QTestExplorerObject.RefreshTreeView();
            }
            else
            {
                MessageBox.Show("No QTest parent object selected for test suite POST");
                Log.Info("No QTest parent object selected for test suite POST");
            }

        }
        private void DeleteObject(QTestSystemTreeInfo qTestExplorerObject)
        {
            if (qTestExplorerObject != null)
            {
                var QTestDelete = new QTestDelete(qTestClient.Client, qTestExplorerObject);
                QTestDelete.Delete();
                qTestExplorerObject.RefreshTreeView();
            }
            else
            {
                MessageBox.Show("No QTest object to delete");
                Log.Info("No QTest object to delete");
            }
            
        }

        private void QTestExplorerView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            QTestExplorerObject = (QTestSystemTreeInfo)e.NewValue;
            if (QTestExplorerObject.IsQtestCollectionObject)
            {
                btNewTestSuite.IsEnabled = true;
                btNewTestCycle.IsEnabled = true;
            }
            else
            {
                btNewTestSuite.IsEnabled = false;
                btNewTestCycle.IsEnabled = false;
            }

            if (QTestExplorerObject.CanBeDeleted ) 
            {
                btDeleteObject.IsEnabled = true;
            }
            else
            {
                btDeleteObject.IsEnabled = false;
            }
           

        }
        private void NavigateTestPlan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTestPlanPage();
        }
        private void CheckAllTests_Click(object sender, RoutedEventArgs e)
        {
            testDataForQTest.SetAllTestsInQTest();
            Log.Info("All tests run from a list set for a new test run number assign");
        }
        private async void btNewTestCycle_Click(object sender, RoutedEventArgs e)
        {
            PopUpNewCycle = new PopUpNewCycle();
            var cycleName = await PopUpNewCycle.GetCycleName();
            if (!String.IsNullOrEmpty(cycleName))
            {
                CreateNewCycle(cycleName);
            }
            else
            {
                Log.Info("Entered cycle name is null or empty");
            }
            
        }
        private async void btNewTestSuite_Click(object sender, RoutedEventArgs e)
        {
            PopUpNewSuite = new PopUpNewSuite();
            var suiteName = await PopUpNewSuite.GetSuiteName();
            if (!String.IsNullOrEmpty(suiteName))
            {
                CreateNewSuite(suiteName);
            }
            else
            {
                Log.Info("Entered suite name is null or empty");
            }

        }
        private void btDeleteObject_Click(object sender, RoutedEventArgs e)
        {
            DeleteObject(QTestExplorerObject);
        }

        

        private void LogField_TextChanged(object sender, TextChangedEventArgs e)
        {
            LogField.ScrollToEnd();
        }


        #region tests
        private void RunTests()
        {
            Test_GetReleases();
        }
        private void Test_GetReleases()
        {
            Test_GetReleasesInstance = new QTestGetReleases(qTestClient.Client, testDataForQTest.ProjectId);
            Test_Releases = new List<QTestGetObject>();
            var testRelease = Test_GetReleasesInstance.GetResponse();
            foreach (QTestGetObject testObject in testRelease)
            {
                Console.WriteLine(testObject.Pid + " "+ testObject.Name+ " "+ testObject.Id);
            }
        }


        #endregion

       
    }
}
