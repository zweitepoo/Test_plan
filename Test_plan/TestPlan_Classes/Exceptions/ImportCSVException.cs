using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Test_plan
{
    public class ImportCSVException: Exception
    {
       private  CsvHelperException csvException;
        public string Message; 

        public ImportCSVException(CsvHelperException csvHelperException, string message)
        {
            this.csvException = csvHelperException;
            this.Message = message;
        }

        public override string ToString()
        {
            return Message;
        }

    }
}
