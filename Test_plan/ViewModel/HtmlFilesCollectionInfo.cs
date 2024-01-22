using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test_plan.ViewModel
{
    public class HtmlFilesCollectionInfo
    {
        public ObservableCollection<ExplorerObject> HtmlFiles;
        public ExplorerObject ReferenceFileObject {  get;private set; }

        public HtmlFilesCollectionInfo()
        {
            HtmlFiles = new ObservableCollection<ExplorerObject>();
        }

        public void GenerateFileList(string filePath)
        {
            if (FileFolderInfo.IsDirectory(filePath)||(FileFolderInfo.IsLogicalDrive(filePath)))
            {
                HtmlFiles.Clear();
                ReferenceFileObject = new DirectoryObject(filePath);
                AddChildrenDirectoriesFiles(filePath);
                AddChildrenHtmlFiles(filePath);
            }
            else if (FileFolderInfo.IsHtmlFile(filePath))
            {
                HtmlFiles.Clear();
                ReferenceFileObject = new HtmlFileObject(filePath);
                HtmlFiles.Add(ReferenceFileObject);
            } 
            else
            {

            }
        }

        private void AddChildrenHtmlFiles(string path)
        {
            try 
            {
                var files = Directory.GetFiles(path);
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        var fileExtension = FileFolderInfo.GetFileExtension(file);
                        if (string.Equals(fileExtension, "html"))
                        {
                            var newFile = new HtmlFileObject(file);
                            HtmlFiles.Add(newFile);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return;
            }

        }

        private void AddChildrenDirectoriesFiles(string path)
        {
            var directories = Directory.GetDirectories(path);
            if (directories.Length > 0)
            {
                foreach (var dir in directories)
                {
                    AddDirectoryHtmlFiles(dir);
                }
            }
        }
        private void AddDirectoryHtmlFiles(string path)
        {
            try
            {
                var files = Directory.GetFiles(path);
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        var fileExtension = FileFolderInfo.GetFileExtension(file);
                        if (string.Equals(fileExtension, "html"))
                        {
                            var newFile = new DirectoryNamedHtmlFile(file);
                            HtmlFiles.Add(newFile);
                        }
                    }
                }
            }  
            catch (UnauthorizedAccessException ex) 
            {
                return;          
            }
            
            
        }

    }
}
