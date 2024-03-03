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
            
            
        }

        private void InitializeLog()
        {
            Log.SetOutputTextBox(LogField);
            Log.Info("Logger Initialize");
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

        private void InitializeDataGrid()
        {
            
            testDataForQTest = new TestPlanDataForQTest();
            TestsGrid.ItemsSource = testDataForQTest.GetTestList();

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
