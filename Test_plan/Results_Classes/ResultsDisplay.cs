using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    internal class ResultsDisplay
    {
        public string URL {  get; private set; }

        public void SetURL(string url)
        {
            URL = url;
        }
    }
}
