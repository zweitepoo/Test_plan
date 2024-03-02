using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test_plan
{
    public class QTestClient
    {
        public  HttpClient Client { get;private set; }
        public string Token { get; set; }  
        public QTestClient (string httpProjectAddres)
        {
            Client = new HttpClient() { BaseAddress = new Uri(httpProjectAddres) };
        }
        public void SetBearerToken(string token)
        {
            Token = token;
            Client.DefaultRequestHeaders.Add("Authorization", Token);
        }
        
    }
}
