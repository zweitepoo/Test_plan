using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using CsvHelper;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;

namespace Test_plan
{
    [Serializable]
    public static class TestRunSeqCSV
    {
        private static string userDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
       
        //public TestRunSeqCSV()
        //{                     
        //}

        public static void ExportTestRunSeqToCSV(IEnumerable<TestRun> testRunSequence)
        {
            var csvReadable = ConvertToCSVReadable(testRunSequence);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = userDataFolderPath;
            saveFileDialog.Filter = "Test plan data file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)   
            {

                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    writer.WriteRecords(csvReadable);
                }

                MessageBox.Show("Test plan data file saved: " + saveFileDialog.ToString());
            }           

        }

        public static List<TestRun> ImportTestRunSeqFromCSV()
        {

            var testRunCSVReadable = ImportCVSReadbaleTestRunSequence();
            var testRunSequence = ConvertFromCSVReadableToTestRunSeq(testRunCSVReadable); 
            return testRunSequence;
            
        }

        private static List<TestRun> ConvertFromCSVReadableToTestRunSeq(List<TestRunCSVReadable> testRunCSVReadable)
        {
            var testRunSequence = new List<TestRun>();
            foreach(var testRunCSV in testRunCSVReadable) 
            {
                var CLX1 = TestbedConfiguration.GenerateSingleControllerByNumber(testRunCSV.SlotCLX1Code);
                var CLX2 = TestbedConfiguration.GenerateSingleControllerByNumber(testRunCSV.SlotCLX2Code);
                var CLX3 = TestbedConfiguration.GenerateSingleControllerByNumber(testRunCSV.SlotCLX3Code);
                var CLX4 = TestbedConfiguration.GenerateSingleControllerByNumber(testRunCSV.SlotCLX4Code);
                var pythonScripts = new bool[]
                {
                    testRunCSV.Prepare_dirs,
                    testRunCSV.Prepare_files,
                    testRunCSV.Flash_controllers,
                    testRunCSV.Import_l5k_to_acd,
                    testRunCSV.Download_ACD,
                    testRunCSV.Flash_terminals,
                    testRunCSV.Tst_prepare_testbed,
                    testRunCSV.Set_tbm_tags,
                    testRunCSV.Report_runner
                };

                var testRun = new TestRun(
                    testRunCSV.TestCaseNumber,
                    testRunCSV.TestRunNumber,
                    testRunCSV.AlarmInstance,
                    testRunCSV.TestName,
                    testRunCSV.FlashType,
                    testRunCSV.ACD,
                    testRunCSV.VPD,
                    CLX1,
                    CLX2,
                    CLX3,
                    CLX4,
                    testRunCSV.TestbedSymbol,
                    pythonScripts
                 );
                testRunSequence.Add(testRun);
            }
            return testRunSequence;
        }

        private static List<TestRunCSVReadable> ImportCVSReadbaleTestRunSequence()
        {
            var records = new List<TestRunCSVReadable>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "Test plan data file (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                {
                    try
                    {
                        var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                        reader.Read();
                        reader.ReadHeader();
                        while (reader.Read())
                        {
                            var record = new TestRunCSVReadable( 
                                                                reader.GetField<int>("TestCaseNumber"),
                                                                reader.GetField<int>("AlarmInstance"),
                                                                reader.GetField<string>("TestName"),
                                                                reader.GetField<int>("TestRunNumber"),
                                                                reader.GetField<TBSymbol>("TestbedSymbol"),                                                                
                                                                reader.GetField<bool>("Prepare_dirs"),
                                                                reader.GetField<bool>("Prepare_files"),
                                                                reader.GetField<bool>("Flash_controllers"),
                                                                reader.GetField<bool>("Import_l5k_to_acd"),
                                                                reader.GetField<bool>("Download_ACD"),
                                                                reader.GetField<bool>("Flash_terminals"),
                                                                reader.GetField<bool>("Tst_prepare_testbed"),
                                                                reader.GetField<bool>("Set_tbm_tags"),
                                                                reader.GetField<bool>("Report_runner"),
                                                                reader.GetField<string>("FlashType"),
                                                                reader.GetField<string>("ACD"),
                                                                reader.GetField<string>("VPD"),
                                                                reader.GetField<ControllerCodeName>("SlotCLX1Code"),
                                                                reader.GetField<ControllerCodeName>("SlotCLX2Code"),
                                                                reader.GetField<ControllerCodeName>("SlotCLX3Code"),
                                                                reader.GetField<ControllerCodeName>("SlotCLX4Code")

                                                                );
                            records.Add(record);
                        }
                        return records;
                    }
                    catch (CsvHelperException ex)
                    {
                        throw new ImportCSVException(ex, "Wrong test plan list file - " + openFileDialog.ToString());
                    }

                }
            }
            else
                return records;

        }

        private static List<TestRunCSVReadable> ConvertToCSVReadable(IEnumerable<TestRun> testRunSequence)
        {
            var csvReadable  = new List<TestRunCSVReadable>();
            foreach (TestRun testRun in testRunSequence)
            {
                var testRunCSVReadable = new TestRunCSVReadable(testRun);
                csvReadable.Add(testRunCSVReadable);
            }
            return csvReadable;
        }

    }
}
