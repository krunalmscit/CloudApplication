using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class IPGCloudRequest 
    {
        public string giftAndLoyaltyTerminalId { get; set; }

        public IPGCloudSyncDataForCompletion synchronizationData { get; set; }
        public string pairingToken { get; set; }
        public string amount { get; set; }
        public string entryMethod { get; set; }
        public string invoiceNumber { get; set; }
        public string echoData { get; set; }
        public string moto { get; set; }
        public string originalApprovalNumber { get; set; }
        public string cardType { get; set; }
        public string authCode { get; set; }
        public string originalPreauthAmount { get; set; }
        public string pan { get; set; }
        public string swipeIndicator { get; set; }
        public string accountType { get; set; }
        public string aid { get; set; }
        public string encryptedTrack2 { get; set; } 
        public string terminalId { get; set; }
        public string tipType { get; set; }
        public string mode { get; set; }
        public string cashbackLimit { get; set; }
        public string surchargeFee { get; set; }
        //public string originalApprovalNumber { get; set; }
        public string referenceNumber { get;  set; }
    }
}