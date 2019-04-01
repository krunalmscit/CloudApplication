using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class Receipt
    {
        public string Completed { get; set; }
        public string TransType { get; set; }
        public string Error { get; set; }
        public string InitRequired { get; set; }
        public string SafIndicator { get; set; }
        public string ResponseCode { get; set; }
        public string ISO { get; set; }
        public string LanguageCode { get; set; }
        public object PartialAuthAmount { get; set; }
        public object AvailableBalance { get; set; }
        public object TipAmount { get; set; }
        public object EMVCashBackAmount { get; set; }
        public object SurchargeAmount { get; set; }
        public object ForeignCurrencyAmount { get; set; }
        public object ForeignCurrencyCode { get; set; }
        public object BaseRate { get; set; }
        public object ExchangeRate { get; set; }
        public string Pan { get; set; }
        public string CardType { get; set; }
        public string CardName { get; set; }
        public string AccountType { get; set; }
        public string SwipeIndicator { get; set; }
        public string FormFactor { get; set; }
        public string CvmIndicator { get; set; }
        public object ReservedField1 { get; set; }
        public object ReservedField2 { get; set; }
        public string AuthCode { get; set; }
        public object InvoiceNumber { get; set; }
        public object EMVEchoData { get; set; }
        public object ReservedField3 { get; set; }
        public object ReservedField4 { get; set; }
        public string Aid { get; set; }
        public string AppLabel { get; set; }
        public object AppPreferredName { get; set; }
        public string Arqc { get; set; }
        public string TvrArqc { get; set; }
        public string Tcacc { get; set; }
        public string TvrTcacc { get; set; }
        public object Tsi { get; set; }
        public object TokenResponseCode { get; set; }
        public object Token { get; set; }
        public string LogonRequired { get; set; }
        public object EncryptedCardInfo { get; set; }
        public string TransDate { get; set; }
        public string TransTime { get; set; }
        public string Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public string ReceiptId { get; set; }
        public string TransId { get; set; }
        public string TimedOut { get; set; }
        public string CloudTicket { get; set; }
        public string TxnName { get; set; }
        public string receiptUrl { get; set; }
        public int MyProperty { get; set; }
      
    }
}