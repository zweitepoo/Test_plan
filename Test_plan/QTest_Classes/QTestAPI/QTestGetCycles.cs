using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    internal class QTestGetCycles : QTestGet
    {
        public int ParentId {  get; set; }
        public string ParentType { get; set; }
        public override string URL {
            get
            {
                return String.Format($"api/v3/projects/{ProjectId}/test-cycles?parentId={ParentId}&parentType={ParentType}");
            }
            set { } }
       
        public QTestGetCycles(HttpClient httpClient, int projectId, int parentId, string parentType) 
        {
            this.HttpClient = httpClient;
            this.ProjectId = projectId;
            this.ParentId = parentId;
            this.ParentType = parentType;
            
        }
        public override string MessageHeader() 
        {
            return "QTest get test cycles ";
        }
    }
}
