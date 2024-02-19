using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public class NoDataForControllerException: Exception
    {
        public string Message;
        public NoDataForControllerException(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
