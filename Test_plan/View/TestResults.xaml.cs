
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Test_plan.ViewModel;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;



namespace Test_plan
{
    /// <summary>
    /// Interaction logic for TestResults.xaml
    /// </summary>
    public partial class TestResults : Page
    {
       
        ResultsDisplay castResults;
        FileSystemTreeInfo castFileExplorer;
        MainWindow MainWindow;
        HtmlFilesCollectionInfo htmlFilesCollectionInfo;
        PathSaveButton PathSaveButton;
        public string yamlText {  get; set; }
       
        public TestResults(MainWindow mainWindow)
        {
            MainWindow = mainWindow;            
            InitializeComponent();
            InitializeTreeview();
            InitializeResultFileView();
            InitializeSaveButton();
            
            
            
          //  castResults = this.Resources["results"] as ResultsDisplay;
          //  string curDir = Directory.GetCurrentDirectory();             
        }

        private void InitializeSaveButton()
        {
            PathSaveButton = new PathSaveButton(SetDeafaultDirectory, "Set default folder");
            FileExplorerView.SelectedItemChanged += PathSaveButton.TreeView_SelectedItemChanged;
        }

        private void InitializeResultFileView()
        {
            htmlFilesCollectionInfo = new HtmlFilesCollectionInfo();
            ResultFilesToDisplay.ItemsSource = htmlFilesCollectionInfo.HtmlFiles;
        }

        private void FileExplorerView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var fileObject = (FileSystemTreeInfo)e.NewValue;
            var SelectedObjectPath = fileObject.GetFileObjectPath();
            htmlFilesCollectionInfo.GenerateFileList(SelectedObjectPath);  
          
        }

        private void InitializeTreeview()
        {
            FileExplorerView.SelectedItemChanged += FileExplorerView_SelectedItemChanged;
            DriveObject
                 .GetDrives()
                 .ToList()
                 .ForEach(drive =>
                 {
                     var fileSystemTreeObject = new FileSystemTreeInfo(drive);
                     FileExplorerView.Items.Add(fileSystemTreeObject);                     
                 });
            ExpandDefaultDirectory(FileExplorerView);
        }

        private void ExpandDefaultDirectory(TreeView fileTreeExplorerView)
        {
            string path = GetWorkingDirectory();
            if (String.IsNullOrEmpty(path))
                return;
            foreach(var item in fileTreeExplorerView.Items)
            {
                var fileObject = item as FileSystemTreeInfo;
                var fileObjectPath = fileObject.GetFileObjectPath();
                if (path.Contains(fileObjectPath))
                {
                    fileObject.IsExpanded = true;
                    if (path.Equals(fileObjectPath))
                    {
                        fileObject.IsSelected = true;
                        return;
                    }
                    ExpandDefaultDirectory(fileObject, path);
                    return;
                }
            }
        }

        private void ExpandDefaultDirectory(FileSystemTreeInfo parentObject, string path)
        {
            foreach (var fileObject in parentObject.Children)
            {                
                var fileObjectPath = fileObject.GetFileObjectPath();
                if (path.Contains(fileObjectPath))
                {
                    fileObject.IsExpanded = true;
                    if (path.Equals(fileObjectPath))
                    {
                        fileObject.IsSelected = true;
                        return;
                    }
                    ExpandDefaultDirectory(fileObject, path);
                    return;
                }
            }
        }

        private string GetWorkingDirectory()
        {
            if (File.Exists(Startup.LocalDirectory + "\\Paths.yaml"))
            {
                using (StreamReader sr = new StreamReader(Startup.LocalDirectory + "\\Paths.yaml"))
                {
                    yamlText = sr.ReadToEnd();
                }
                var deserializer = new DeserializerBuilder()
                     .WithNamingConvention(CamelCaseNamingConvention.Instance)
                     .Build();
                var dDict = deserializer.Deserialize<Dictionary<string, string>>(yamlText);
                System.Console.WriteLine(dDict["ResultWorkingDirectory"]);
                DefaultPathDisplay.Text = dDict["ResultWorkingDirectory"];
                return dDict["ResultWorkingDirectory"];
            }
            else
                return String.Empty; 
            
           
        }

        private void NavigateTestPlan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTestPlanPage();
        }

        private void ResultFilesToDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResultFilesToDisplay.SelectedItem == null)
                return;
            var htmlFile = (ExplorerObject)ResultFilesToDisplay.SelectedItem;
            var addres = String.Format(@"file:///" + htmlFile.ObjectPath);
            Browser.LoadUrl(addres);
        }

        private void SetDeafaultDirectory_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string,string> dict = new Dictionary<string, string>() 
            {
                {"ResultWorkingDirectory", PathSaveButton.SelectedPath }
                
            };
            DefaultPathDisplay.Text=PathSaveButton.SelectedPath;
            var serializer = new SerializerBuilder()
                               .WithNamingConvention(CamelCaseNamingConvention.Instance)
                               .Build();
            var yaml = serializer.Serialize(dict);
            using (StreamWriter sw = new StreamWriter(Startup.LocalDirectory + "\\Paths.yaml"))
            {
                sw.Write(yaml);
            }
        }
    }
}
