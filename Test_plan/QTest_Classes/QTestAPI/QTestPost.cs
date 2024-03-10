using CefSharp.DevTools.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public abstract  class QTestPost
    {
        public abstract string URL { get; set; }

        public HttpClient HttpClient;
        public int ProjectId { get; set; }
        public abstract string MessageHeader();

      
    }
}
