using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_plan.QTest_Classes.QTestAPI;

namespace Test_plan
{
    internal class QTestGetRuns : QTestGet
    {
        public int ParentId {  get; set; }
        public string ParentType { get; set; }
        public override string URL {
            get
            {
                return String.Format($"api/v3/projects/{ProjectId}/test-runs?parentId={ParentId}&parentType={ParentType}");
            }
            set { } }
       
        public QTestGetRuns(HttpClient httpClient, int projectId, int parentId, string parentType) 
        {
            this.HttpClient = httpClient;
            this.ProjectId = projectId;
            this.ParentId = parentId;
            this.ParentType = parentType;
            
        }

        public override List<QTestGetObject> GetResponse()
        {
           
            List<QTestGetObject> retList = new List<QTestGetObject>();

            var result = GetResult();
            Console.WriteLine(result);
            if (result == null)
            {
                retList.Clear();
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var response = JsonSerializer.Deserialize<QTestGetItems>(result, options);
                foreach ( var item in response.items ) 
                {
                    retList.Add(item);
                } 
            }
            return retList;
        }
        public override string MessageHeader() 
        {
            return "QTest get test runs ";
        }
    }
}
