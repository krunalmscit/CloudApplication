using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class Billing_Details
    {
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string same_as_shipping { get; set; }
    }
}
