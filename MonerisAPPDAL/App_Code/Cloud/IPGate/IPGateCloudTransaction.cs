using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MonerisDAL.App_Code
{
    public class IPGateCloudTransaction 
    {
        public string merchantId { get; set; }
        public string terminalId { get; set; }
        public string txnType { get; set; }
        public IPGCloudRequest request = new IPGCloudRequest();


        public string apiToken { get; set; }
    }
}