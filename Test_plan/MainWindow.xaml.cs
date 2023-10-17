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
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace Test_plan
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestPlan castTestPlan;
        public OpenFileDialog openFileDialog = null;
        public MainWindow()
        {
            InitializeComponent();
            castTestPlan = this.Resources["testPlan"] as TestPlan;
            TB_Selection.SelectedIndex = 0;
            set_Optix.IsChecked = true;
            set_prevFlash.IsChecked = true;
            res_ignoreFlash.IsChecked = true;               
            castTestPlan.DeserializeUserData();
            UpdateRadioButtons();
            castTestPlan.DeserializeTestCasesList();
            tc_list.Items.Refresh();

        }

        private void UpdateRadioButtons()
        {
            if (castTestPlan.ActiveProject == ProjectSymbol.ViewE)
                set_ViewE.IsChecked = true;
            else if (castTestPlan.ActiveProject == ProjectSymbol.Optix)
                set_Optix.IsChecked = true;
            if (castTestPlan.FlashWithPreviousBuild == "1")
                set_prevFlash.IsChecked= true;
            else if (castTestPlan.FlashWithPreviousBuild == "0")
                res_prevFlash.IsChecked= true;
            if (castTestPlan.IgnoreFlashFault== "1")
                set_ignoreFlash.IsChecked= true;
            else if (castTestPlan.IgnoreFlashFault== "0")
                res_ignoreFlash.IsChecked= true;
        }

        // Insert new testrun in testqueue
        private void addTestRun_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.AddTestRun(TestQueue.SelectedIndex);
            TestQueue.Items.Refresh();



        }

        //Remove testrun from TestQueue list
        private void TestQueue_KeyDown(object sender, KeyEventArgs e)
        {
            if (TestQueue.SelectedIndex != -1)
            {
                if (e.Key == Key.Delete)

                    castTestPlan.RemoveTestRun(TestQueue.SelectedIndex);

            }

        }


        //Read Testrun data 
        private void TestQueue_DoubleTapped(object sender, MouseButtonEventArgs e)
        {
            if (TestQueue.SelectedIndex != -1)
            {
                castTestPlan.ShowTestRunValues(TestQueue.SelectedIndex);
            }

        }

        // Modify Testrun data
        private void modifyTestRun_Click(object sender, RoutedEventArgs e)
        {
            if (TestQueue.SelectedIndex != -1)
            {
                castTestPlan.UpdateTestRunValue(castTestPlan.TestRunSequence[TestQueue.SelectedIndex]);
                TestQueue.Items.Refresh();

            }

        }
        //Move up testrun in queue
        private void up_Click(object sender, RoutedEventArgs e)
        {
            if (TestQueue.SelectedIndex != -1)
            {
                if (TestQueue.SelectedIndex > 0)
                {
                    var tempIndex = TestQueue.SelectedIndex;
                    castTestPlan.MoveTestInQueue(TestQueue.SelectedIndex, -1);
                    TestQueue.SelectedIndex = tempIndex - 1;


                }

            }

        }
        //Move down testrun in queue
        private void down_Click(object sender, RoutedEventArgs e)
        {
            if (TestQueue.SelectedIndex != -1)
            {
                if (TestQueue.SelectedIndex < TestQueue.Items.Count - 1)
                {
                    var tempIndex = TestQueue.SelectedIndex;
                    castTestPlan.MoveTestInQueue(TestQueue.SelectedIndex, +1);
                    TestQueue.SelectedIndex = tempIndex + 1;
                }

            }

        }
        //Select TBxx


        private void TB_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CLX1_Selection.SelectedIndex = -1;
            CLX2_Selection.SelectedIndex = -1;
            CLX3_Selection.SelectedIndex = -1;
            CLX4_Selection.SelectedIndex = -1;
            castTestPlan.UpdateTestbedConfig(castTestPlan.TestbedList[TB_Selection.SelectedIndex], castTestPlan.TestbedConfig.TestbedSelected);
            SetControllersBGColor();
            TestQueue.Items.Refresh();



        }

        //Select testrun's Controllers   manually
        private void CLX1_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CLX1_Selection.SelectedIndex == -1)
                return;
            Controller clx1 = CLX1_Selection.SelectedItem as Controller;
            castTestPlan.SetCLXSlot(0, clx1);
            SetControllersBGColor();
        }
        private void CLX2_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CLX2_Selection.SelectedIndex == -1)
                return;
            Controller clx2 = CLX2_Selection.SelectedItem as Controller;
            castTestPlan.SetCLXSlot(1, clx2);
            SetControllersBGColor();
        }
        private void CLX3_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CLX3_Selection.SelectedIndex == -1)
                return;
            Controller clx3 = CLX3_Selection.SelectedItem as Controller;
            castTestPlan.SetCLXSlot(2, clx3);
            SetControllersBGColor();

        }
        private void CLX4_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CLX4_Selection.SelectedIndex == -1)
                return;
            Controller clx4 = CLX4_Selection.SelectedItem as Controller;
            castTestPlan.SetCLXSlot(3, clx4);
            SetControllersBGColor();
        }

        // Fast controllers setup buttons
        private void setAll_l8z_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[3]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[11]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[18]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[25]);
            SetControllersBGColor();
        }

        private void setAll_l7x_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[0]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[8]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[15]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[22]);
            SetControllersBGColor();

        }

        private void setAll_l8zS_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[5]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[13]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[20]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[27]);
            SetControllersBGColor();
        }

        private void setAll_l3y_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[1]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[9]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[16]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[23]);
            SetControllersBGColor();
        }

        private void setAll_l3z_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[2]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[10]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[17]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[24]);
            SetControllersBGColor();
        }

        private void setAll_l3zS_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[6]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[14]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[21]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[28]);
            SetControllersBGColor();
        }

        private void setAll_EPIC_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetCLXSlot(0, castTestPlan.TestbedConfig.AllControllersList[4]);
            castTestPlan.SetCLXSlot(1, castTestPlan.TestbedConfig.AllControllersList[12]);
            castTestPlan.SetCLXSlot(2, castTestPlan.TestbedConfig.AllControllersList[19]);
            castTestPlan.SetCLXSlot(3, castTestPlan.TestbedConfig.AllControllersList[26]);
            SetControllersBGColor();
        }

        
        public void SetControllersBGColor()
        {

            SolidColorBrush lightGreen = new SolidColorBrush(Colors.Green);
            SolidColorBrush red = new SolidColorBrush(Colors.Red);

            if (castTestPlan.CheckControllerIsAvailable(castTestPlan.ControllersSet[0]))
                CLX1_display.Background = lightGreen;
            else
                CLX1_display.Background = red;

            if (castTestPlan.CheckControllerIsAvailable(castTestPlan.ControllersSet[1]))
                CLX2_display.Background = lightGreen;
            else
                CLX2_display.Background = red;

            if (castTestPlan.CheckControllerIsAvailable(castTestPlan.ControllersSet[2]))
                CLX3_display.Background = lightGreen;
            else
                CLX3_display.Background = red;

            if (castTestPlan.CheckControllerIsAvailable(castTestPlan.ControllersSet[3]))
                CLX4_display.Background = lightGreen;
            else
                CLX4_display.Background = red;
        }

        private void saveTestPlan_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SerializeTestPlan();
        }

        private void loadTestPlan_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.DeserializeTestPlan();
        }

        private void set_Optix_Checked(object sender, RoutedEventArgs e)
        {

            castTestPlan.UpdateActiveProject(ProjectSymbol.Optix);
            castTestPlan.UpdateScripts(ProjectSymbol.Optix);
            CB_download_Acd.IsEnabled = false;
            CB_flash_Controllers.IsEnabled = false;
            CB_flash_terminals.IsEnabled = false;
            CB_import_L5k.IsEnabled = false;
            CB_prep_Dirs.IsEnabled = true;
            CB_prep_Files.IsEnabled = false;
            CB_report_runner.IsEnabled = true;
            CB_set_tbm_tags.IsEnabled = true;
            CB_tst_prepare_testbed.IsEnabled = false;

            CB_download_Acd.Opacity = 0.5;
            CB_flash_Controllers.Opacity = 0.5;
            CB_flash_terminals.Opacity = 0.5;
            CB_import_L5k.Opacity = 0.5;
            CB_prep_Dirs.Opacity = 1.0;
            CB_prep_Files.Opacity = 0.5;
            CB_report_runner.Opacity = 1.0;
            CB_set_tbm_tags.Opacity = 1.0;
            CB_tst_prepare_testbed.Opacity = 0.5;


        }

        private void set_ViewE_Checked(object sender, RoutedEventArgs e)
        {

            castTestPlan.UpdateActiveProject(ProjectSymbol.ViewE);
            castTestPlan.UpdateScripts(ProjectSymbol.ViewE);
            CB_download_Acd.IsEnabled = true;
            CB_flash_Controllers.IsEnabled = true;
            CB_flash_terminals.IsEnabled = true;
            CB_import_L5k.IsEnabled = true;
            CB_prep_Dirs.IsEnabled = true;
            CB_prep_Files.IsEnabled = true;
            CB_report_runner.IsEnabled = true;
            CB_set_tbm_tags.IsEnabled = true;
            CB_tst_prepare_testbed.IsEnabled = true;

            CB_download_Acd.Opacity = 1.0;
            CB_flash_Controllers.Opacity = 1.0;
            CB_flash_terminals.Opacity = 1.0;
            CB_import_L5k.Opacity = 1.0;
            CB_prep_Dirs.Opacity = 1.0;
            CB_prep_Files.Opacity = 1.0;
            CB_report_runner.Opacity = 1.0;
            CB_set_tbm_tags.Opacity = 1.0;
            CB_tst_prepare_testbed.Opacity = 1.0;
        }

        private void addTestCase_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.AddTestCase();
        }

        private void removeTestCase_Click(object sender, RoutedEventArgs e)
        {
            if (tc_list.SelectedIndex != -1)
                castTestPlan.RemoveTestCase(tc_list.SelectedItem);
            tc_list.SelectedIndex = -1;
        }

        private void removeTestRun_Click(object sender, RoutedEventArgs e)
        {
            if (TestQueue.SelectedIndex != -1)
                castTestPlan.RemoveTestRun(TestQueue.SelectedIndex);

        }

        

        private void tc_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tc_list.SelectedIndex != -1)
                castTestPlan.ShowTestCaseValues(castTestPlan.ActiveTCList[tc_list.SelectedIndex]);
        }
             


        private void set_prevFlash_Checked(object sender, RoutedEventArgs e)
        {
         castTestPlan.UpdateFlashWithPrevBuild(true);
        }

        private void res_prevFlash_Checked(object sender, RoutedEventArgs e)
        {
            castTestPlan.UpdateFlashWithPrevBuild(false);
        }

        private void set_ignoreFlash_Checked(object sender, RoutedEventArgs e)
        {
            castTestPlan.UpdateIgnoreFlashFault(true);
        }

        private void res_ignoreFlash_Checked(object sender, RoutedEventArgs e)
        {
            castTestPlan.UpdateIgnoreFlashFault(false);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void generateJSON_Click(object sender, RoutedEventArgs e)
        {
            if (!castTestPlan.TestPlanDataIsValid())
                return;
            castTestPlan.GenerateJSON();
        }

        private void TestPlanOutput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void saveJSON_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SaveJSON();
        }

        private void saveAsJsonfile_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SaveAsJSON();
        }

        private void openJSON_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.OpenJSON();
        }


       

        private void Menu_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Menu_TestPlan_Open_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.DeserializeTestPlan();
            TestQueue.Items.Refresh();
        }

        private void Menu_TestPlan_Save_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SerializeTestPlan();
        }

       

        private void Menu_Json_Open_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.OpenJSON();
        }

        private void Menu_Json_Save_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SaveJSON();
        }

        private void Menu_Json_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SaveAsJSON();
        }

        private void Menu_About_Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test plan generator v.1.0" + Environment.NewLine + "author: Jakub Madej");
        }

         private void Menu_Export_Test_Case_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.ExportTCList();

        }
        private void Menu_Test_Case_Import_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.ImportTCList();
        }

        private void Menu_Test_Case_Save_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SerializeTestCasesList();
        }

        private void CheckAllScripts_Click(object sender, RoutedEventArgs e)
        {
            if (castTestPlan.ActiveProject == ProjectSymbol.ViewE)
            {
                CB_prep_Files.IsChecked=true;
                CB_flash_Controllers.IsChecked=true;
                CB_import_L5k.IsChecked=true;
                CB_download_Acd.IsChecked=true;
                CB_flash_terminals.IsChecked = true;
                CB_tst_prepare_testbed.IsChecked=true;
            }
            CB_prep_Dirs.IsChecked=true;
            CB_set_tbm_tags.IsChecked=true;
            CB_report_runner.IsChecked=true;
        }
        
        private void Python_SetExePath_Click(object sender, RoutedEventArgs e)
        {
           castTestPlan.SetPythonExePath();
            castTestPlan.SerializeUserData();
        }

        private void Python_SetScriptPath_Click(object sender, RoutedEventArgs e)
        {
            castTestPlan.SetPythonScriptPath();
            castTestPlan.SerializeUserData();
        }

        private void Python_RunScript_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(castTestPlan.PythonExeFilePath))
                castTestPlan.SetPythonExePath();
            if (string.IsNullOrEmpty(castTestPlan.PythonScriptsFolderPath) || string.IsNullOrEmpty(castTestPlan.PythonScriptFilePath))
                castTestPlan.SetPythonScriptPath();
            
            castTestPlan.SerializeUserData();

            PythonOutput pythonOutput = new PythonOutput(castTestPlan);
            pythonOutput.Show();
        }

        private void TextBox_SearchTC_TextChanged(object sender, TextChangedEventArgs e)
        {
            castTestPlan.FilterList(TextBox_SearchTC.Text);
            tc_list.IsDropDownOpen = true;

        }
    }
}
