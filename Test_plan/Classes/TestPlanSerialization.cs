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

namespace Test_plan
{
    [Serializable]
    public class TestPlanSerialization
    {
        public List<TestRun> TestRunSequence { get { return testRunSequence; } private set { } }
        private List<TestRun> testRunSequence  = new List<TestRun>();
        private string userDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
        private string userDataFilePath;
       


        public TestPlanSerialization()
        {
            userDataFilePath = userDataFolderPath + @"\User_data.dat";            
        }

        public void SerializeTestPlan(IEnumerable<TestRun> testRunSequence)
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
            saveFileDialog.Filter = "Test plan data file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)   
            {


                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    writer.WriteRecords(testRunSequence);
                }

                MessageBox.Show("Test plan data file saved: " + saveFileDialog.ToString());
            }           

        }

        public List<TestRun> ImportTestPlanListFromCSV()
        {
            var records = new List<TestRun>();
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
                            var record = new TestRun(reader.GetField<int>("TestCaseNumber"),
                                                     reader.GetField<int>("TestRunNumber"),
                                                     reader.GetField<int>("AlarmInstance"),
                                                     reader.GetField<string>("TestName"),
                                                     reader.GetField<string>("FlashType"),
                                                     reader.GetField<string>("ACD"),
                                                     reader.GetField<string>("VPD"),
                                                     reader.GetField<Controller[]>("ControllersSet"),
                                                     reader.GetField<TBSymbol>("TestbedSymbol"),
                                                     reader.GetField<bool[]>("PythonScripts")
                                                     );
                            records.Add(record);
                        }
                        return records;
                    }
                    catch (CsvHelperException ex)
                    {
                        throw new ImportCSVException(ex,"Wrong test plan list file - " + openFileDialog.ToString());                    
                    }

                }
            }
            else 
              return records; 
        }



    }
}
