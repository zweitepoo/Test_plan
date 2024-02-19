using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Test_plan
{
    [Serializable]
    public class TestCase
    {
       
        public int TestCaseNumber { get; private  set; }
        
        public string TestName { get; private set; }
       
        public int AlarmInstance { get; private set; }

 


        public TestCase(int testCaseNumber, string testName, int alarmInstance)
        {
            TestCaseNumber = testCaseNumber;
            TestName = testName;
            AlarmInstance = alarmInstance;
           
        }

        public void UpdateTestCase(int testCaseNumber, string testName, int alarmInstance)
        {
            TestCaseNumber = testCaseNumber;
            TestName = testName;
            AlarmInstance = alarmInstance;
           
        }
       

        public override string ToString()
        {
            return "TC" + TestCaseNumber + "_" + TestName ;
        }
    }
}
