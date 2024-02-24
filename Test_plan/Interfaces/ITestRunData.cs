using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan.Interfaces
{
    public interface ITestRunData
    {
        
        public int TestRunNumber { get;  set; }
        public int TestCaseNumber { get; set; }
        public string DisplayName { get; set; }

    }
}
