using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Test_plan.View;

namespace Test_plan
{
    public class PopUpNewSuite : PopUpNewObject
    {
        public string SuiteName {  get;private set; }
        public PopUpNewSuite() 
        {
            this.SetMessage("Enter test suite name");
        }

        public async Task<string> GetSuiteName()
        {

            var NewTestSuiteWindow = new NewTestObject(MessageText);
            NewTestSuiteWindow.Closed += NewTestSuiteWindow.ExitHandler;
            NewTestSuiteWindow.Show();
            try
            {
                if (await NewTestSuiteWindow.NameSetAsync())
                {
                    SuiteName = NewTestSuiteWindow.ObjectName;
                    NewTestSuiteWindow.Close();                    
                    
                }
                else
                {
                    SuiteName = String.Empty;
                    NewTestSuiteWindow.Close();
                }
                return SuiteName;
               
            }
            catch (InvalidOperationException ex)
            {
                Log.Info("New test suite  - " + ex.Message);
                NewTestSuiteWindow.Close();
                SuiteName = String.Empty;
                return SuiteName;
            }
        } 
    }
}
