using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class Tax 
    {
        public Tax()
        {

        }
        public Tax(decimal total) 
        {
            this.amount = (total * (Convert.ToDecimal(rate) / 100)).ToString();
            //this.amount = ((Convert.ToDecimal() * Convert.ToInt32(this.rate))/100).ToString();
        }
        public string amount { get; set; }
        public string description { get; set; }
        public string rate { get; set; }
    }

}