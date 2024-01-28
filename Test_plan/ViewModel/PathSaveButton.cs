using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test_plan.ViewModel
{
    public class PathSaveButton
    {
        public Button SaveButton { get; set; }
        public string ButtonText {  get; set; }      
        public string SelectedPath { get; private set; }
        static PathSaveButton() {}      

        public PathSaveButton(Button saveButton, string buttonText)
        {
            SaveButton = saveButton;
            ButtonText = buttonText;           
            
            SaveButton.Click += SaveButton_Click;
            SelectedPath = String.Empty;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveButton.Content = "Set default folder";
        }

        public void TreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
            {
                SaveButton.Content = "Set default folder";
                SelectedPath = String.Empty;
            }
            else
            {
                var treeItem = e.NewValue as FileSystemTreeInfo;
                var fileObject = treeItem.FileSystemObject;
                if (fileObject is DirectoryObject) 
                {
                    SaveButton.Content = "Set default folder*";
                    SaveButton.IsEnabled = true;
                    SelectedPath = fileObject.ObjectPath;


                }
                else
                {
                    SaveButton.Content = "Set default folder";
                    SaveButton.IsEnabled = false;                    
                    SelectedPath = String.Empty;
                }
            }
           
        }
    }
}
