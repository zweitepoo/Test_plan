using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan.Classes
{
    public class TestCaseMap: ClassMap<TestCase>
    {
        public TestCaseMap()
        {
            Map(p => p.TestCaseNumber).Index(0);
            Map(p => p.TestName).Index(1);
            Map(p => p.AlarmInstance).Index(2);
        }

    }
}
