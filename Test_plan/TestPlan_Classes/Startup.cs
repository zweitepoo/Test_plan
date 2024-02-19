using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    internal static class Startup
    {
        public static string LocalDirectory { get; set; }
        public static void CreateLocalAppDirectory(string folderName)
        {
            LocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + $"\\{folderName}";
            if (Directory.Exists(LocalDirectory))
                return;
            else
            {
                Directory.CreateDirectory(LocalDirectory);
            }
        }
    }
}
