using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Test_plan.View;

namespace Test_plan
{
    public class PopUpNewCycle : PopUpNewObject
    {
        public string CycleName {  get;private set; }
        public PopUpNewCycle() 
        {
            this.SetMessage("Enter test cycle name");
        }

        public async Task<string> GetCycleName()
        {

            var NewTestCycleWindow = new NewTestObject(MessageText);
            NewTestCycleWindow.Closed += NewTestCycleWindow.ExitHandler;
            NewTestCycleWindow.Show();
            try
            {
                if (await NewTestCycleWindow.NameSetAsync())
                {
                    CycleName = NewTestCycleWindow.ObjectName;
                    NewTestCycleWindow.Close();                    
                    
                }
                else
                {
                    CycleName = String.Empty;
                    NewTestCycleWindow.Close();
                }
                return CycleName;
               
            }
            catch (InvalidOperationException ex)
            {
                Log.Info("New test cycle  - " + ex.Message);
                NewTestCycleWindow.Close();
                CycleName = String.Empty;
                return CycleName;
            }
        } 
    }
}
