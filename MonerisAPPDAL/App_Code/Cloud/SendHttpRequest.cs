using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonerisDAL.App_Code.Cloud
{
    public class SendHttpRequest
    {
        private static string postUrl = "http://ec2-52-73-55-162.compute-1.amazonaws.com/Terminal/"; // QA1, QA2
        private static string postUrlIntranlQA = "https://ippostest.moneris.com/Terminal/";  // internal QA
        public async Task<string>  PerformTransaction(IPGateCloudTransaction transaction,string environment)
        {
            string Url = string.Empty;
            if (environment.Trim() == "QA1" || environment.Trim() == "QA2")
            {
                Url = postUrl;
            }
            else if (environment.Trim().ToUpper() == "INTERNAL QA")
            {
                Url = postUrlIntranlQA;
            }
            string jsonRequest = JsonConvert.SerializeObject(transaction, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            string resultContent;
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage result = await client.PostAsync(Url, content);
                    resultContent = await result.Content.ReadAsStringAsync();
                   CloudPoolingResponse.RootObject syncRecpt = JsonConvert.DeserializeObject<CloudPoolingResponse.RootObject>(resultContent);
                    string syncResponse = JsonConvert.SerializeObject(syncRecpt, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
            }
            catch (Exception clouse)
            {
                resultContent = clouse.InnerException.Message.ToString();
            }
            finally
            {
               
            }
            return resultContent;
        }


        public CloudReceipt.Rootobject poolReceipt(string URL)
        {
            CloudReceipt.Rootobject cloudRece = new CloudReceipt.Rootobject();
            var request = (HttpWebRequest)WebRequest.Create(URL);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (!string.IsNullOrEmpty(responseString))
            {
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(responseString);
            }
            else
            {
                cloudRece = null;
            }
            return cloudRece;
        }
    }
}
