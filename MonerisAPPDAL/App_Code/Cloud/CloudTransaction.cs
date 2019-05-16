

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
            private string _orderId;
            public string orderId
            {
                get { return this._orderId; }
                set { this._orderId = value; }
            }

            private string _txnNumber;

            public string txnNumber
            {
                get { return _txnNumber;}
                set { _txnNumber = value; }
            }

            private string _amount;

            public string amount
            {
                get { return _amount; }
                set { _amount = value; }
            }

            private string _pairingToken;

            public string pairingToken
            {
                get { return _pairingToken; }
                set { _pairingToken = value; }
            }

            private string _terminalId;

            public string terminalId
            {
                get { return _terminalId; }
                set { _terminalId = value; }
            }

            private string _entryMethod;

            public string entryMethod
            {
                get { return _entryMethod; }
                set { _entryMethod = value; }
            }


            private string _setTip;

            public string setTip
            {
                get { return _setTip; }
                set { _setTip = value; }
            }

            private string _tipType;
            //public int monerisToken;
            public string monerisToken;

            public string tipType
            {
                get { return _tipType; }
                set { _tipType = value; }
            }

            
            public bool? moto { get; set; }
            public Debit debit { get; set; }
            public Credit[] credit { get; set; }
            public string originalApprovalNumber { get; set; }
            public string echoData { get; set; }
            public string promoCode { get; set; }
            //public string mode { get; set; }
            //public string surchargeFee { get; set; }
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
            public bool? enabled { get; set; }
            public string token { get; set; }
            public string predefinedPercentage1 { get; set; }
            public string predefinedPercentage2 { get; set; }
            public string predefinedPercentage3 { get; set; }
            public string tipWarningThreshold { get; set; }
            public string enableSurcharge { get; set; }
            public string surchargeFeeOnInterac { get; set; }
            public string surchargeFeeOnInteracCashback { get; set; }
            public string thresholdLimit { get; set; }
            public string enableCashback { get; set; }
            public string predefinedAmount1 { get; set; }
            public string predefinedAmount2 { get; set; }
            public string predefinedAmount3 { get; set; }
            public string allowCashbackCustomEntry { get; set; }
            public string interacCashbackLimit { get; set; }
            public string visaCashbackLimit { get; set; }
            public string mastercardCashbackLimit { get; set; }

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