using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Test_plan;

namespace Test_plan
{
    internal class QTestReleaseTreeObject : QTestTreeObject
    {

        public QTestReleaseTreeObject(string name, string pid, int id)
        {
            this.Name = name;
            this.Pid = pid;
            this.Id = id;
            this.ObjectIcon = SetIcon();
        }

        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetReleaseImage();
            return image;
        }

        public static List<QTestReleaseTreeObject> GetReleases(QTestGetReleases apiGetReleases)
        {
            List<QTestReleaseTreeObject> qtestReleases = new List<QTestReleaseTreeObject>();
            var releases = apiGetReleases.GetResponse();

            foreach (var release in releases)
            {
                qtestReleases.Add(new QTestReleaseTreeObject(release.Name, release.Pid, release.Id));
            }
            return qtestReleases;
        }
    }
}
