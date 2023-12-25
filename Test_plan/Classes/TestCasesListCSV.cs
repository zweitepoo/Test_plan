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
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;

namespace Test_plan.Classes
{
    internal static class TestCasesListCSV
    {
        private static readonly string defaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";


        public static void GenerateOptixTestCaseListCSV(List<TestCase> testCaseList)
        {
            using (StreamWriter streamWriter = new StreamWriter(defaultFolder + @"\TestCaseListOptix.csv"))
            {
                var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                writer.WriteRecords(testCaseList);
            }

        }

        public static void GenerateViewETestCaseListCSV(List<TestCase> testCaseList)
        {
            using (StreamWriter streamWriter = new StreamWriter(defaultFolder + @"\TestCaseListViewE.csv"))
            {
                var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                writer.WriteRecords(testCaseList);
            }
        }

        public static List<TestCase> LoadOptixTestCaseListCSV()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(defaultFolder + @"\TestCaseListOptix.csv"))
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

        public static List<TestCase> LoadViewETestCaseListCSV()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(defaultFolder + @"\TestCaseListViewE.csv"))
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



        public static void ExportTCListToCSV(List<TestCase> testCaseList, ProjectSymbol activeProject)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            saveFileDialog.FileName = (activeProject == ProjectSymbol.Optix ? saveFileDialog.FileName = "TestCaseListOptix" : "TestCaseListViewE");
            saveFileDialog.Filter = "Test case list data file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    writer.WriteRecords(testCaseList);
                }
                MessageBox.Show("Test case list data file export done: " + saveFileDialog.ToString());
            }
        }



        public static List<TestCase> ImportTCList(ProjectSymbol activeProject)
        {
            var records = new List<TestCase>();
            var optixFileList = defaultFolder + @"\TestCaseListOptix.csv";
            var viewEFileList = defaultFolder + @"\TestCaseListViewE.csv";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = $"Test case {activeProject} list data file (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                    {
                        var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                        
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
                    return records;
                }
            }
            else
                return records;
        }
    }
}
