using MonerisAPPDAL.App_Code.DBHelper;
using MonerisDAL.App_Code;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplication.Cloud
{
    public partial class Gateway : System.Web.UI.Page
    {
        /*Add Some Comment */
        private static string postUrl = "http://ec2-52-73-55-162.compute-1.amazonaws.com/Terminal/"; // QA1, QA2
        private static string jsonRequest = string.Empty;
        private static string resultContent = string.Empty;
        CloudTransaction transaction = new CloudTransaction();
        private CloudPoolingResponse.RootObject syncRecpt = new CloudPoolingResponse.RootObject();
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["awsInternalDB"].ToString();
        CloudReceipt.Rootobject cloudRece = new CloudReceipt.Rootobject();
        GatewayDbRespository db = new GatewayDbRespository();


        protected void Page_Load(object sender, EventArgs e)
        {
            //txtStoreID.Text = "monca00597";
            //txtAPIToken.Text = "7Xq0zhMcaVKBCkAV4rX5";
            //txtTerminalId.Text = "E1194378";

            if (!IsPostBack)
            {
                //btnSaveDB.Visible = false;
                //lblDbSave.Visible = false;
                lblFollowOn.Visible = false;
                if (Request.QueryString != null)
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["orderId"]))
                        txtOrderID.Text = Request.QueryString["orderId"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["txnNumber"]))
                        txtTxnNumber.Text = Request.QueryString["txnNumber"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["storeId"]))
                        txtStoreID.Text = Request.QueryString["storeId"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["apiToken"]))
                        txtAPIToken.Text = Request.QueryString["apiToken"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["terminalId"]))
                        txtTerminalId.Text = Request.QueryString["terminalId"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["amount"]))
                        txtAmount.Text = Request.QueryString["amount"].ToString().Trim();
                }
            }

        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "purchase";
            transaction.request = new CloudTransaction.Request()
            {
                amount = txtAmount.Text.ToString().Trim(),
                echoData = txtEchoData.Text.Trim()
            };
            if (drpSetTip.SelectedItem.Value != "0")
            {
                transaction.request.setTip = drpSetTip.SelectedItem.Value.ToString();
            }
            if (string.IsNullOrEmpty(txtOrderID.Text.Trim().ToString()))
            {
                transaction.request.orderId = "Test_KP_Purchase" + System.DateTime.Now.TimeOfDay.ToString();
            }
            else
            {
                transaction.request.orderId = txtOrderID.Text.Trim().ToString();
            }
            if (drpEntryMethod.SelectedItem.Value != "A")
            {
                transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.ToString();
            }
            performTransactionAsync();
        }

        protected void btnPreauth_Click(object sender, EventArgs e)
        {

            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "preauth";
            transaction.request = new CloudTransaction.Request()
            {
                //orderId = "Test_KP_PreAuth" + System.DateTime.Now.TimeOfDay.ToString(),
                //orderId = txtOrderID.Text.Trim(),
                amount = txtAmount.Text.Trim(),
                echoData = txtEchoData.Text.Trim()
            };

            if (string.IsNullOrEmpty(txtOrderID.Text.Trim().ToString()))
            {
                transaction.request.orderId = "Test_KP_PreAuth" + System.DateTime.Now.TimeOfDay.ToString();
            }
            else
            {
                transaction.request.orderId = txtOrderID.Text.Trim().ToString();
            }
            if (drpEntryMethod.SelectedItem.Value != "A")
            {
                transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.ToString();
            }
            performTransactionAsync();

        }

        protected void btnCompletion_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);
                transaction.txnType = "completion";
                transaction.request = new CloudTransaction.Request()
                {
                    // Change request to send a diffrent order ID in completion
                    //orderId = cloudRece.receipt.ReceiptId,
                    orderId = txtOrderID.Text.Trim(),
                    amount = txtAmount.Text.ToString().Trim(),
                    txnNumber = cloudRece.receipt.TransId,
                    echoData = txtEchoData.Text.Trim()
                };
                performTransactionAsync();
            }
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            //  CloudReceipt.Rootobject cloudRece = new CloudReceipt.Rootobject();
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);
                transaction.txnType = "refund";
                transaction.request = new CloudTransaction.Request()
                {
                    //orderId = cloudRece.receipt.ReceiptId,
                    orderId = txtOrderID.Text.Trim(),
                    amount = txtAmount.Text.ToString().Trim(),
                    txnNumber = cloudRece.receipt.TransId
                };
                if (drpEntryMethod.SelectedItem.Value != "A")
                {
                    transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.ToString();
                }
                performTransactionAsync();
            }
        }

        protected void btnVoid_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            if (Session["FollowOn"] != null)
            {
                string s = Session["FollowOn"].ToString();
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(s);

                transaction.txnType = "purchaseCorrection";
                transaction.request = new CloudTransaction.Request()
                {
                    //orderId = cloudRece.receipt.ReceiptId,
                    orderId = txtOrderID.Text.Trim(),
                    amount = txtAmount.Text.ToString().Trim(),
                    txnNumber = cloudRece.receipt.TransId,
                    echoData = txtEchoData.Text.Trim()
                };
                if (drpEntryMethod.SelectedItem.Value != "A")
                {
                    transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.ToString();
                }

                /// Need to add echo date


                performTransactionAsync();
            }
        }

        protected void btnIndRefund_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "refund";
            transaction.request = new CloudTransaction.Request()
            {
                amount = txtAmount.Text.ToString().Trim()
            };

            if (string.IsNullOrEmpty(txtOrderID.Text.Trim().ToString()))
            {
                transaction.request.orderId = "Test_KP_IndRrefund" + System.DateTime.Now.TimeOfDay.ToString();
            }
            else
            {
                transaction.request.orderId = txtOrderID.Text.Trim().ToString();
            }
            if (drpEntryMethod.SelectedItem.Value != "A")
            {
                transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.ToString();
            }
            performTransactionAsync();
        }

        protected void btnFpost_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "forcePost";

            transaction.request = new CloudTransaction.Request()
            {
                amount = txtAmount.Text.Trim(),
                originalApprovalNumber = txtOriginalInvNumber.Text.Trim(),
                echoData = "",
                promoCode = ""
            };
            if (drpEntryMethod.SelectedIndex != 0)
                transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value;

            if (string.IsNullOrEmpty(txtOrderID.Text.Trim().ToString()))
            {
                transaction.request.orderId = "Test_KP_ForcePost" + System.DateTime.Now.TimeOfDay.ToString();
            }
            else
            {
                transaction.request.orderId = txtOrderID.Text.Trim().ToString();
            }
            performTransactionAsync();
        }

        protected void btnPairing_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "pair";
            //transaction.terminalId = txtTerminalId.SelectedItem.Text.ToString();
            transaction.request = new CloudTransaction.Request()
            {
                pairingToken = txtPairingToken.Text.Trim().ToString()
            };
            performTransactionAsync();

        }

        protected void btnunpair_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "unpair";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {

            };
            performTransactionAsync();
        }

        protected void btnSetTip_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "setTip";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {
                tipType = drpSetTip.SelectedItem.Value
            };
            performTransactionAsync();
        }

        protected void btnBatchClose_Click(object sender, EventArgs e)
        {
            transaction.txnType = "batchClose";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {

            };
            performTransactionAsync();
        }

        protected void btnOpenTotal_Click(object sender, EventArgs e)
        {
            transaction.txnType = "openTotal";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {

            };
            performTransactionAsync();
        }

        protected void btnInit_Click(object sender, EventArgs e)
        {
            transaction.txnType = "initialization";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {

            };
            performTransactionAsync();
        }

        protected void btnCardRead_Click(object sender, EventArgs e)
        {
            transaction.txnType = "cardRead";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {

            };
            performTransactionAsync();
        }

        protected void btnCashback_Click(object sender, EventArgs e)
        {
            transaction.txnType = "cashback";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {
                mode = drpCashbackMode.SelectedItem.Value,
                debit = new CloudTransaction.Request.Debit
                {
                    limit = txtDebit.Text.Trim()
                },
                credit = new CloudTransaction.Request.Credit[] {
                    new CloudTransaction.Request.Credit() {  cardPlan ="V", limit = txtVisaCashBackLimit.Text.Trim() },
                    new CloudTransaction.Request.Credit() { cardPlan = "M", limit = txtMCCashBackLimit.Text.Trim()}
                }
            };
            performTransactionAsync();
        }

        protected void btnSurcharge_Click(object sender, EventArgs e)
        {
            transaction.txnType = "surcharge";
            transaction.request = new CloudTransaction.Request()
            {
                surchargeFee = txtSurcharge.Text.Trim(),
                mode = drpSurcharge.SelectedItem.Value
            };
            performTransactionAsync();
        }

        protected void btnBarCodeScan_Click(object sender, EventArgs e)
        {
            transaction.txnType = "scanBarcode";
            transaction.request = new CloudTransaction.Request()
            {
                displayTimeout = txtdisplayTimeOut.Text.Trim(),
                scanType = txtScanType.Text.Trim(),
                promptText1 = txtPrompt1.Text.Trim(),
                promptText2 = txtPrompt2.Text.Trim(),
                promptText3 = txtPrompt3.Text.Trim(),
                promptText4 = txtPrompt4.Text.Trim()
            };
            performTransactionAsync();
        }

        protected void btnPool_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["poolRcpt"] != null)
                {
                    CloudPoolingResponse.RootObject r = (CloudPoolingResponse.RootObject)Session["poolRcpt"];
                    string URL = r.Receipt.receiptUrl;
                    if (URL != null && !string.IsNullOrEmpty(URL))
                    {
                        txtPollingReceipt.Text = JsonConvert.SerializeObject(poolReceipt(URL)
                                                            , Formatting.Indented
                                                            , new JsonSerializerSettings
                                                            {
                                                                NullValueHandling = NullValueHandling.Ignore
                                                            });
                        CloudReceipt.Rootobject v = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(txtPollingReceipt.Text.Trim());
                        //txtTxnNumber.Text = v.receipt.TransId;
                        Session["FollowOn"] = txtPollingReceipt.Text;
                    }
                    //btnSaveDB.Visible = true;
                }

                CloudReceipt.Rootobject cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(txtPollingReceipt.Text.Trim());
                int i = Request.QueryString.AllKeys.Count();
                if (Request.QueryString.AllKeys.Count() == 0)
                {
                    if (cloudRece != null && (cloudRece.receipt.TxnName.ToLower().Trim() == "purchase" || cloudRece.receipt.TxnName.ToLower().Trim() == "preauth"))
                    {
                        lblFollowOn.Visible = true;
                        lblFollowOn.Text = Request.Url.ToString() + "?orderid=" + cloudRece.receipt.ReceiptId + "&txnNumber=" + cloudRece.receipt.TransId + "&storeId=" + txtStoreID.Text.Trim() + "&apiToken=" + txtAPIToken.Text.Trim() + "&terminalId=" + txtTerminalId.Text.Trim() + "&amount=" + cloudRece.receipt.Amount;

                    }
                    else { lblFollowOn.Visible = false; }
                }
                else
                {
                    lblFollowOn.Text = "";
                }
                if (!string.IsNullOrEmpty(txtRequest.Text) && !string.IsNullOrEmpty(txtRespose.Text) && !string.IsNullOrEmpty(txtPollingReceipt.Text))
                    db.SaveToDb(txtRequest.Text.Trim(), txtRespose.Text.Trim(), txtPollingReceipt.Text.Trim());
            }


            catch
            {

            }

        }

        protected void btnSaveDB_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            bool success = false;
            try
            {
                success = db.SaveToDb(txtRequest.Text.Trim(), txtRespose.Text.Trim(), txtPollingReceipt.Text.Trim());
            }
            catch (Exception ex)
            {
                s = ex.InnerException.Message;
            }
            //if (success)
            //{
            //    lblDbSave.Visible = true;
            //    lblDbSave.Text = "Data Saved.";
            //}
            //else
            //{
            //    lblDbSave.Visible = true;
            //    lblDbSave.Text = s;
            //}
        }

        protected void drpSurcharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSurcharge.SelectedItem.Value == "SD")
                txtSurcharge.Enabled = false;
            else
                txtSurcharge.Enabled = true;
        }
        protected void btnBalanceInquiery_Click(object sender, EventArgs e)
        {
            transaction.txnType = "balanceInquiry";
            transaction.request = new CloudTransaction.Request()
            {
                entryMethod = drpEntryMethod.SelectedItem.Value
            };
            performTransactionAsync();
        }

        protected void btnMPOS_Click(object sender, EventArgs e)
        {
            transaction.txnType = "mobilePosSetting";
            if (chkmPOS.Checked)
                transaction.request = new CloudTransaction.Request() { enabled = true };
            else
                transaction.request = new CloudTransaction.Request() { enabled = false };
            performTransactionAsync();
        }
        protected void btnPanMask_Click(object sender, EventArgs e)
        {
            transaction.txnType = "panMaskingSetting";
            if (chkPanMask.Checked)
                transaction.request = new CloudTransaction.Request() { enabled = true };
            else
                transaction.request = new CloudTransaction.Request() { enabled = false };

            performTransactionAsync();
        }


        protected void btnaddBinRnage_Click(object sender, EventArgs e)
        {
            transaction.txnType = "addGiftBinRange";
            transaction.request = new CloudTransaction.Request()
            {
                lowPrefix = txtLowPrefix.Text.Trim(),
                highPrefix = txtHighPrefix.Text.Trim()
            };
            performTransactionAsync();
        }

        protected void btnDeleteRange_Click(object sender, EventArgs e)
        {
            transaction.txnType = "deleteGiftBinRange";
            transaction.request = new CloudTransaction.Request()
            {
                lowPrefix = txtDelLowRange.Text.Trim(),
                highPrefix = txtDelHighRange.Text.Trim()
            };
            performTransactionAsync();
        }

        protected void btnGiftCardRead_Click(object sender, EventArgs e)
        {
            transaction.txnType = "giftCardRead";
            transaction.request = new CloudTransaction.Request()
            {
                entryOptions = txtEntryOption.Text.Trim(),
                track = txtTrack.Text.Trim()
            };
            performTransactionAsync();
        }

        protected void btnGetBinRange_Click(object sender, EventArgs e)
        {
            transaction.txnType = "getGiftBinRanges";
            transaction.request = new CloudTransaction.Request();
            performTransactionAsync();
        }

        /// Helper Methods
        /// 
        private async void performTransactionAsync()
        {
            if (lblFollowOn.Visible == true)
                lblFollowOn.Visible = false;
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            if (!string.IsNullOrEmpty(txtEchoData.Text))
                transaction.request.echoData = txtEchoData.Text.Trim();

            if (rdEnv.SelectedItem.Value == "0")
                postUrl = "http://ec2-52-73-55-162.compute-1.amazonaws.com/Terminal/";

            if (rdEnv.SelectedItem.Value == "1")
                postUrl = "https://ippostest.moneris.com/Terminal";

            if (chkMoto.Checked)
                transaction.request.moto = true;

            txtRequest.Text = JsonConvert.SerializeObject(
                                            transaction,
                                            Formatting.Indented,
                                            new JsonSerializerSettings
                                            {
                                                NullValueHandling = NullValueHandling.Ignore
                                            });
            jsonRequest = JsonConvert.SerializeObject(transaction, Formatting.Indented);
            lblRequestUrl.Text = postUrl;
            StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            if (!string.IsNullOrEmpty(txtPollingReceipt.Text))
            {
                txtPollingReceipt.Text = "";
                //btnSaveDB.Visible = false;
                //lblDbSave.Text = ""; 
            }
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage result = await client.PostAsync(postUrl, content);
                    resultContent = await result.Content.ReadAsStringAsync();
                    //lblRes.Text = Server.HtmlDecode("<pre>"+ resultContent +"</pre>");
                    txtRespose.Text = JsonConvert.SerializeObject(resultContent, Formatting.Indented); ;
                    syncRecpt = JsonConvert.DeserializeObject<CloudPoolingResponse.RootObject>(resultContent);
                    Session["PoolRcpt"] = syncRecpt;
                }
                await getReceiptAsync();
            }
            catch (Exception clouse)
            {
                resultContent = clouse.InnerException.Message.ToString();
            }
        }

        private async Task getReceiptAsync()
        {
            WebClient client = new WebClient();
            if (syncRecpt.Receipt.Error == "false" && !string.IsNullOrEmpty(syncRecpt.Receipt.receiptUrl))
            {
                CloudReceipt.Rootobject pollingReceipt = new CloudReceipt.Rootobject();
                do
                {
                    pollingReceipt = await Task.Run(() => poolReceipt(syncRecpt.Receipt.receiptUrl));

                } while (pollingReceipt.receipt.Completed != "true");
                txtPollingReceipt.Text = JsonConvert.SerializeObject(pollingReceipt
                                                        , Formatting.Indented
                                                        , new JsonSerializerSettings
                                                        {
                                                            NullValueHandling = NullValueHandling.Ignore
                                                        });

                CloudReceipt.Rootobject cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(txtPollingReceipt.Text.Trim());
                
                if (Request.QueryString.AllKeys.Count() == 0)
                {
                    if (cloudRece != null && (cloudRece.receipt.TxnName.ToLower().Trim() == "purchase" || cloudRece.receipt.TxnName.ToLower().Trim() == "preauth"))
                    {
                        lblFollowOn.Visible = true;
                        lblFollowOn.Text = Request.Url.ToString() + "?orderid=" + cloudRece.receipt.ReceiptId + "&txnNumber=" + cloudRece.receipt.TransId + "&storeId=" + txtStoreID.Text.Trim() + "&apiToken=" + txtAPIToken.Text.Trim() + "&terminalId=" + txtTerminalId.Text.Trim() + "&amount=" + cloudRece.receipt.Amount;

                    }
                    else { lblFollowOn.Visible = false; }
                }
                else
                {
                    lblFollowOn.Text = "";
                }

                //if (!string.IsNullOrEmpty(txtRequest.Text) && !string.IsNullOrEmpty(txtRespose.Text) && !string.IsNullOrEmpty(txtPollingReceipt.Text))
                //    db.SaveToDb(txtRequest.Text.Trim(), txtRespose.Text.Trim(), txtPollingReceipt.Text.Trim());
            }
            else
            {
            }
        }

        private async Task getReceiptAsyncParrallel()
        {
            WebClient client = new WebClient();

            if (syncRecpt.Receipt.Error == "false" && !string.IsNullOrEmpty(syncRecpt.Receipt.receiptUrl))
            {
                CloudReceipt.Rootobject pollingReceipt = new CloudReceipt.Rootobject();
                do
                {
                    pollingReceipt = await Task.Run(() => poolReceipt(syncRecpt.Receipt.receiptUrl));

                } while (pollingReceipt.receipt.Completed != "true");

                txtPollingReceipt.Text = JsonConvert.SerializeObject(pollingReceipt
                                                        , Formatting.Indented
                                                        , new JsonSerializerSettings
                                                        {
                                                            NullValueHandling = NullValueHandling.Ignore
                                                        });
            }
            else
            {
            }
        }

        public CloudReceipt.Rootobject poolReceipt(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (!string.IsNullOrEmpty(responseString))
            {
                cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(responseString);
            }
            RestContorols();

            return cloudRece;
        }

        private void RestContorols()
        {
            drpCashbackMode.SelectedIndex = 0;
            txtVisaCashBackLimit.Text = string.Empty;
            txtMCCashBackLimit.Text = string.Empty;
            txtDebit.Text = string.Empty;
            txtDebit.Enabled = false;
            txtMCCashBackLimit.Enabled = false;
            txtVisaCashBackLimit.Enabled = false;
            if (chkMoto.Checked)
                chkMoto.Checked = false;
        }

        protected void drpCashbackMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCashbackMode.SelectedItem.Value == "CD")
            {
                txtDebit.Enabled = false;
                txtVisaCashBackLimit.Enabled = false;
                txtMCCashBackLimit.Enabled = false;
            }
            if (drpCashbackMode.SelectedItem.Value == "CE")
            {
                txtDebit.Enabled = true;
                txtVisaCashBackLimit.Enabled = false;
                txtMCCashBackLimit.Enabled = false;
            }
            if (drpCashbackMode.SelectedItem.Value == "DE")
            {
                txtDebit.Enabled = false;
                txtVisaCashBackLimit.Enabled = true;
                txtMCCashBackLimit.Enabled = true;
            }
            if (drpCashbackMode.SelectedItem.Value == "HE")
            {
                txtDebit.Enabled = true;
                txtVisaCashBackLimit.Enabled = true;
                txtMCCashBackLimit.Enabled = true;
            }
        }


    }
}