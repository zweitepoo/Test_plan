using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private bool isSelected;
        public bool IsSelected { get { return isSelected; } 
            set 
            {
                if (isSelected != value)
                {
                    isSelected = value;
                }
                OnPropertyChanged("IsSelected");                        
            } 
        }

        public  FileSystemTreeInfo(ExplorerObject objectData) 
        {
            Children = new ObservableCollection<FileSystemTreeInfo>();
            FileSystemObject = objectData;
            ChildrenInsertDummyObject();             
            PropertyChanged +=new PropertyChangedEventHandler(FileSystemTreeInfo_PropertyChanged);
        }
        public string GetFileObjectPath()
        {
            return FileSystemObject.ObjectPath;
        }
        private void ChildrenInsertDummyObject()
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
                else if  (string.Equals(e.PropertyName, "IsSelected", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (IsSelected == true)
                    OnObjectSelected();
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

       
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event RoutedEventHandler ObjectSelected;
        private void OnObjectSelected()
        {
            if (ObjectSelected != null)
                ObjectSelected(this, new RoutedEventArgs());
        }
 
    }
}
