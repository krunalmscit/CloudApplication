

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public  class MonerisCheckOutComplete
    {

        public MonerisCheckOutComplete()
        {
            //Item item = new Item();
        }
        public string store_id { get; set; }
        public string api_token { get; set; }
        public string checkout_id { get; set; }
        public string integrator { get; set; }
        public string txn_total { get; set; }
        public string environment { get; set; }
        public string action { get; set; }
        public string order_no { get; set; }
        public string cust_id { get; set; }
        public string dynamic_descriptor { get; set; }
        public Cart cart { get; set; }
        public ContactDetails  contact_details { get; set; }
        public Shipping_Details shipping_details { get; set; }
        public Billing_Details billing_details { get; set; }
        public List<Shipping_Rates> shipping_rates { get; set; }
    }

    public class ContactDetails
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}