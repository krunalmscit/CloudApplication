using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class MonerisCheckOut
    {
        public string store_id { get; set; }
        public string api_token { get; set; }
        public string checkout_id { get; set; }
        public string integrator{ get; set; }
        public string txn_total { get; set; }
        public string environment { get; set; }
        public string action{ get; set; }
        public string order_no{ get; set; }
        public string cust_id { get; set; }
        public string dynamic_descriptor { get; set; }
    }
}