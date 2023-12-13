using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public static class TestbedInfo
    {
        public static List<Controller> AllControllers = new List<Controller>()
            {
                new Controller(ControllerNum.CLX1, ControllerSymbol.L7x),
                new Controller(ControllerNum.CLX2, ControllerSymbol.L3y),
                new Controller(ControllerNum.CLX3, ControllerSymbol.L3z),
                new Controller(ControllerNum.CLX4, ControllerSymbol.L8z),
                new Controller(ControllerNum.CLX5, ControllerSymbol.EPIC),
                new Controller(ControllerNum.CLX6, ControllerSymbol.L8zS),
                new Controller(ControllerNum.CLX7, ControllerSymbol.L3zS),
                new Controller(ControllerNum.CLX8, ControllerSymbol.RedL7xL8z),
                new Controller(ControllerNum.CLX9, ControllerSymbol.L7x),
                new Controller(ControllerNum.CLX10, ControllerSymbol.L3y),
                new Controller(ControllerNum.CLX11, ControllerSymbol.L3z),
                new Controller(ControllerNum.CLX12, ControllerSymbol.L8z),
                new Controller(ControllerNum.CLX13, ControllerSymbol.EPIC),
                new Controller(ControllerNum.CLX14, ControllerSymbol.L8zS),
                new Controller(ControllerNum.CLX15, ControllerSymbol.L3zS),
                new Controller(ControllerNum.CLX16, ControllerSymbol.L7x),
                new Controller(ControllerNum.CLX17, ControllerSymbol.L3y),
                new Controller(ControllerNum.CLX18, ControllerSymbol.L3z),
                new Controller(ControllerNum.CLX19, ControllerSymbol.L8z),
                new Controller(ControllerNum.CLX20, ControllerSymbol.EPIC),
                new Controller(ControllerNum.CLX21, ControllerSymbol.L8zS),
                new Controller(ControllerNum.CLX22, ControllerSymbol.L3zS),
                new Controller(ControllerNum.CLX23, ControllerSymbol.L7x),
                new Controller(ControllerNum.CLX24, ControllerSymbol.L3y),
                new Controller(ControllerNum.CLX25, ControllerSymbol.L3z),
                new Controller(ControllerNum.CLX26, ControllerSymbol.L8z),
                new Controller(ControllerNum.CLX27, ControllerSymbol.EPIC),
                new Controller(ControllerNum.CLX28, ControllerSymbol.L8zS),
                new Controller(ControllerNum.CLX29, ControllerSymbol.L3zS),

        };

        public static Dictionary<TBSymbol, List<ControllerSymbol>> AllTestbedsAvailableControllers = new Dictionary<TBSymbol, List<ControllerSymbol>> {
               {TBSymbol.VES01, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.L3y, ControllerSymbol.L3zS, ControllerSymbol.RedL7xL8z}},
               {TBSymbol.VES02, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.L3y, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3zS}},
               {TBSymbol.VES11, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.L3y, ControllerSymbol.RedL7xL8z, ControllerSymbol.EPIC}},
               {TBSymbol.VES12, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.L3y, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3z, ControllerSymbol.EPIC}},
               {TBSymbol.VES21, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.L3y, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3z}},
               {TBSymbol.VES22, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.L3y, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3z}},
               {TBSymbol.VES31, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3z}},
               {TBSymbol.VES32, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3z}},
               {TBSymbol.VES41, new List<ControllerSymbol>{ ControllerSymbol.L7x, ControllerSymbol.L8z, ControllerSymbol.L8zS, ControllerSymbol.RedL7xL8z, ControllerSymbol.L3z}}
           };

    }
}

