
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


namespace Test_plan
{
    /// <summary>
    /// Interaction logic for TestResults.xaml
    /// </summary>
    public partial class TestResults : Page
    {
        #region Constructor
        ResultsDisplay castResults;
        FileSystemTreeInfo castFileExplorer;
        MainWindow MainWindow;
       
        public TestResults(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            InitializeComponent();
            InitializeTreeview();
            castResults = this.Resources["results"] as ResultsDisplay;


            string curDir = Directory.GetCurrentDirectory(); 
            
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (FolderView.Items.Count>0)
                return;
            //Get every logical drive
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create a new item for it
                var item = new TreeViewItem()
                {
                    // Set the header and path
                    Header = drive.ToString(),
                    Tag = drive,
                };              

                item.Items.Add(null);

                //Listen out for items being expanded
                item.Expanded += Folder_Expanded;

                // Add it to the main tree view
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial checks
            var item  = (TreeViewItem)sender;

            //if the item only contains the dummy data

            if (item.Items.Count != 1 && item.Items[0] != null)
                return;
            item.Items.Clear();
            //Get full path
            var fullPath= (string)item.Tag;
            #endregion

            #region Get directories

            var directories  = new List<string>();
            try
            {
                var dirs  = Directory.GetDirectories(fullPath);
                    
                if (dirs.Length>0)
                    directories.AddRange(dirs);               
            }
            catch { }


            directories.ForEach(directoryPath =>
            {
                //Create directory item
                var subItem = new TreeViewItem() 
                {
                    Header= GetFileFolderName(directoryPath),
                    //tag as full path
                    Tag = directoryPath,

                };

                //Add dummy item so we can expand folder
                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expanded;

                //Add to parent
                item.Items.Add(subItem);    
            });
            #endregion

            #region Get files
            var files = new List<string>();
            try
            {
                var fileDirs = Directory.GetFiles(fullPath);

                if (fileDirs.Length > 0)
                    files.AddRange(fileDirs);
            }
            catch { }


            files.ForEach(filesPath =>
            {
                //Create file item
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filesPath),
                    //tag as full path
                    Tag = filesPath,

                };                             

                //Add to parent
                item.Items.Add(subItem);
                #endregion
            });

            }

        #endregion
        /// <summary>
        /// File the file or folder name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetFileFolderName(string path)
        {
            // C:\Something\a folder
            // C:\Something\a file.png

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // make all slashes backslashes
            var normalizedPath = path.Replace('/', '\\');
            // find the last backslash
            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);

            
        }

        private void NavigateTestPlan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTestPlanPage();
        }

        private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ResultFilesToDisplay.Items.Clear();           
            var item = (TreeViewItem)FolderView.SelectedItem;
            var fullPath = (string)item.Tag;

            var isFolder = new FileInfo(fullPath).Attributes.HasFlag(FileAttributes.Directory);
            if (!isFolder)
                return;

                var files = new List<string>();
            try
            {
                var fileDirs = Directory.GetFiles(fullPath);

                if (fileDirs.Length > 0)
                    files.AddRange(fileDirs);
            }
            catch { }

            
            foreach ( var filePath in files)
            {
                var file = GetFileFolderName(filePath);
                if (file.Contains(".html"))
                {                    
                    ResultFilesToDisplay.Items.Add(filePath);                    
                }                    
            }
            
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
