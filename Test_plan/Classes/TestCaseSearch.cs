using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan.Classes
{
    public class TestCaseSearch
    {
        public List<TestCase> Results { get; private set; }
        public string SearchText= "";

        public TestCaseSearch()
        {
            Results = new List<TestCase>();           
        }
        public void SetNewSearchList(IEnumerable<TestCase> activeList)
        {
           Results.Clear();
           Results.AddRange(activeList);
           SearchText = "";
        }

        public List<TestCase> FilterList(string searchText, IEnumerable<TestCase> activeList)
        {
            Results.Clear();
            var results = from tc in activeList
                          where tc.ToString().ToLower().Contains(searchText.ToLower())
                          select tc;
            Results.AddRange(results);

            return Results;
        }

            

    }
}
