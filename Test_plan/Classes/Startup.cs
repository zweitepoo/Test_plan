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
        public static void CreateLocalAppDirectory(string folderName)
        {
            string localAppFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + $"\\{folderName}";
            if (Directory.Exists(localAppFolder))
                return;
            else
            {
                Directory.CreateDirectory(localAppFolder);
            }
        }
    }
}
