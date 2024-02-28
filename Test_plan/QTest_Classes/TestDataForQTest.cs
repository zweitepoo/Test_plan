using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_plan.Interfaces;

namespace Test_plan.QTest_Classes
{
   
    public class TestDataForQTest : PropertyNotifier
    {
        public ObservableCollection<QTestInputTestRun> QTestList { get; set; }
        
        public TestDataForQTest()
        {
            QTestList = new ObservableCollection<QTestInputTestRun>();
        }
        public void AddTest(ITestRunData testRun)
        {
            QTestList.Add(new QTestInputTestRun(testRun));
        }
        public void Clear()
        {
            QTestList.Clear();
        }

        public ObservableCollection<QTestInputTestRun> GetTestList()
        {
            return QTestList;
        }

        public void SetAllTestsInQTest()
        {            
            foreach(var test in QTestList) 
            {
                test.SetAllowTestRunChange();                
            }
        }
    }
}
