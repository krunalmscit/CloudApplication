using MonerisDAL.App_Code;
using MonerisDAL.App_Code.DBHelper;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace CloudApplication.Cloud
{
    public partial class IPGate : System.Web.UI.Page
    {
        IPGateCloudTransaction transaction = new IPGateCloudTransaction();
        private static string postUrl = "http://ec2-52-73-55-162.compute-1.amazonaws.com/Terminal/"; // QA1, QA2
        //private static string postUrl = "https://ippostest.moneris.com/Terminal/";  // internal QA
        private static string jsonRequest = string.Empty;
        private static string resultContent = string.Empty;
        private CloudPoolingResponse.RootObject syncRecpt = new CloudPoolingResponse.RootObject();
        CloudReceipt.Rootobject cloudRece = new CloudReceipt.Rootobject();
        IPGateCloudDbRepository repo = new IPGateCloudDbRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            //transaction.merchantId = "0030103593019";
            //transaction.terminalId = "66011464";
            //transaction.merchantId = "0030128908135";
            //transaction.terminalId = "66020420";
            //transaction.apiToken = "ipgatetoken";

            if (!IsPostBack)
            {
                /// === Rich Pinpad ====
                //txtMerchantId.Text = "0030103593019";
                //txtTerminalID.Text = "66012612";
                //txtToken.Text = "richtoken";

                // For Pin Pad == 61099700
                txtMerchantId.Text = "0030128908135";
                txtTerminalID.Text = "66020420";
                txtToken.Text = "phettoken";
            }
        }


        protected void btnUnPair_Click(object sender, EventArgs e)
        {
            transaction.txnType = "unpair";
            performTransactionAsync();
        }

        protected void btnPair_Click(object sender, EventArgs e)
        {
            transaction.txnType = "pair";
            transaction.request.pairingToken = txtPairingToken.Text.Trim();
            performTransactionAsync();
        }

        protected void btnPreAuth_Click(object sender, EventArgs e)
        {
            transaction.txnType = "preauth";
            transaction.request.amount = txtAmount.Text.Trim();
            performTransactionAsync();
        }

        protected void btnPollingReceipt_Click(object sender, EventArgs e)
        {
            if (Session["poolRcpt"] != null)
            {
                var r = (CloudPoolingResponse.RootObject)Session["poolRcpt"];
                string URL = r.Receipt.receiptUrl;
                if (URL != null && !string.IsNullOrEmpty(URL))
                {
                    var resObject = (poolReceipt(URL));
                    txtAsyncResponse.Text = JsonConvert.SerializeObject(resObject, Formatting.Indented);
                    //repo.InsertDateToDB(resObject, txtRequest.Text, txtResposne.Text, txtAsyncResponse.Text, txtTestCase.Text, txtMerchantId.Text.Trim(), txtToken.Text.Trim());
                    //txtTxnNumber.Text = v.receipt.TransId;
                    Session["FollowOn"] = txtAsyncResponse.Text;
                    txtAmount.Text = "";
                    txtTestCase.Text = "";
                    txtSurcharge.Text = "";
                    txtCahsbackLimit.Text = "";
                    txtOriginalinvNumber.Text = "";
                    txtInvoice.Text = "";
                    rdEntryMethods.SelectedIndex = 0;
                    txtPairingToken.Text = "";
                    txtEchoData.Text = "";
                    HttpCookie myPollingCookie = new HttpCookie("PollingCookie");
                    myPollingCookie.Values["poolingReceipt"] = txtAsyncResponse.Text;
                    Response.Cookies.Add(myPollingCookie);
                }
                //btnSaveDB.Visible = true;
            }
        }

        protected void btnCapture_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["PollingCookie"];
            string followon = cookie != null ? cookie.Value.Split('=')[1] : "undifined";
            //transaction.txnType = "completion";
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);
                transaction.txnType = "completion";
                transaction.request.amount = txtAmount.Text.Trim();
                transaction.request.cardType = cloudRece.receipt.CardType;
                transaction.request.originalApprovalNumber = cloudRece.receipt.AuthCode;
                transaction.request.originalPreauthAmount = cloudRece.receipt.Amount;
                transaction.request.pan = cloudRece.receipt.Pan;
                transaction.request.swipeIndicator = cloudRece.receipt.SwipeIndicator;
                transaction.request.accountType = cloudRece.receipt.AccountType;
                transaction.request.aid = cloudRece.receipt.Aid;
                transaction.request.encryptedTrack2 = cloudRece.receipt.EncryptedTrack2;
                transaction.request.synchronizationData = cloudRece.receipt.synchronizationData;
                performTransactionAsync();
            }
        }

        protected void btnForcePost_Click(object sender, EventArgs e)
        {
            transaction.txnType = "forcePost";
            transaction.request.amount = txtAmount.Text.Trim();
            transaction.request.originalApprovalNumber = txtOriginalinvNumber.Text.Trim();
            performTransactionAsync();
        }

        protected void btnEnableCashBack_Click(object sender, EventArgs e)
        {
            transaction.txnType = "cashback";
            transaction.request.cashbackLimit = txtCahsbackLimit.Text.Trim();
            transaction.request.mode = drpMode.SelectedItem.Value.Trim();
            performTransactionAsync();
        }

        protected void btnSurcharge_Click(object sender, EventArgs e)
        {
            transaction.txnType = "surcharge";
            transaction.request.surchargeFee = txtSurcharge.Text.Trim();
            transaction.request.mode = drpSurchargeMode.SelectedItem.Value.Trim();
            performTransactionAsync();
        }

        protected void btnBalanceInquiry_Click(object sender, EventArgs e)
        {
            transaction.txnType = "balanceInquiry";
            transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.Trim();
            performTransactionAsync();
        }

        protected void btnOpenTotal_Click(object sender, EventArgs e)
        {
            transaction.txnType = "openTotal";
            performTransactionAsync();
        }

        protected void btnBatchCLose_Click(object sender, EventArgs e)
        {
            transaction.txnType = "batchClose";
            performTransactionAsync();
        }

        protected void btnPurchaseCorrections_Click(object sender, EventArgs e)
        {
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);
                transaction.txnType = "purchaseCorrection";
                transaction.request.amount = txtAmount.Text.Trim();
                transaction.request.originalApprovalNumber = cloudRece.receipt.AuthCode;
                performTransactionAsync();
            }
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            transaction.txnType = "refund";
            transaction.request.amount = txtAmount.Text;
            performTransactionAsync();
        }

        protected void btnRefundCorrections_Click(object sender, EventArgs e)
        {
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);
                transaction.txnType = "refundCorrection";
                transaction.request.amount = txtAmount.Text.Trim();
                transaction.request.originalApprovalNumber = cloudRece.receipt.AuthCode;
                performTransactionAsync();

            }
        }

        protected void brnSetTIp_Click(object sender, EventArgs e)
        {
            transaction.txnType = "setTip";
            if (drpSetTip.SelectedItem.Value == "TF")
            {
                transaction.request.tipType = "TF";
            }
            if (drpSetTip.SelectedItem.Value == "TB")
            {
                transaction.request.tipType = "TB";
            }
            if (drpSetTip.SelectedItem.Value == "TC")
            {
                transaction.request.tipType = "TC";
            }
            if (drpSetTip.SelectedItem.Value == "TH")
            {
                transaction.request.tipType = "TH";
            }
            performTransactionAsync();
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            transaction.txnType = "purchase";
            transaction.request.amount = txtAmount.Text.Trim();
            performTransactionAsync();
        }

        protected void btnGiftInit_Click(object sender, EventArgs e)
        {
            transaction.txnType = "giftAndLoyaltyInitialization";
            transaction.request.giftAndLoyaltyTerminalId = "36314";
            transaction.request.mode = drpGiftMode.SelectedItem.Value;
            performTransactionAsync();

        }

        protected void btnGPurchase_Click(object sender, EventArgs e)
        {
            transaction.txnType = "giftPurchase";
            transaction.request.amount = txtGLAmount.Text.Trim();
            performTransactionAsync();
        }

        protected void btnGRefund_Click(object sender, EventArgs e)
        {
            SetupGiftFollowon();
            transaction.txnType = "giftRefund";
            performTransactionAsync();
        }

        protected void btnGVoid_Click(object sender, EventArgs e)
        {
            SetupGiftFollowon();
            transaction.txnType = "giftVoid";
            performTransactionAsync();
        }

        protected void btnLRefund_Click(object sender, EventArgs e)
        {
            transaction.txnType = "loyaltyRefund";
            transaction.request.amount = txtGLAmount.Text.Trim();
            performTransactionAsync();
        }

        protected void btnLVoid_Click(object sender, EventArgs e)
        {
            transaction.txnType = "loyaltyVoid";
            SetupGiftFollowon();
            performTransactionAsync();
        }

        protected void btnLPurchase_Click(object sender, EventArgs e)
        {
            transaction.txnType = "loyaltyPurchase";
            transaction.request.amount = txtGLAmount.Text.Trim();
            performTransactionAsync();

        }

        // ==== Helper methods

        private async void performTransactionAsync()
        {
            if (drpMoto.SelectedIndex > 0)
            {
                transaction.request.moto = "true";
            }
            if (!String.IsNullOrEmpty(txtEchoData.Text.Trim()))
            {
                transaction.request.echoData = txtEchoData.Text.Trim();
            }
            SetEntryMethod();
            transaction.merchantId = txtMerchantId.Text.Trim();
            transaction.terminalId = txtTerminalID.Text.Trim();
            transaction.apiToken = txtToken.Text.Trim();
            jsonRequest = JsonConvert.SerializeObject(transaction, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            txtRequest.Text = jsonRequest;
            lblReqUrl.Text = postUrl;
            lblTestCase.Text = txtTestCase.Text;
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            if (!string.IsNullOrEmpty(txtAsyncResponse.Text))
            {
                txtAsyncResponse.Text = ""; //btnSaveDB.Visible = false; lblDbSave.Text = ""; 
            }
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage result = await client.PostAsync(postUrl, content);
                    resultContent = await result.Content.ReadAsStringAsync();
                    syncRecpt = JsonConvert.DeserializeObject<CloudPoolingResponse.RootObject>(resultContent);
                    txtResposne.Text = JsonConvert.SerializeObject(syncRecpt, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    HttpCookie myCookie = new HttpCookie("TransactionCookie");
                    //myCookie.Values["Request"] = lblRequest.Text;
                    myCookie.Values["Request"] = txtRequest.Text;
                    myCookie.Values["SyncResponse"] = syncRecpt.ToString();
                    Response.Cookies.Add(myCookie);
                    Session["PoolRcpt"] = syncRecpt;
                }
            }
            catch (Exception clouse)
            {

                resultContent = clouse.InnerException.Message.ToString();
            }
            finally
            {

            }
        }

        public CloudReceipt.Rootobject poolReceipt(string URL)
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (!string.IsNullOrEmpty(responseString))
            {
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(responseString);
            }
            return cloudRece;
        }

        private void SetEntryMethod()
        {
            if (rdEntryMethods.SelectedItem.Value == "N/A")
            {
                transaction.request.entryMethod = null;
            }
            if (rdEntryMethods.SelectedItem.Value == "B")
            {
                transaction.request.entryMethod = "B";
            }
            if (rdEntryMethods.SelectedItem.Value == "M")
            {
                transaction.request.entryMethod = "M";
            }
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                transaction.request.invoiceNumber = txtInvoice.Text.Trim();
            }
        }

        private void SetupGiftFollowon()
        {
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);
                transaction.request.amount = txtGLAmount.Text.Trim();
                transaction.request.referenceNumber = cloudRece.receipt.ReferenceNumber;
            }
        }
    }
}