using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test_plan
{
    internal class QTestGetTestCaseId : QTestGet
    {
       
        public string TestCasePid { get; set; }
        public override string URL {
            get
            {
                return String.Format($"api/v3/projects/{ProjectId}/test-cases/{TestCasePid}?projectId={ProjectId}&testCaseIdOrPid={TestCasePid}");
            }
            set { } }
       
        public QTestGetTestCaseId(HttpClient httpClient, int projectId, string testCasePid) 
        {
            this.HttpClient = httpClient;
            this.ProjectId = projectId;
            this.TestCasePid = testCasePid;
        }
        public override string MessageHeader() 
        {
            return "QTest get test case Id ";
        }

        public virtual int GetTestCaseId()
        {
            QTestGetObject tempObject = new QTestGetObject();

            var result = GetResult();
            if (result == null)
            {
                tempObject = null;
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var response = JsonSerializer.Deserialize<QTestGetObject>(result, options);
                tempObject = response;
            }
            return tempObject.Id;
        }
        public void SetTestCasePid(string testCasePid)
        {
            TestCasePid = testCasePid;
        }
    }
}
