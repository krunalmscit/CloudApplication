using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class Prodcut
    {
        public string ItemDesc{get; set;}
        public string itemCode{get; set;}
        public string ext_amount{get; set;}

        public Prodcut(string desc, string code, string amount)
        {
            ItemDesc = desc;
            itemCode = code;
            ext_amount = amount;
        }
    }
}