using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test_plan
{
    internal class QTestCycleTreeObject : QTestTreeObject
    {
        public QTestCycleTreeObject(string name, string pid, int id)
        {
            this.Name = name;
            this.Pid = pid;
            this.Id = id;
            this.ObjectIcon = SetIcon();
        }

        public override Image SetIcon()
        {
            var image = new Image();
            image.Source = FileSystemBitMapImage.GetCycleImage();
            return image;
        }

        public static List<QTestCycleTreeObject> GetCycles(QTestGetCycles apiGetCycles)
        {
            List<QTestCycleTreeObject> qtestCycles = new List<QTestCycleTreeObject>();
            var cycles = apiGetCycles.GetResponse();

            foreach (var cycle in cycles)
            {
                qtestCycles.Add(new QTestCycleTreeObject(cycle.Name, cycle.Pid, cycle.Id));
            }
            return qtestCycles;
        }
    }
}
