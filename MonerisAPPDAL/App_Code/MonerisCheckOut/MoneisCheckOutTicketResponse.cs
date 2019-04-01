using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class MonerisCheckOutTicketResponse
    {
        public Response response { get; set; }
        public class Response
        {
            public string success { get; set; }
            public string ticket { get; set; }
            internal JSONError error { get; set; }
        }
       
}
}