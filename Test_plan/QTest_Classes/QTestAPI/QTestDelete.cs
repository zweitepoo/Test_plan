using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using CefSharp.DevTools.IO;

namespace Test_plan
{
    internal class QTestDelete : QTestPost
    {
             
       public int ObjectId { get { return SelectedItem.QTestObject.Id; } set { } }
       private QTestSystemTreeInfo SelectedItem {  get; set; }


        public override string URL
        {
            get
            {
                string url = String.Empty;
                if (SelectedItem.QTestObject is QTestCycleTreeObject)
                {
                    url = String.Format($"api/v3/projects/{ProjectId}/test-cycles/{ObjectId}");
                }
                else if (SelectedItem.QTestObject is QTestSuiteTreeObject)
                {
                    url = String.Format($"api/v3/projects/{ProjectId}/test-suites/{ObjectId}");
                }
                else if (SelectedItem.QTestObject is QTestRunTreeObject)
                {
                    url = String.Format($"api/v3/projects/{ProjectId}/test-runs/{ObjectId}");
                }
                return url;
            }
            set { }
        }

        public QTestDelete(HttpClient httpClient, QTestSystemTreeInfo selectedItem)
        {
            this.HttpClient = httpClient;
            this.ProjectId = selectedItem.ProjectId;
            this.SelectedItem = selectedItem; 


        }
        public override string MessageHeader()
        {
            return "QTest delete object ";
        }

        public void Delete()
        {
                 

            var response = HttpClient.DeleteAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                 Log.Info(MessageHeader()+ SelectedItem.QTestObject.Name+ " successful");
            }
            else
            {
                Log.Info(MessageHeader()+ " fail " + response.StatusCode);
            }
        }
    }
}
