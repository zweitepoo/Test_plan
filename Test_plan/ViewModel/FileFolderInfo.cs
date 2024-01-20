using System;
using System.Collections.Generic;
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
    }
}
