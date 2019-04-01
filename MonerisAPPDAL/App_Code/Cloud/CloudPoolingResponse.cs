
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class CloudPoolingResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public string Completed { get; set; }
        public string Error { get; set; }
        public string TimedOut { get; set; }
        public string CloudTicket { get; set; }
        public string receiptUrl { get; set; }
        public class RootObject
        {
            public Receipt Receipt { get; set; }
        }
    }
}