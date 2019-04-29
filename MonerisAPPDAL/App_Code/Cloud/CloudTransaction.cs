

namespace MonerisDAL.App_Code
{
    public class CloudTransaction
    {   
        public string storeId { get; set; }
        public string apiToken { get; set; }
        public string txnType { get; set; }
        public string terminalId { get; set; }
        public Request request { get; set; }
        
        public class Request
        {
            public string orderId { get; set; }

            public string txnNumber { get; set; }

            public string amount { get; set; }

            public string pairingToken { get; set; }

            public string terminalId { get; set; }

            public string entryMethod { get; set; }

            public string setTip { get; set; }

            public string tipType { get; set; }
            public bool moto { get; set; }
            public Debit debit { get; set; }
            public Credit[] credit { get; set; }
            public string originalAuthCode { get; set; }
            public string echoData { get; set; }
            public string promoCode { get; set; }
            public string mode { get; set; }
            public string surchargeFee { get; set; }
            //public bool enable { get; set; }
            public string displayTimeout { get; set; }
            public string scanType { get; set; }
            public string promptText1 { get; set; }
            public string promptText2 { get; set; }
            public string promptText3 { get; set; }
            public string promptText4 { get; set; }
            public string lowPrefix { get; set; }
            public string highPrefix { get; set; }
            public string entryOptions { get; set; }
            public string track { get; set; }
            public bool enabled { get; set; }
            public string EntryMethod { get; set; }

            public class Debit
            {
                public string limit { get; set; }
            }

            public class Credit
            {
                public string cardPlan { get; set; }
                public string limit { get; set; }
            }

        }

        ////public List<Request> _request { get; set; }

        //public void addRequest(string orderId, string amount)
        //{
        //    Request r = new Request();
        //    r.Amount = amount;
        //    r.OrderId = orderId;
        //    _request.Add(r);
        //}

        //public async Task<CloudPoolingResponse.RootObject> PerfromTransaction(string URL)
        //{
        //    string request = JsonConvert.SerializeObject(this, Formatting.Indented);
        //    //jsonRequest = JsonConvert.SerializeObject(transaction, Formatting.Indented);

        //    string resultContent;
        //    CloudPoolingResponse.RootObject recpt = new CloudPoolingResponse.RootObject();

        //    var content = new StringContent(request, Encoding.UTF8, "application/json");
        //    try
        //    {
        //        using (var client = new System.Net.Http.HttpClient())
        //        {
        //            HttpResponseMessage result = await client.PostAsync(URL, content);
        //            resultContent = await result.Content.ReadAsStringAsync();

        //            recpt = JsonConvert.DeserializeObject<CloudPoolingResponse.RootObject>(resultContent);
        //            //Session["PoolRcpt"] = recpt;
        //            return recpt;
        //        }
        //    }
        //    catch (Exception clouse)
        //    {
        //        resultContent = clouse.InnerException.Message.ToString();
        //        return null;
        //    }
        //    finally {

        //    }
        //}
    }
}