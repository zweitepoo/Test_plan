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
                new Controller(ControllerCodeName.CLX1, ControllerFamilyType.L7x),
                new Controller(ControllerCodeName.CLX2, ControllerFamilyType.L3y),
                new Controller(ControllerCodeName.CLX3, ControllerFamilyType.L3z),
                new Controller(ControllerCodeName.CLX4, ControllerFamilyType.L8z),
                new Controller(ControllerCodeName.CLX5, ControllerFamilyType.EPIC),
                new Controller(ControllerCodeName.CLX6, ControllerFamilyType.L8zS),
                new Controller(ControllerCodeName.CLX7, ControllerFamilyType.L3zS),
                new Controller(ControllerCodeName.CLX8, ControllerFamilyType.RedL7xL8z),
                new Controller(ControllerCodeName.CLX9, ControllerFamilyType.L7x),
                new Controller(ControllerCodeName.CLX10, ControllerFamilyType.L3y),
                new Controller(ControllerCodeName.CLX11, ControllerFamilyType.L3z),
                new Controller(ControllerCodeName.CLX12, ControllerFamilyType.L8z),
                new Controller(ControllerCodeName.CLX13, ControllerFamilyType.EPIC),
                new Controller(ControllerCodeName.CLX14, ControllerFamilyType.L8zS),
                new Controller(ControllerCodeName.CLX15, ControllerFamilyType.L3zS),
                new Controller(ControllerCodeName.CLX16, ControllerFamilyType.L7x),
                new Controller(ControllerCodeName.CLX17, ControllerFamilyType.L3y),
                new Controller(ControllerCodeName.CLX18, ControllerFamilyType.L3z),
                new Controller(ControllerCodeName.CLX19, ControllerFamilyType.L8z),
                new Controller(ControllerCodeName.CLX20, ControllerFamilyType.EPIC),
                new Controller(ControllerCodeName.CLX21, ControllerFamilyType.L8zS),
                new Controller(ControllerCodeName.CLX22, ControllerFamilyType.L3zS),
                new Controller(ControllerCodeName.CLX23, ControllerFamilyType.L7x),
                new Controller(ControllerCodeName.CLX24, ControllerFamilyType.L3y),
                new Controller(ControllerCodeName.CLX25, ControllerFamilyType.L3z),
                new Controller(ControllerCodeName.CLX26, ControllerFamilyType.L8z),
                new Controller(ControllerCodeName.CLX27, ControllerFamilyType.EPIC),
                new Controller(ControllerCodeName.CLX28, ControllerFamilyType.L8zS),
                new Controller(ControllerCodeName.CLX29, ControllerFamilyType.L3zS),

        };

        public static Dictionary<TBSymbol, List<ControllerFamilyType>> AllTestbedsAvailableControllers = new Dictionary<TBSymbol, List<ControllerFamilyType>> {
               {TBSymbol.VES01, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.L3y, ControllerFamilyType.L3zS, ControllerFamilyType.RedL7xL8z}},
               {TBSymbol.VES02, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.L3y, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3zS}},
               {TBSymbol.VES11, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.L3y, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.EPIC}},
               {TBSymbol.VES12, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.L3y, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3z, ControllerFamilyType.EPIC}},
               {TBSymbol.VES21, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.L3y, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3z}},
               {TBSymbol.VES22, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.L3y, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3z}},
               {TBSymbol.VES31, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3z}},
               {TBSymbol.VES32, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3z}},
               {TBSymbol.VES41, new List<ControllerFamilyType>{ ControllerFamilyType.L7x, ControllerFamilyType.L8z, ControllerFamilyType.L8zS, ControllerFamilyType.RedL7xL8z, ControllerFamilyType.L3z}}
           };

    }
}

