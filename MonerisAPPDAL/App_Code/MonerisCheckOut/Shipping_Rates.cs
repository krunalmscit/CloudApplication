using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class Shipping_Rates
    {
        public string code { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string txn_taxes { get; set; }
        public string txn_total { get; set; }
        public string default_rate { get; set; }
    }
}