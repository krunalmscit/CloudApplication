using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code
{
    public class IPGCloudSyncDataForCompletion
    {
        public string PosEntryMode { get; set; }
        public string EmvRequestData { get; set; }
        public string EmvAdditionalData { get; set; }
        public string ChipConditionCode { get; set; }
        public string ReasonOnlineCode { get; set; }
        public string IssuerScriptResults { get; set; }
        public string IsoResponseCode { get; set; }
    }

    
}