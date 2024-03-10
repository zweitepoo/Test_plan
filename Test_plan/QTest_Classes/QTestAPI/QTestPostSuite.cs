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
    internal class QTestPostSuite : QTestPost
    {
        public int ParentId { get; set; }
        public string ParentType { get; set; }
        public QTestPostObject PostObject { get;private set; }
        public override string URL
        {
            get
            {
                return String.Format($"api/v3/projects/{ProjectId}/test-suites?projectId={ProjectId}&parentId={ParentId}&parentType={ParentType}");
            }
            set { }
        }

        public QTestPostSuite(HttpClient httpClient, QTestSystemTreeInfo selectedItem, string suiteName)
        {
            this.HttpClient = httpClient;
            this.ProjectId = selectedItem.ProjectId;
            this.ParentId = selectedItem.QTestObject.Id;
            this.ParentType = selectedItem.ParentType;
            PostObject = new QTestPostObject(ParentId, ParentType, suiteName);

        }
        public override string MessageHeader()
        {
            return "QTest post test cycle ";
        }

        public void PostSuite()
        {
            var json = JsonSerializer.Serialize(PostObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json"); 
            var response = HttpClient.PostAsync(URL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<QTestGetObject>(responseContent, options);
                Log.Info(MessageHeader()+ $"ID= {postResponse.Id} Name=  {postResponse.Name}  Pid= {postResponse.Pid} ");
            }
            else
            {
                Log.Info(MessageHeader() + response.StatusCode);
            }
        }
    }
}
