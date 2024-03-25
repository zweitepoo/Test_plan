using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    internal class QTestTestRun
    {
        public string name { get; set; }
        public QTestTestCaseId test_case { get; set; }
        public QTestTestRun(string name, int testCaseId)
        {
            this.name = name;
            test_case = new QTestTestCaseId(testCaseId);
        }
    }
}
