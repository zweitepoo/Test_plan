
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Test_plan.ViewModel;



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
       
        public TestResults(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            InitializeComponent();
            InitializeTreeview();
            InitializeResultFileView();
            FileExplorerView.SelectedItemChanged += FileExplorerView_SelectedItemChanged;
            castResults = this.Resources["results"] as ResultsDisplay;
            string curDir = Directory.GetCurrentDirectory();             
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
            return @"C:\";
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

        }
    }
}
