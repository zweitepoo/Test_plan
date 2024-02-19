using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_plan
{
    /// <summary>
    /// Interaction logic for QTest.xaml
    /// </summary>
    public partial class QTest : Page
    {
        MainWindow MainWindow;
        public QTest(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            var testPlan = MainWindow.TestPlanPage.Resources["testPlan"] as TestPlan;            
            InitializeComponent();
        }

        private void NavigateTestPlan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTestPlanPage();
        }
    }
}
