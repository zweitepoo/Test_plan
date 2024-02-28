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
        TestDataForQTest testDataForQTest;
        public QTest(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            testPlan = MainWindow.TestPlanPage.Resources["testPlan"] as TestPlan;        
            QTestList = new ObservableCollection<QTestInputTestRun>();

            InitializeComponent();
            InitializeDataGrid();
            InitializeLog();

        }

        private void InitializeLog()
        {
            Log.SetOutputTextBox(LogField);
            Log.Info("Logger Initialize");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateQTestInputList();
        }

        private void CreateQTestInputList()
        {
            testDataForQTest.Clear();
            foreach (ITestRunData testRun in testPlan.TestRunSequence)
            {
                testDataForQTest.AddTest(new QTestInputTestRun(testRun));
            }
        }

        private void InitializeDataGrid()
        {
            testDataForQTest = new TestDataForQTest();
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
    }
}
