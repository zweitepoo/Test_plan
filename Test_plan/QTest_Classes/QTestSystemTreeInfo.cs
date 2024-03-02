using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Test_plan.ViewModel;

namespace Test_plan
{
    internal class QTestSystemTreeInfo : PropertyNotifier
    {
        public ObservableCollection<QTestSystemTreeInfo> Children { get; set; }
        public QTestTreeObject QTestObject { get; set; }
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

        public bool IsQtestCollectionObject {
            get
            {
                if (QTestObject is QTestReleaseTreeObject || QTestObject is QTestCycleTreeObject)
                {
                    return true;
                }
                else return false;
            }
            set { } }


        public QTestSystemTreeInfo(QTestTreeObject objectData)
        {
            Children = new ObservableCollection<QTestSystemTreeInfo>();
            QTestObject = objectData;
            ChildrenInsertDummyObject();
            PropertyChanged += new PropertyChangedEventHandler(FileSystemTreeInfo_PropertyChanged);
        }
        private void ChildrenInsertDummyObject()
        {
            if (IsQtestCollectionObject)
            {
                Children.Add(null);
            }
        }


        private void FileSystemTreeInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsQtestCollectionObject)
            {
                if (string.Equals(e.PropertyName, "IsExpanded", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (IsExpanded)
                    {
                        Children.Clear();
                        ExploreDirectories();
                        ExploreTestSuites();

                    }
                }
                else if (string.Equals(e.PropertyName, "IsSelected", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (IsSelected == true)
                        OnObjectSelected();
                }
            }
        }

        private void ExploreTestSuites()
        {
            throw new NotImplementedException();
        }

        private void ExploreDirectories()
        {
            throw new NotImplementedException();
        }

        public event RoutedEventHandler ObjectSelected;
        private void OnObjectSelected()
        {
            if (ObjectSelected != null)
                ObjectSelected(this, new RoutedEventArgs());
        }
    }
}
