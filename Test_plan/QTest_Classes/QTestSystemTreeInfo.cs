using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
        public HttpClient Client { get; private set; }
        public int ProjectId { get ; private set; }
        public string ParentType { get
            {
                if (QTestObject is QTestCycleTreeObject)
                {
                    return QTestParentType.TestCycle;
                }
                else if (QTestObject is QTestReleaseTreeObject)
                {
                    return QTestParentType.Release;
                }
                else if (QTestObject is QTestSuiteTreeObject)
                {
                    return QTestParentType.TestSuite;
                }
                else
                {
                    return QTestParentType.Root;
                }

            }
            set { }
        }
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

        public bool IsTestSuiteObject
        {
            get
            {
                if (QTestObject is QTestSuiteTreeObject )
                {
                    return true;
                }
                else return false;
            }
            set { }
        }
        public bool CanBeDeleted
        {
            get
            {
                if ((QTestObject is QTestSuiteTreeObject)|| (QTestObject is QTestRunTreeObject) || (QTestObject is QTestCycleTreeObject))
                {
                    return true;
                }
                else return false;
            }
            set { }
        }


        public QTestSystemTreeInfo(QTestTreeObject objectData, HttpClient client, int projectId)
        {
            Children = new ObservableCollection<QTestSystemTreeInfo>();
            QTestObject = objectData;
            if (IsTestSuiteObject || IsQtestCollectionObject)
            {
                ChildrenInsertDummyObject();
            }
            
            PropertyChanged += new PropertyChangedEventHandler(FileSystemTreeInfo_PropertyChanged);
            this.Client = client;
            this.ProjectId = projectId;
            
        }
        public void RefreshTreeView()
        {
            IsExpanded = true;
        }
        private void ChildrenInsertDummyObject()
        {
                Children.Add(null);           
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
                        ExploreTestCycles();
                        ExploreTestSuites();

                    }
                }
                else if (string.Equals(e.PropertyName, "IsSelected", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (IsSelected == true)
                        OnObjectSelected();
                }
            }
            else if (IsTestSuiteObject)
            {
               
                    if (string.Equals(e.PropertyName, "IsExpanded", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (IsExpanded)
                        {
                            Children.Clear();                            
                            ExploreTestRuns();

                        }
                    }
                    else if (string.Equals(e.PropertyName, "IsSelected", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (IsSelected == true)
                            OnObjectSelected();
                    }
                
            }
        }

        private void ExploreTestRuns()
        {
            if (IsTestSuiteObject)
            {
                var TestGetRuns = new QTestGetRuns(Client, ProjectId, QTestObject.Id, ParentType);
                var qtestRunsTreeObjects = QTestRunTreeObject.GetRuns(TestGetRuns);
                qtestRunsTreeObjects.ForEach(item =>
                {
                    Children.Add(new QTestSystemTreeInfo(item, Client, ProjectId));
                });
            }
        }

        private void ExploreTestSuites()
        {
            var TestGetSuites = new QTestGetSuites(Client, ProjectId, QTestObject.Id, ParentType);
            var qtestSuitesTreeObjects = QTestSuiteTreeObject.GetSuites(TestGetSuites);
            qtestSuitesTreeObjects.ForEach(item =>
            {
                Children.Add(new QTestSystemTreeInfo(item, Client, ProjectId));
            });
        }

        private void ExploreTestCycles()
        {
            if (IsQtestCollectionObject)
            {
                var TestGetCycles = new QTestGetCycles(Client, ProjectId, QTestObject.Id, ParentType);
                var qtestCyclesTreeObjects = QTestCycleTreeObject.GetCycles(TestGetCycles);
                qtestCyclesTreeObjects.ForEach(item =>
                {
                    Children.Add(new QTestSystemTreeInfo(item, Client, ProjectId));
                });               
            }
        }

        public event RoutedEventHandler ObjectSelected;
        private void OnObjectSelected()
        {
            if (ObjectSelected != null)
                ObjectSelected(this, new RoutedEventArgs());
        }
    }
}
