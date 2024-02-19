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
using System.Windows.Shapes;
using Test_plan;

namespace Test_plan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public Page TestPlanPage;
       public  Page TestResultPage;
       public Page QTestPage;
        public MainWindow()
        {
            InitializeComponent();
            Startup.CreateLocalAppDirectory("TestPlanGenerator");
            this.TestPlanPage = new TestPlanPage(this);
            this.TestResultPage = new TestResults(this);
            this.QTestPage = new QTest(this);

            NavigateTestPlanPage();
        }
       

        public void NavigateTestPlanPage()
        {
            Main.Content = this.TestPlanPage;
        }
        public void NavigateTestResultPage()
        {
            Main.Content = this.TestResultPage;
        }
        public void NavigateQTestPage()
        {
            Main.Content = this.QTestPage;
        }
    }
}
