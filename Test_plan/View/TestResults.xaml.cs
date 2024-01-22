
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
        }

        

        private void NavigateTestPlan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTestPlanPage();
        }

        private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //ResultFilesToDisplay.Items.Clear();           
            //var item = (TreeViewItem)FolderView.SelectedItem;
            //var fullPath = (string)item.Tag;

            //var isFolder = new FileInfo(fullPath).Attributes.HasFlag(FileAttributes.Directory);
            //if (!isFolder)
            //    return;

            //    var files = new List<string>();
            //try
            //{
            //    var fileDirs = Directory.GetFiles(fullPath);

            //    if (fileDirs.Length > 0)
            //        files.AddRange(fileDirs);
            //}
            //catch { }

            
            //foreach ( var filePath in files)
            //{
            //    var file = GetFileFolderName(filePath);
            //    if (file.Contains(".html"))
            //    {                    
            //        ResultFilesToDisplay.Items.Add(filePath);                    
            //    }                    
            //}
            
        }

        private void ResultFilesToDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResultFilesToDisplay.SelectedItem == null)
                return;           
            var addres = String.Format(@"file:///" + ResultFilesToDisplay.SelectedItem.ToString());
            Browser.LoadUrl(addres);
        }
    }
}
