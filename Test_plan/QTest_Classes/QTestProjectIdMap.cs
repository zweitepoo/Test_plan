using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Test_plan
{
    
    public  class QTestProjectIdMap
    {
        public static Dictionary<ProjectSymbol, int> mapDict = new Dictionary<ProjectSymbol, int>()
            {
                { ProjectSymbol.Optix,120524 },
                { ProjectSymbol.ViewE, 86113 }
            };

        public static int GetProjectId(ProjectSymbol projectSymbol)
        {
            return mapDict[projectSymbol];            
        }
            

    }
}
