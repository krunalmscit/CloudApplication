using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOnerisAPI.App_Code
{
    public class MonerisCheckoutReceiptRequest
    {  
            public string store_id { get; set; }
            public string api_token { get; set; }
            public string checkout_id { get; set; }
            public string integrator { get; set; }
            public string environment { get; set; }
            public string action { get; set; }
            public string ticket { get; set; }

    }
}