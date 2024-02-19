using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public static class FileFolderInfo
    {
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
        public static string GetFileExtension(string Path)
        {
            var rawExtension = System.IO.Path.GetExtension(Path);
            if (string.IsNullOrEmpty(rawExtension))
            {
                return string.Empty;
            }                
            var shortExtension = rawExtension
                                        .ToLower()
                                        .Substring(1);
            return shortExtension;
        }

        public static bool IsHtmlFile(string filePath)
        {
            var fileExtension = GetFileExtension(filePath);
            if (string.Equals(fileExtension, "html"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
                return true;
            else
                return false;
        }
        public static bool IsLogicalDrive(string path)
        {
            return (new DirectoryInfo(path).FullName == new DirectoryInfo(path).Root.FullName);
        }
       

    }
}
