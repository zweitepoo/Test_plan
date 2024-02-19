using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public class TestRunCSVReadable
    {
        public int TestCaseNumber { get; private set; }
        public int AlarmInstance { get; private set; }
        public string TestName { get; private set; }
        public int TestRunNumber { get; private set; }

        public TBSymbol TestbedSymbol { get; set; }

        public bool Prepare_dirs { get; private set; }
        public bool Prepare_files { get; private set; }
        public bool Flash_controllers { get; private set; }
        public bool Import_l5k_to_acd { get; private set; }
        public bool Download_ACD { get; private set; }
        public bool Flash_terminals { get; private set; }
        public bool Tst_prepare_testbed { get; private set; }
        public bool Set_tbm_tags { get; private set; }
        public bool Report_runner {  get; private set; }
        public string FlashType { get; set; }

        public string ACD { get; set; }

        public string VPD { get; set; }

        public ControllerCodeName SlotCLX1Code { get; private set; }
        public ControllerCodeName SlotCLX2Code { get; private set; }
        public ControllerCodeName SlotCLX3Code { get; private set; }
        public ControllerCodeName SlotCLX4Code { get; private set; }

        public TestRunCSVReadable(TestRun testRun)
        {
            TestName = testRun.TestName;
            AlarmInstance = testRun.AlarmInstance;
            TestCaseNumber = testRun.TestCaseNumber;
            TestRunNumber = testRun.TestRunNumber;
            TestbedSymbol = testRun.TestbedSymbol;

            Prepare_dirs = testRun.PythonScripts[0];
            Prepare_files = testRun.PythonScripts[1];
            Flash_controllers = testRun.PythonScripts[2];
            Import_l5k_to_acd = testRun.PythonScripts[3];
            Download_ACD = testRun.PythonScripts[4];
            Flash_terminals = testRun.PythonScripts[5];
            Tst_prepare_testbed = testRun.PythonScripts[6];
            Set_tbm_tags = testRun.PythonScripts[7];
            Report_runner = testRun.PythonScripts[8];

            FlashType = testRun.FlashType;
            ACD = testRun.ACD;
            VPD = testRun.VPD;
            SlotCLX1Code = testRun.Slot_CLX1.ControllerCode;
            SlotCLX2Code = testRun.Slot_CLX2.ControllerCode;
            SlotCLX3Code = testRun.Slot_CLX3.ControllerCode;
            SlotCLX4Code = testRun.Slot_CLX4.ControllerCode;

        }

        public TestRunCSVReadable(int testCaseNumber, int alarmInstance, string testName, int testRunNumber, TBSymbol testbedSymbol, bool prepare_dirs, bool prepare_files, bool flash_controllers, bool import_l5k_to_acd, bool download_ACD, bool flash_terminals, bool tst_prepare_testbed, bool set_tbm_tags, bool report_runner, string flashType, string aCD, string vPD, ControllerCodeName slotCLX1Code, ControllerCodeName slotCLX2Code, ControllerCodeName slotCLX3Code, ControllerCodeName slotCLX4Code)
        {
            TestCaseNumber = testCaseNumber;
            AlarmInstance = alarmInstance;
            TestName = testName;
            TestRunNumber = testRunNumber;
            TestbedSymbol = testbedSymbol;
            Prepare_dirs = prepare_dirs;
            Prepare_files = prepare_files;
            Flash_controllers = flash_controllers;
            Import_l5k_to_acd = import_l5k_to_acd;
            Download_ACD = download_ACD;
            Flash_terminals = flash_terminals;
            Tst_prepare_testbed = tst_prepare_testbed;
            Set_tbm_tags = set_tbm_tags;
            Report_runner = report_runner;
            FlashType = flashType;
            ACD = aCD;
            VPD = vPD;
            SlotCLX1Code = slotCLX1Code;
            SlotCLX2Code = slotCLX2Code;
            SlotCLX3Code = slotCLX3Code;
            SlotCLX4Code = slotCLX4Code;
        }
    }
}
