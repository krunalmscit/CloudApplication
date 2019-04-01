using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MonerisDAL.App_Code
{
    public class MonerisCheckoutReceiptResponse
    {
        public Response response { get; set; }
        public class Response
        {
            public string success { get; set; }
            public Request request { get; set; }
            public Receipt receipt { get; set; }
        }
        public class Request
        {
            public string txn_total { get; set; }
            public Cust_Info cust_info { get; set; }
            public Shipping shipping { get; set; }
            public Billing_Details billing { get; set; }
            public string cc_total { get; set; }
            public CreditCardInfo cc { get; set; }
            public string ticket { get; set; }
            public string cust_id { get; set; }
            public string dynamic_descriptor { get; set; }
            public string order_no { get; set; }
            public string eci { get; set; }
        }
        public class Cust_Info
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
        }
        public class Shipping
        {
            public string address_1 { get; set; }
            public string address_2 { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string province { get; set; }
            public string postal_code { get; set; }
        }
        public class CreditCardInfo
        {
            public string first6last4 { get; set; }
            public string expiry { get; set; }
            public string cardholder { get; set; }
        }
        public class Receipt
        {
            public string result { get; set; }
            public ReceiptDetails cc { get; set; }
        }
        public class ReceiptDetails
        {
            public string order_no { get; set; }
            public string cust_id { get; set; }
            public string transaction_no { get; set; }
            public string reference_no { get; set; }
            public string transaction_code { get; set; }
            public string transaction_type { get; set; }
            public string transaction_date_time { get; set; }
            public string corporateCard { get; set; }
            public string amount { get; set; }
            public string response_code { get; set; }
            public string iso_response_code { get; set; }
            public string approval_code { get; set; }
            public string card_type { get; set; }
            public string dynamic_descriptor { get; set; }
            public string invoice_number { get; set; }
            public string customer_code { get; set; }
            public string eci { get; set; }
            public string cvd_result_code { get; set; }
            public string avs_result_code { get; set; }
            public string first4last4 { get; set; }
            public string expiry_date { get; set; }
            public string recur_success { get; set; }
            public string ecr_no { get; set; }
            public string batch_no { get; set; }
            public string sequence_no { get; set; }
            public string result { get; set; }
        }
    }
}