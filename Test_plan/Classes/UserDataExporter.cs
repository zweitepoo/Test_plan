using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    [Serializable]
    internal class UserDataExporter
    {
        public string BuildNumberText { get; set; }
        public string ControllerBuildText { get; set; }
        public string FlashWithPreviousBuild { get; private set; }
        public string IgnoreFlashFault { get; private set; }
        public string DatabaseSQL { get; set; }
        public ProjectSymbol ActiveProject { get; private set; }
        public string PythonExeFilePath { get; private set; }
        public string PythonScriptsFolderPath { get; private set; }
        public string PythonScriptFilePath { get; private set; }
        public string AddaBypass {  get; private set; }
        
         

       
        public void SaveUserData(TestPlan userData)
        {
            string userDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TestPlanGenerator";
            string userDataFilePath = userDataFolderPath + @"\User_data.dat"; 

            BuildNumberText = userData.BuildNumberText;
            ControllerBuildText = userData.ControllerBuildText;
            FlashWithPreviousBuild = userData.FlashWithPreviousBuild;
            IgnoreFlashFault = userData.IgnoreFlashFault;
            DatabaseSQL = userData.DatabaseSQL;
            ActiveProject = userData.ActiveProject;
            PythonExeFilePath = userData.PythonExeFilePath;
            PythonScriptsFolderPath = userData.PythonScriptsFolderPath;
            PythonScriptFilePath = userData.PythonScriptFilePath;
            AddaBypass = userData.AddaBypass;
            
            if (!Directory.Exists(userDataFolderPath))
                Directory.CreateDirectory(userDataFolderPath);

            using (Stream output = File.Open(userDataFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, this);
            }
        }
    }
}
