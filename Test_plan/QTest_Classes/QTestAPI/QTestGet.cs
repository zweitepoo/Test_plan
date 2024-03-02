using CefSharp.DevTools.DOM;
using CefSharp.DevTools.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;



namespace Test_plan
{
    public abstract class QTestGet
    {
        public abstract string URL { get; set; }

        public  HttpClient HttpClient;
        public int ProjectId { get; set; }
        public abstract string ErrorMessageHeader();
        public virtual List<QTestGetObject> GetResponse()
        {
            List<QTestGetObject> tempList = new List<QTestGetObject>();

            var result = GetResult();
            if (result == null) 
            {
                tempList.Clear();
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var response = JsonSerializer.Deserialize<List<QTestGetObject>>(result, options);
                tempList.AddRange(response);
            }
            return tempList;
        }      

        public virtual string GetResult()
        {
            string getResult;
            var result = HttpClient.GetAsync(URL).Result;
            if (result.IsSuccessStatusCode)
            {
               getResult = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Log.Info( ErrorMessageHeader() + " error status code" + result.StatusCode.ToString());
                getResult = null;
            }
            return getResult;
        }


    }
}
