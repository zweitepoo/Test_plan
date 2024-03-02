using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    internal class QTestGetReleases : QTestGet
    {
        public override string URL {
            get
            {
                return String.Format($"api/v3/projects/{ProjectId}/releases?projectId={ProjectId}");
            }
            set { } }
       
        public QTestGetReleases(HttpClient httpClient, int projectId) 
        {
            this.HttpClient = httpClient;
            this.ProjectId = projectId;
        }
        public override string ErrorMessageHeader() 
        {
            return "QTest Get Releases ";
        }
    }
}
