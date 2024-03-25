using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_plan.Interfaces;
using Test_plan;

namespace Test_plan
{
    public class QTestInputTestRun : PropertyNotifier, ITestRunData
    {
        public int TestRunNumber { get ; set ; }
        public int TestCaseNumber { get ; set; }
        public string DisplayName {  get ; set ; }
        public bool AllowTestRunNrChange { get ; set ; }
        public string TestRunPID { get { return String.Format("TR-{0}", TestRunNumber); } set { } }
        public string TestCasePID { get { return String.Format("TC-{0}", TestCaseNumber); } set { } }
        public int TestCaseId { get; set; }


        public QTestInputTestRun (ITestRunData runData)
        {
            TestRunNumber = runData.TestRunNumber;
            TestCaseNumber = runData.TestCaseNumber;
            DisplayName = runData.DisplayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public void SetAllowTestRunChange()
        {
            AllowTestRunNrChange = true;
            OnPropertyChanged("AllowTestRunNrChange");            
        }

        public void SetTestCaseId(int testCaseId)
        {
            TestCaseId = testCaseId;
        }
        public void SetTestRunNumber(string testRunPid)
        {
            int testRun;
            if( int.TryParse(testRunPid.Substring(3),out testRun))
            {
                TestRunNumber = testRun;
                OnPropertyChanged("TestRunPID");
            }
                          
        }

    }
}
