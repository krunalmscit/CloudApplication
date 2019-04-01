using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonerisDAL.App_Code.Cloud.IPGate
{
    public class PreAuthDetails
    {
        public string AuthCode { get; set; }
        public string Amount { get; set; }
        public string PAN { get; set; }
        //public DateTimeOffset? TransactionDateTime { get; set; }
        public string SynchronizationData { get; set; }
        public string encryptedTrack2 { get; set; }
        public string OriginalPreAuthAmount { get; set; }
        public string CardType { get; set; }
        public string AccoutnType { get; internal set; }
        public string Aid { get; internal set; }
        public string SwipeIndicator { get; internal set; }
        public string Tid { get; internal set; }
        public string TransactionDate { get; internal set; }
        public string TransactionTime { get; internal set; }
        public string APIToken { get; internal set; }
        public string MerchantId { get; internal set; }
    }
}
