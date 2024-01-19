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
        private bool iSExpanded;
        public bool IsExpanded { get { return iSExpanded; } 
            set 
            {
                if (iSExpanded != value)
                {
                    iSExpanded = value;
                }
                OnPropertyChanged("IsExpanded");                        
            } 
        }

        public  FileSystemTreeInfo(ExplorerObject objectData) 
        {
            Children = new ObservableCollection<FileSystemTreeInfo>();
            FileSystemObject = objectData;
            Children.Add(null);
            PropertyChanged +=new PropertyChangedEventHandler(FileSystemTreeInfo_PropertyChanged);
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
