using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_plan.ViewModel;

namespace Test_plan.QTest_Classes
{
    internal class QTestSystemTreeInfo : PropertyNotifier
    {
        public ObservableCollection<QTestSystemTreeInfo> Children { get; set; }
        public QTestSystemObject QTestObject { get; set; }
        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
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
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                }
                OnPropertyChanged("IsSelected");
            }
        }


        public QTestSystemTreeInfo(QTestSystemObject objectData)
        {
            Children = new ObservableCollection<QTestSystemTreeInfo>();
            QTestObject = objectData;
            ChildrenInsertDummyObject();
            PropertyChanged += new PropertyChangedEventHandler(FileSystemTreeInfo_PropertyChanged);
        }
        private void ChildrenInsertDummyObject()
        {
            if (QTestObject is DriveObject || QTestObject is DirectoryObject)
            {
                Children.Add(null);
            }
        }
    }
}
