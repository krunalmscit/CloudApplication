using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class CloudReceipt
    {
        public class Rootobject
        {
            public Receipt receipt { get; set; }
        }

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
            public String PartialAuthAmount { get; set; }
            public String AvailableBalance { get; set; }
            public String TipAmount { get; set; }
            public String EMVCashBackAmount { get; set; }
            public String SurchargeAmount { get; set; }
            public String ForeignCurrencyAmount { get; set; }
            public String ForeignCurrencyCode { get; set; }
            public String BaseRate { get; set; }
            public String ExchangeRate { get; set; }
            public string Pan { get; set; }
            public string CardType { get; set; }
            public string CardName { get; set; }
            public string AccountType { get; set; }
            public string SwipeIndicator { get; set; }
            public string FormFactor { get; set; }
            public string CvmIndicator { get; set; }
            public String ReservedField1 { get; set; }
            public String ReservedField2 { get; set; }
            public String ReservedField3 { get; set; }
            public String ReservedField4 { get; set; }
            public string AuthCode { get; set; }
            public String InvoiceNumber { get; set; }
            public String EMVEchoData { get; set; }          
            public string Aid { get; set; }
            public string AppLabel { get; set; }
            public string AppPreferredName { get; set; }
            public string Arqc { get; set; }
            public string TvrArqc { get; set; }
            public string Tcacc { get; set; }
            public string TvrTcacc { get; set; }
            public String Tsi { get; set; }
            public String TokenResponseCode { get; set; }
            public String Token { get; set; }
            public string LogonRequired { get; set; }
            public String EncryptedCardInfo { get; set; }
            public string TransDate { get; set; }
            public string TransTime { get; set; }
            public string Amount { get; set; }
            public string ReferenceNumber { get; set; }
            public string ReceiptId { get; set; }
            public string TransId { get; set; }
            public string TimedOut { get; set; }
            public string CloudTicket { get; set; }
            public string TxnName { get; set; }
            public string ErrorCode { get; set; }
            public string EncryptedTrack2 { get; set; }
            public string EncryptedPan { get; set; }
            public string IsoResponseCode { get; set; }
            public string BarCode { get; set; }
            public string BarCodeFormatInfo { get; set; }
            public IPGCloudSyncDataForCompletion synchronizationData { get; set; }

            public string BatchNumber { get; set; }
            public string HostPurchaseCount { get; set; }
            public string HostPurchaseTotal { get; set; }
            public string HostRefundCount { get; set; }
            public string HostRefundTotal { get; set; }
            public string HostCorrectionCount { get; set; }
            public string HostCorrectionTotal { get; set; }
            public string HostPaymentCount { get; set; }
            public string HostPaymentTotal { get; set; }
            public string HostPaymentCorrectionCount { get; set; }
            public string HostPaymentCorrectionTotal { get; set; }
            public string HostCreditCount { get; set; }
            public string DevicePurchaseCount { get; set; }
            public string DevicePurchaseTotal { get; set; }
            public string DeviceRefundCount { get; set; }
            public string DeviceRefundTotal { get; set; }
            public string DeviceCorrectionCount { get; set; }
            public string DeviceCorrectionTotal { get; set; }
            public string DevicePaymentCount { get; set; }
            public string DevicePaymentTotal { get; set; }
            public string DevicePaymentCorrectionCount { get; set; }
            public string DevicePaymentCorrectionTotal { get; set; }
            public string DeviceCreditCount { get; set; }
            public string MyPrTransDateoperty { get; set; }
            public BinRanges[] BinRanges { get; set; }
            public string Track1 { get; set; }
            public string Track2 { get; set; }
            public string Track3 { get; set; }
            public string EncryptedTrack1 { get; set; }
            //public string EncryptedTrack2 { get; set; }
            public string EncryptedTrack3 { get; set; }
            public string ConditionCode { get; set; }
        }


    }

    public class BinRanges
    {
        public string LowPrefix { get; set; }
        public string HighPrefix { get; set; }
    }
}