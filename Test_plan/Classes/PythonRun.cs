using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;


using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Test_plan
{
    internal class PythonRun
    {
        public string PythonExeFilePath { get; private set; }
        public string PythonScriptsFolderPath { get; private set; }
        public string PythonScriptFilePath { get; private set; }
        private OpenFileDialog openFileDialog = null;

        public PythonRun()
        {
            PythonExeFilePath = string.Empty;
            PythonScriptsFolderPath = string.Empty;
            PythonScriptFilePath = string.Empty;
        }

        public void SetPythonExePath()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "python.exe  (*.exe)|*.exe";

            if (openFileDialog.ShowDialog() == true)
            {
                PythonExeFilePath = System.IO.Path.GetFullPath(openFileDialog.FileName);
            }
        }

        public void SetPythonScriptPath()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "*.py file (*.py)|*.py";

            if (openFileDialog.ShowDialog() == true)
            {

                PythonScriptsFolderPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);

                PythonScriptFilePath = System.IO.Path.GetFullPath(openFileDialog.FileName);

            }
        }

        public void RunPythonScript(TextBox textBox)

        {
            Process process;
            process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += new DataReceivedEventHandler((sendingProcess, dataLine) =>
            {
                if (dataLine.Data != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        textBox.Text += dataLine.Data + Environment.NewLine;
                        textBox.ScrollToEnd();
                    });
                };
            });
            process.EnableRaisingEvents = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            process.BeginOutputReadLine();
            process.StandardInput.WriteLine($"cd {PythonScriptsFolderPath}");
            process.StandardInput.WriteLine($"{PythonExeFilePath} \"{PythonScriptFilePath}\"");
            process.StandardInput.Close();

            // process.WaitForExit();
            // process.WaitForExit();
        }

        internal void UpdatePaths(string pythonExeFilePath, string pythonScriptsFolderPath, string pythonScriptFilePath)
        {
            PythonExeFilePath = pythonExeFilePath;
            PythonScriptsFolderPath= pythonScriptsFolderPath;   
            PythonScriptFilePath= pythonScriptFilePath;
        }
    }
}
