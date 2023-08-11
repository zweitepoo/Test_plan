﻿using System;
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

namespace Test_plan
{
    [Serializable]
    internal class TestPlanSerialization
    {
        public Collection<TestRun> TestRunSequence { get { return testRunSequence; } private set { } }
        private Collection<TestRun> testRunSequence  = new Collection<TestRun>();
        public string BuildNumberText { get; set; }
        public string ControllerBuildText { get; set; }
        public string FlashWithPreviousBuild { get; private set; }
        public string IgnoreFlashFault { get; private set; }
        public string DatabaseSQL { get; set; }
        public ProjectSymbol ActiveProject { get; private set; }

        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
        private string filePath;
       


        public TestPlanSerialization()
        {
            filePath = folder + @"\User_data.dat";
        }

        public void SerializeTestPlan(Collection<TestRun> testRunSequence)
        {
            this.testRunSequence.Clear();
            foreach (TestRun testRun in testRunSequence)
                this.testRunSequence.Add(testRun);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            saveFileDialog.Filter = "Test plan data file (*.dat)|*.dat";

            if (saveFileDialog.ShowDialog() == true)   
            {                
                
                using (Stream output = File.Open(saveFileDialog.FileName, FileMode.Create))
                {

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(output, this);
                }
                MessageBox.Show("Test plan data file saved: " + saveFileDialog.ToString());
            }

        }

        public void DeserializeTestPlan()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "Test plan data file (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() == true)
            {
                using (Stream input = openFileDialog.OpenFile())
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    try
                    {
                        TestPlanSerialization tempTestPlan = (TestPlanSerialization)formatter.Deserialize(input);
                        testRunSequence.Clear();
                        foreach (TestRun testRun in tempTestPlan.TestRunSequence)
                        {
                            testRunSequence.Add(testRun);
                        }
                    }
                    catch (SerializationException)
                    {
                        MessageBox.Show("Invalid Testplan file: " + openFileDialog.ToString());
                    }

                }


            }
        }



        public void SerializeUserData(string buildNumberText, string controllerBuildText, string flashWithPreviousBuild, string ignoreFlashFault, string databaseSQL, ProjectSymbol activeProject)
        {
            BuildNumberText = buildNumberText;
            ControllerBuildText = controllerBuildText;
            FlashWithPreviousBuild = flashWithPreviousBuild;
            IgnoreFlashFault = ignoreFlashFault;
            DatabaseSQL = databaseSQL;
            ActiveProject = activeProject;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using (Stream output = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, this);
               
            }
        }


        public bool DeserializeUserData()
        {

            if (!File.Exists(filePath))
                return false;
            try
            {
                using (Stream input = File.OpenRead(filePath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    TestPlanSerialization tempTestPLanSerialization = (TestPlanSerialization)formatter.Deserialize(input);

                    BuildNumberText = tempTestPLanSerialization.BuildNumberText;
                    ControllerBuildText = tempTestPLanSerialization.ControllerBuildText;   
                    FlashWithPreviousBuild = tempTestPLanSerialization.FlashWithPreviousBuild;
                    IgnoreFlashFault = tempTestPLanSerialization.IgnoreFlashFault;
                    DatabaseSQL = tempTestPLanSerialization.DatabaseSQL;
                    ActiveProject = tempTestPLanSerialization.ActiveProject;                    
                }
                
            }
            catch (SerializationException)
            {
                MessageBox.Show("Invalid UserData file: " + filePath);
            }
            return true;

        }


    }
}
