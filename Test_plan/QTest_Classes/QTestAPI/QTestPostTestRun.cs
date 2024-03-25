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
    internal class QTestPostTestRun : QTestPost
    {
        public int ParentId { get;private set; }
        public string ParentType { get;private set; }
        public string Pid { get; private set; }
        QTestTestRun QTestTestRun;


        public override string URL
        {
            get
            {
                return String.Format($"api/v3/projects/{ProjectId}/test-runs?projectId={ProjectId}&parentId={ParentId}&parentType={ParentType}");
            }
            set { }
        }

        public QTestPostTestRun(HttpClient httpClient, QTestSystemTreeInfo selectedItem,string testRunName,int testCaseId)
        {
            this.HttpClient = httpClient;
            this.ProjectId = selectedItem.ProjectId;
            this.ParentId = selectedItem.QTestObject.Id;
            this.ParentType = selectedItem.ParentType;
            this.QTestTestRun = new QTestTestRun(testRunName, testCaseId);
            this.Pid = string.Empty;
           
        }
        public override string MessageHeader()
        {
            return "QTest post test run ";
        }

        public void PostTestRun()
        {
            var json = JsonSerializer.Serialize(QTestTestRun);
            var content = new StringContent(json, Encoding.UTF8, "application/json"); 
            var response = HttpClient.PostAsync(URL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<QTestRunPid>(responseContent, options);
                Pid = postResponse.Pid;
                Log.Info(MessageHeader()+$"test created name= \"{QTestTestRun.name}\""+ $" Pid= {postResponse.Pid} ");

            }
            else
            {
                Log.Info(MessageHeader() + response.StatusCode);
            }
        }
        public string GetTestRunPid()
        {
            return Pid;
        }
    }
}
