using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace Test_plan.Classes
{
    internal class TestCasesListCSV
    {
        private static readonly string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";       
        public List<TestCase> TestCaseList;

        public TestCasesListCSV( IEnumerable<TestCase> testCaseList) 
        {
           
            TestCaseList = new List<TestCase>();
            TestCaseList.AddRange(testCaseList);            
        }
        public TestCasesListCSV()
        {

            TestCaseList = new List<TestCase>();
            
        }

        public void GenerateFile(ProjectSymbol projectSymbol) 
        {
            if (projectSymbol == ProjectSymbol.Optix)
            {
                using (StreamWriter streamWriter = new StreamWriter(folder + @"\TestCaseListOptix.csv"))
                {
                    var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    writer.WriteRecords(TestCaseList);
                }
            }
            else if (projectSymbol == ProjectSymbol.ViewE)
            {
                using (StreamWriter streamWriter = new StreamWriter(folder + @"\TestCaseListViewE.csv"))
                {
                    var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    writer.WriteRecords(TestCaseList);
                }
            }
        }

        public  List<TestCase> ImportTestCaseListFromCSV(ProjectSymbol projectSymbol)
        {
            
            if (projectSymbol == ProjectSymbol.Optix)
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(folder + @"\TestCaseListOptix.csv"))
                    {
                        var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                        var records = new List<TestCase>();
                        reader.Read();
                        reader.ReadHeader();
                        while (reader.Read())
                        {
                            var record = new TestCase(reader.GetField<int>("TestCaseNumber"), reader.GetField("TestName"), reader.GetField<int>("AlarmInstance"));
                            records.Add(record);
                        }


                        return records;
                    }
                }
                catch (CsvHelperException ex) 
                {
                    MessageBox.Show("Wrong TestCaseListOptix.csv file ");
                    return null;
                }
            }
            else
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(folder + @"\TestCaseListViewE.csv"))
                    {
                        var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                        var records = new List<TestCase>();
                        reader.Read();
                        reader.ReadHeader();
                        while (reader.Read())
                        {
                            var record = new TestCase(reader.GetField<int>("TestCaseNumber"), reader.GetField("TestName"), reader.GetField<int>("AlarmInstance"));
                            records.Add(record);
                        }

                        return records;
                    }
                }
                catch (CsvHelperException ex)
                {
                    MessageBox.Show("Wrong TestCaseListViewE.csv file ");
                    return null;
                }
            } 
        }

        
        
    }
}
