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
        public QTest(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            testPlan = MainWindow.TestPlanPage.Resources["testPlan"] as TestPlan;        
            QTestList = new ObservableCollection<QTestInputTestRun>();

            InitializeComponent();
            InitializeDataGrid();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateQTestInputList();
        }

        private void CreateQTestInputList()
        {
            QTestList.Clear();
            foreach (ITestRunData testRun in testPlan.TestRunSequence)
            {
                QTestList.Add(new QTestInputTestRun(testRun));
            }
        }

        private void InitializeDataGrid()
        {
            
            TestsGrid.ItemsSource = QTestList;

        }

        private void NavigateTestPlan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTestPlanPage();
        }
        
    }
}
