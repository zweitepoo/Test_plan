using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Test_plan;
using Test_plan.ViewModel;


namespace Test_plan
{
    public class FileSystemTreeInfo :INotifyPropertyChanged
    {
        public ObservableCollection<FileSystemTreeInfo> Children {  get; set; } 
        
        public ExplorerObject FileSystemObject {  get; set; }
        private bool isExpanded;
        public bool IsExpanded { get { return isExpanded; } 
            set 
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                }
                OnPropertyChanged("IsExpanded");                        
            } 
        }

        public  FileSystemTreeInfo(ExplorerObject objectData) 
        {
            Children = new ObservableCollection<FileSystemTreeInfo>();
            FileSystemObject = objectData;
            InsertDummyObject();            
            PropertyChanged +=new PropertyChangedEventHandler(FileSystemTreeInfo_PropertyChanged);
        }

        private void InsertDummyObject()
        {
            if (FileSystemObject is DriveObject || FileSystemObject is  DirectoryObject)
            {
                Children.Add(null);
            }
        }

        private void FileSystemTreeInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (FileSystemObject is DriveObject || FileSystemObject is DirectoryObject)
            {
                if (string.Equals(e.PropertyName,"IsExpanded",StringComparison.CurrentCultureIgnoreCase))
                {
                    if (IsExpanded)
                    {
                        Children.Clear();
                        ExploreDirectories();
                        ExploreFiles();
                    }
                }
            }
        }

        private void ExploreFiles()
        {
            if (FileSystemObject is (DriveObject or DirectoryObject))
            {
                var files = Directory.GetFiles(FileSystemObject.ObjectPath);
                if(files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        FileObject newFile;
                        var fileExtension = FileFolderInfo.GetFileExtension(file);
                        if (string.Equals(fileExtension, "html"))
                        {
                            newFile = new HtmlFileObject(file);
                        }
                        else
                        {
                            newFile = new FileObject(file);
                        }
                        
                        Children.Add(new FileSystemTreeInfo(newFile));
                    }
                }
            }
        }

        private void ExploreDirectories()
        {
           if (FileSystemObject is (DriveObject or DirectoryObject))
            {
                var directories = Directory.GetDirectories(FileSystemObject.ObjectPath);
                if (directories.Length > 0)
                {
                    
                    foreach (var dir in directories)
                    {
                        DirectoryObject newDir  = new DirectoryObject(dir);
                        Children.Add(new FileSystemTreeInfo(newDir));
                    }
                }
               
            }
        }

        public void AddNullItemToFileObjects() 
        {
            
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
