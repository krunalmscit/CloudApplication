using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class Cart
    {
        public Cart()
        {

            this.items = new List<Item>();
            //this.tax.amount = ((Convert.ToDecimal(this.subtotal) * Convert.ToDecimal(this.tax.rate)) / 100).ToString();

        }
        public List<Item> items { get; set; }
        public string subtotal { get; set; }
        public Tax tax { get; set; }
    }
}