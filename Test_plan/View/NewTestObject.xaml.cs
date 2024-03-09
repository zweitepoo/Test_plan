using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test_plan.View
{
    /// <summary>
    /// Interaction logic for NewTestCycle.xaml
    /// </summary>
    public partial class NewTestObject : Window
    {

       
        public PopUpNewObject PopUpNewObject { get; set; }
        TaskCompletionSource<bool> tcs1;        
        public NewTestObject(PopUpNewObject popUpNewObject)
        {
            InitializeComponent();
            
            this.PopUpNewObject = popUpNewObject;
            tbSetValueHeader.Text = PopUpNewObject.MessageText;  

        }

        public Task<bool> NameSetAsync()
        {
            tcs1 = new TaskCompletionSource<bool>();
            return tcs1.Task;
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbObjectName.Text))
            {
                tcs1.SetException(new InvalidOperationException("New object name empty"));
            }
            else
            {
                PopUpNewObject.SetObjectName(tbObjectName.Text);
                tbObjectName.Text = string.Empty;
                tcs1.SetResult(true);
            }
            
            


        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PopUpNewObject.ClearName();
            tcs1.SetResult(false);

        }

        

        internal void ExitHandler(object sender, EventArgs e)
        {
            PopUpNewObject.ClearName();
            if (!tcs1.Task.IsCompleted)
            {
                tcs1.SetException(new InvalidOperationException("New object operation cancelled"));
            }
            

        }
    }
}
