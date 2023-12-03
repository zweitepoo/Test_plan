using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test_plan.Classes
{
    internal class UserDataImporter
    {
        public string BuildNumberText { get;private set; }
        public string ControllerBuildText { get;private set; }
        public string FlashWithPreviousBuild { get; private set; }
        public string IgnoreFlashFault { get; private set; }
        public string DatabaseSQL { get;private set; }
        public ProjectSymbol ActiveProject { get; private set; }
        public string PythonExeFilePath { get; private set; }
        public string PythonScriptsFolderPath { get; private set; }
        public string PythonScriptFilePath { get; private set; }
        public bool isDerserializeDone { get; private set; }


        public void DeserializeUserData()
        {
            isDerserializeDone = false;
            string userDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
            string userDataFilePath = userDataFolderPath + @"\User_data.dat";

            if (!File.Exists(userDataFilePath))
                return ;
            try
            {
                using (Stream input = File.OpenRead(userDataFilePath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    UserDataExporter userData = (UserDataExporter)formatter.Deserialize(input);

                    if (String.IsNullOrEmpty(userData.BuildNumberText))
                        BuildNumberText = "empty";
                    else
                        BuildNumberText = userData.BuildNumberText;

                    if (String.IsNullOrEmpty(userData.ControllerBuildText))
                        ControllerBuildText = "36";
                    else
                        ControllerBuildText = userData.ControllerBuildText;

                    if (String.IsNullOrEmpty(userData.FlashWithPreviousBuild))
                        FlashWithPreviousBuild = "0";
                    else
                        FlashWithPreviousBuild = userData.FlashWithPreviousBuild;

                    if (String.IsNullOrEmpty(userData.IgnoreFlashFault))
                        IgnoreFlashFault = "0";
                    else
                        IgnoreFlashFault = userData.IgnoreFlashFault;

                    if (String.IsNullOrEmpty(userData.DatabaseSQL))
                        DatabaseSQL = "ViewE_SQL";
                    else
                        DatabaseSQL = userData.DatabaseSQL;

                    if (userData.ActiveProject == 0)
                        ActiveProject = ProjectSymbol.Optix;
                    else
                        ActiveProject = userData.ActiveProject;

                    PythonExeFilePath = userData.PythonExeFilePath;
                    PythonScriptFilePath = userData.PythonScriptFilePath;
                    PythonScriptsFolderPath = userData.PythonScriptsFolderPath;
                    isDerserializeDone = true;
                }

            }
            catch (SerializationException)
            {
                MessageBox.Show("Invalid UserData file: " + userDataFilePath);                
            }
            

        }
    }
}
