﻿using System;
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

namespace Test_plan
{
    /// <summary>
    /// Interaction logic for PythonOutput.xaml
    /// </summary>
    public partial class PythonOutput : Window
    {
        TestPlan passedTestPlan;
        public PythonOutput(TestPlan testPlan)
        {
            InitializeComponent();
            passedTestPlan = testPlan;
            passedTestPlan.RunPythonScript(PythonOutputTxt);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            passedTestPlan.ClosePythonRun();
        }
    }
}
