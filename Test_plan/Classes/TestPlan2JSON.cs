using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

using System.IO;


namespace Test_plan
{
    [Serializable]
    public class TestPlan2JSON
    {
        private Collection<TestRun> testRunSequence;
        private TBSymbol testBed;
        private ProjectSymbol activeProject;
        private string buildNumber;
        private string controllerBuild;
        private string flashWithPreviousBuild;
        private string ignoreFlashFault;
        private string databaseSQL;
        public string JSONtext { get { return GenerateJSONtext(); } private set { } }
      //  private string testPlanJson;

        public TestPlan2JSON (Collection<TestRun> testRunSequence, TBSymbol testBed, ProjectSymbol activeProject,
                                 string buildNumber, string controllerBuild, string flashWithPreviousBuild,
                                 string ignoreFlashFault, string databaseSQL)
        {
            this.testRunSequence = testRunSequence;
            this.testBed = testBed;
            this.activeProject = activeProject;
            this.buildNumber = buildNumber;
            this.controllerBuild = controllerBuild;
            this.flashWithPreviousBuild = flashWithPreviousBuild;
            this.ignoreFlashFault = ignoreFlashFault;
            this.databaseSQL = databaseSQL;            
        }

        private string GenerateJSONtext()
        {
                       string outputText=String.Empty;

             outputText = "{" + Environment.NewLine + "\"common_testbed_config\": {" + Environment.NewLine;

           // outputText += "\"testbed\"" + " " + "\"" + testBed + "\",";
            outputText =
                $$"""
                  {
                  "common_testbed_config": {
                  "testbed": "{{testBed.ToString()}}",
                  "project": "{{activeProject.ToString().ToLower()}}",
                  "build": "{{buildNumber}}",
                  "controller_build_version": {{controllerBuild}},
                  "flash_with_previous_build": {{flashWithPreviousBuild}},
                  "ignore_flash_fault": {{ignoreFlashFault}},
                  "database": "{{databaseSQL}}"
                  },
                  """ + Environment.NewLine;

            for (int i=0; i<testRunSequence.Count(); i++)
            {
                outputText+=
                    $$"""
                    "test_run_{{i+1}}": {
                    "flash_type": "{{testRunSequence[i].FlashType}}",
                    "files": {
                    "acd": "{{testRunSequence[i].ACD}}",
                    "vpd": "{{testRunSequence[i].VPD}}" 
                    },
                    "testbed_config": {
                    "test_case": "{{testRunSequence[i].TestCaseNumber}}",
                    "test_desc": "{{testRunSequence[i].ToString()}}",
                    "test_ID": "{{testRunSequence[i].TestRunNumber}}"
                    },
                    "alarm_config": {
                    "alarm_instance": "{{testRunSequence[i].AlarmInstance}}"
                    },
                    "controllers": {
                    "CLX1": {{((int)testRunSequence[i].Slot_CLX1.ControllerCode)}},
                    "CLX2": {{((int)testRunSequence[i].Slot_CLX2.ControllerCode)}},
                    "CLX3": {{((int)testRunSequence[i].Slot_CLX3.ControllerCode)}},
                    "CLX4": {{((int)testRunSequence[i].Slot_CLX4.ControllerCode)}}
                    },
                    "test_case_schedule": [

                    """;
                for (int j=0; j < testRunSequence[i].PythonScripts.Length; j++)
                {
                    if (testRunSequence[i].PythonScripts[j] == true)
                    {
                        switch (j)
                        {
                            case 0:
                                outputText += "\"prepare_dirs\"," + Environment.NewLine;
                                break;
                            case 1:
                                outputText += "\"prepare_files\"," + Environment.NewLine;
                                break;
                            case 2:
                                outputText += "\"flash_controllers\"," + Environment.NewLine;
                                break;
                            case 3:
                                outputText += "\"import_l5k_to_acd\"," + Environment.NewLine;
                                break;
                            case 4:
                                outputText += "\"download_acd\"," + Environment.NewLine;
                                break;
                            case 5:
                                outputText += "\"flash_terminals\"," + Environment.NewLine;
                                break;
                            case 6:
                                outputText += "\"tst_prepare_testbed\"," + Environment.NewLine;
                                break;
                            case 7:
                                outputText += "\"set_tbm_tags\"," + Environment.NewLine;
                                break;
                            case 8:
                                outputText += "\"report_runner\"," + Environment.NewLine;
                                break;
                        }
                    }
                    
                }
                
                outputText= outputText.Remove(outputText.Length - 3);
                outputText+= Environment.NewLine+ "]"+ Environment.NewLine + "}," +Environment.NewLine;
           
            }
            outputText = outputText.Remove(outputText.Length - 3);
            outputText += Environment.NewLine + "}";
            return outputText;
        }
        public static bool ValidateCommonData(Collection<TestRun> testRunSequence, TBSymbol testBed, ProjectSymbol activeProject,
                                 string buildNumber, string controllerBuild, string flashWithPreviousBuild,
                                 string ignoreFlashFault, string databaseSQL)
        {
            return true;
        }

    }
}
