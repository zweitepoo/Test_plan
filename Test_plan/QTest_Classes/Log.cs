using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test_plan.QTest_Classes
{
   public static class Log
    {
        public static TextBlock OutputTextBox;
        public static void SetOutputTextBox(TextBlock textBox) 
        {
            if (textBox == null)
            {
                return;
            }
            else
                OutputTextBox = textBox;
        }

        public static void Info(string message)
        {
            OutputTextBox.Text += message + Environment.NewLine;
        }
        

    }
}
