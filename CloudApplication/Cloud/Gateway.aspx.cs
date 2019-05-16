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

        protected async void btnPurchase_Click(object sender, EventArgs e)
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

            if (drpMonerisTokenization.SelectedIndex > 0)
            {
                //transaction.request.monerisToken =  Convert.ToInt32(drpMonerisTokenization.SelectedValue);
                transaction.request.monerisToken = drpMonerisTokenization.SelectedValue;
            }
            if (!string.IsNullOrEmpty(txtMonerisToken.Text))
            {
                transaction.request.token = txtTxnNumber.Text.Trim();
            }

            await performTransactionAsync();
        }

        protected async void btnPreauth_Click(object sender, EventArgs e)
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
            await performTransactionAsync();

        }

        protected async void btnCompletion_Click(object sender, EventArgs e)
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
                await performTransactionAsync();
            }
        }

        protected async void btnRefund_Click(object sender, EventArgs e)
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
                await performTransactionAsync();
            }
        }

        protected async void btnVoid_Click(object sender, EventArgs e)
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
                await performTransactionAsync();
            }
        }

        protected async void btnIndRefund_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "refund";
            transaction.request = new CloudTransaction.Request()
            {
                amount = txtAmount.Text.ToString().Trim(),
                echoData = txtEchoData.Text.Trim()
            };

            if (string.IsNullOrEmpty(txtOrderID.Text.Trim().ToString()))
            {
                transaction.request.orderId = "Test_KP_IndRefund" + System.DateTime.Now.TimeOfDay.ToString();
            }
            else
            {
                transaction.request.orderId = txtOrderID.Text.Trim().ToString();
            }
            if (drpEntryMethod.SelectedItem.Value != "A")
            {
                transaction.request.entryMethod = drpEntryMethod.SelectedItem.Value.ToString();
            }
            await performTransactionAsync();
        }

        protected async void btnFpost_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "forcePost";

            transaction.request = new CloudTransaction.Request()
            {
                amount = txtAmount.Text.Trim(),
                originalApprovalNumber = txtOriginalInvNumber.Text.Trim(),
                echoData = txtEchoData.Text.Trim()
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
            await performTransactionAsync();
        }

        protected async void btnPairing_Click(object sender, EventArgs e)
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
            await performTransactionAsync();

        }

        protected async void btnunpair_Click(object sender, EventArgs e)
        {
            transaction.storeId = txtStoreID.Text.Trim();
            transaction.apiToken = txtAPIToken.Text.Trim();
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.txnType = "unpair";
            transaction.terminalId = txtTerminalId.Text.Trim();
            //transaction.request = new CloudTransaction.Request()
            //{

            //};
            await performTransactionAsync();
        }

        protected async void btnSetTip_Click(object sender, EventArgs e)
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
            if (!string.IsNullOrEmpty(txtPercentPre1.Text))
                transaction.request.predefinedPercentage1 = txtPercentPre1.Text.Trim();
            if (!string.IsNullOrEmpty(txtPercentPre2.Text))
                transaction.request.predefinedPercentage2 = txtPercentPre2.Text.Trim();
            if (!string.IsNullOrEmpty(txtPercentPre3.Text))
                transaction.request.predefinedPercentage3 = txtPercentPre3.Text.Trim();
            if (!string.IsNullOrEmpty(txtTipPerThreshold.Text))
                transaction.request.tipWarningThreshold = txtTipPerThreshold.Text.Trim();

            //Reset a selections
            drpSetTip.SelectedIndex = 0;
            ResetTipAmounts();

            await performTransactionAsync();
        }

        private void ResetTipAmounts()
        {
            txtTipPerThreshold.Enabled = false;
            txtTipPerThreshold.Text = "";
            txtPercentPre1.Enabled = false;
            txtPercentPre1.Text = "";
            txtPercentPre2.Enabled = false;
            txtPercentPre2.Text = "";
            txtPercentPre3.Enabled = false;
            txtPercentPre3.Text = "";
        }

        protected async void btnBatchClose_Click(object sender, EventArgs e)
        {
            transaction.txnType = "batchClose";
            transaction.terminalId = txtTerminalId.Text.Trim();
            //transaction.request = new CloudTransaction.Request()
            //{

            //};
            await performTransactionAsync();
        }

        protected async void btnOpenTotal_Click(object sender, EventArgs e)
        {
            transaction.txnType = "openTotal";
            transaction.terminalId = txtTerminalId.Text.Trim();
            //transaction.request = new CloudTransaction.Request()
            //{

            //};
            await performTransactionAsync();
        }

        protected async void btnInit_Click(object sender, EventArgs e)
        {
            transaction.txnType = "initialization";
            transaction.terminalId = txtTerminalId.Text.Trim();
            transaction.request = new CloudTransaction.Request()
            {

            };
            await performTransactionAsync();
        }

        protected async void btnCardRead_Click(object sender, EventArgs e)
        {
            //transaction.txnType = "cardRead";
            transaction.txnType = "encrypedCardRead";
            transaction.terminalId = txtTerminalId.Text.Trim();
            //transaction.request = new CloudTransaction.Request()
            //{

            //};
            await performTransactionAsync();
        }

        protected async void btnCashback_Click(object sender, EventArgs e)
        {
            transaction.txnType = "cashback";
            transaction.terminalId = txtTerminalId.Text.Trim();


            transaction.request = new CloudTransaction.Request()
            {
                //mode = drpCashbackMode.SelectedItem.Value,
                enableCashback = drpCashbackMode.SelectedItem.Value,
                predefinedAmount1 = txtPre1CashbackAmount.Text.Trim(),
                predefinedAmount2 = txtPre2CashbackAmount.Text.Trim(),
                predefinedAmount3 = txtPre3CashbackAmount.Text.Trim(),
                interacCashbackLimit = txtDebit.Text.Trim(),
                visaCashbackLimit = txtVisaCashBackLimit.Text.Trim(),
                mastercardCashbackLimit = txtMCCashBackLimit.Text.Trim()

                //debit = new CloudTransaction.Request.Debit
                //{
                //    limit = txtDebit.Text.Trim()
                //},
                //credit = new CloudTransaction.Request.Credit[] {
                //    new CloudTransaction.Request.Credit() {  cardPlan ="V", limit = txtVisaCashBackLimit.Text.Trim() },
                //    new CloudTransaction.Request.Credit() { cardPlan = "M", limit = txtMCCashBackLimit.Text.Trim()}
                //}
            };
            if (chkCustomCashback.Checked)
                transaction.request.allowCashbackCustomEntry = "1"; //0 or 1
            else
                transaction.request.allowCashbackCustomEntry = "0";
            await performTransactionAsync();
        }

        protected async void btnSurcharge_Click(object sender, EventArgs e)
        {
            transaction.txnType = "surcharge";
            transaction.request = new CloudTransaction.Request()
            {
                enableSurcharge = drpSurcharge.SelectedItem.Value
            };

            if (drpSurcharge.SelectedItem.Value == "1")
            {
                transaction.request.surchargeFeeOnInterac = txtSurchargeFeeOnIntrac.Text.Trim();
                transaction.request.surchargeFeeOnInteracCashback = txtSurchargeFeeOnIntracWithCashback.Text.Trim();
                transaction.request.thresholdLimit = txtSurchargeThreshold.Text.Trim();
            }
            else if (drpSurcharge.SelectedItem.Value == "0")
            {
                transaction.request.surchargeFeeOnInterac = "0";
                transaction.request.surchargeFeeOnInteracCashback = "0";
                transaction.request.thresholdLimit = "0";
            }
            await performTransactionAsync();
            ResetSurcharge();
        }

        protected async void btnBarCodeScan_Click(object sender, EventArgs e)
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
            await performTransactionAsync();
        }

        //protected void btnPool_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["poolRcpt"] != null)
        //        {
        //            CloudPoolingResponse.RootObject r = (CloudPoolingResponse.RootObject)Session["poolRcpt"];
        //            string URL = r.Receipt.receiptUrl;
        //            if (URL != null && !string.IsNullOrEmpty(URL))
        //            {
        //                txtPollingReceipt.Text = JsonConvert.SerializeObject(poolReceipt(URL)
        //                                                    , Formatting.Indented
        //                                                    , new JsonSerializerSettings
        //                                                    {
        //                                                        NullValueHandling = NullValueHandling.Ignore
        //                                                    });
        //                CloudReceipt.Rootobject v = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(txtPollingReceipt.Text.Trim());
        //                //txtTxnNumber.Text = v.receipt.TransId;
        //                Session["FollowOn"] = txtPollingReceipt.Text;
        //            }
        //            //btnSaveDB.Visible = true;
        //        }

        //        CloudReceipt.Rootobject cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(txtPollingReceipt.Text.Trim());
        //        int i = Request.QueryString.AllKeys.Count();
        //        if (Request.QueryString.AllKeys.Count() == 0)
        //        {
        //            if (cloudRece != null && (cloudRece.receipt.TxnName.ToLower().Trim() == "purchase" || cloudRece.receipt.TxnName.ToLower().Trim() == "preauth"))
        //            {
        //                lblFollowOn.Visible = true;
        //                lblFollowOn.Text = Request.Url.ToString() + "?orderid=" + cloudRece.receipt.ReceiptId + "&txnNumber=" + cloudRece.receipt.TransId + "&storeId=" + txtStoreID.Text.Trim() + "&apiToken=" + txtAPIToken.Text.Trim() + "&terminalId=" + txtTerminalId.Text.Trim() + "&amount=" + cloudRece.receipt.Amount;
        //            }
        //            else { lblFollowOn.Visible = false; }
        //        }
        //        else
        //        {
        //            lblFollowOn.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(txtRequest.Text) && !string.IsNullOrEmpty(txtRespose.Text) && !string.IsNullOrEmpty(txtPollingReceipt.Text))
        //            db.SaveToDb(txtRequest.Text.Trim(), txtRespose.Text.Trim(), txtPollingReceipt.Text.Trim());
        //    }


        //    catch
        //    {

        //    }

        //}

        protected void btnSaveDB_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            bool success = false;
            try
            {
                success = db.InsertToCloudTransactions(txtRequest.Text.Trim(), txtRespose.Text.Trim(), txtPollingReceipt.Text.Trim());
            }
            catch (Exception ex)
            {
                s = ex.InnerException.Message;
            }
        }

        protected void drpSurcharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSurcharge.SelectedItem.Value == "0")
            {
                ResetSurcharge();
            }
            else
            {
                txtSurchargeFeeOnIntrac.Enabled = true;
                txtSurchargeFeeOnIntracWithCashback.Enabled = true;
                txtSurchargeThreshold.Enabled = true;
            }
        }

        private void ResetSurcharge()
        {
            txtSurchargeFeeOnIntrac.Enabled = false;
            txtSurchargeFeeOnIntrac.Text = "";
            txtSurchargeFeeOnIntracWithCashback.Enabled = false;
            txtSurchargeFeeOnIntracWithCashback.Text = "";
            txtSurchargeThreshold.Enabled = false;
            txtSurchargeThreshold.Text = "";
        }

        protected async void btnBalanceInquiery_Click(object sender, EventArgs e)
        {
            transaction.txnType = "balanceInquiry";
            transaction.request = new CloudTransaction.Request()
            {
                entryMethod = drpEntryMethod.SelectedItem.Value
            };
            await performTransactionAsync();
        }

        protected async void btnMPOS_Click(object sender, EventArgs e)
        {
            transaction.txnType = "mobilePosSetting";
            if (chkmPOS.Checked)
                transaction.request = new CloudTransaction.Request() { enabled = true };
            else
                transaction.request = new CloudTransaction.Request() { enabled = false };
            await performTransactionAsync();
        }
        protected async void btnPanMask_Click(object sender, EventArgs e)
        {
            transaction.txnType = "panMaskingSetting";
            if (chkPanMask.Checked)
                transaction.request = new CloudTransaction.Request() { enabled = true };
            else
                transaction.request = new CloudTransaction.Request() { enabled = false };

            await performTransactionAsync();
        }


        protected async void btnaddBinRnage_Click(object sender, EventArgs e)
        {
            //transaction.txnType = "addGiftBinRange";
            transaction.txnType = "addBinRange";
            transaction.request = new CloudTransaction.Request()
            {
                lowPrefix = txtLowPrefix.Text.Trim(),
                highPrefix = txtHighPrefix.Text.Trim()
            };
            await performTransactionAsync();
        }

        protected async void btnDeleteRange_Click(object sender, EventArgs e)
        {
            //transaction.txnType = "deleteGiftBinRange";
            transaction.txnType = "deleteBinRange";
            transaction.request = new CloudTransaction.Request()
            {
                lowPrefix = txtDelLowRange.Text.Trim(),
                highPrefix = txtDelHighRange.Text.Trim()
            };
            await performTransactionAsync();
        }

        protected async void btnGiftCardRead_Click(object sender, EventArgs e)
        {
            //transaction.txnType = "giftUnEncryptedCardRead";
            transaction.txnType = "unencryptedCardRead";
            transaction.request = new CloudTransaction.Request()
            {
                entryOptions = txtEntryOption.Text.Trim(),
                track = txtTrack.Text.Trim()
            };
            await performTransactionAsync();
        }

        protected async void btnGetBinRange_Click(object sender, EventArgs e)
        {
            transaction.txnType = "getBinRanges";
            transaction.request = new CloudTransaction.Request();
            await performTransactionAsync();
        }
        protected async void btntokenization_Click(object sender, EventArgs e)
        {
            transaction.txnType = "setTokenization";
            if (chkTokenization.Checked)
                transaction.request.enabled = true;
            else
                transaction.request.enabled = false;
            await performTransactionAsync();
        }

        /// Helper Methods
        /// 
        private async Task performTransactionAsync()
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
            //lblRequestUrl.Text = postUrl;
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
                    if (pollingReceipt.receipt.Error == "true")
                        break;
                } while (pollingReceipt.receipt.Completed != "true" || pollingReceipt.receipt.Error != "false");

                txtPollingReceipt.Text = JsonConvert.SerializeObject(pollingReceipt
                                                        , Formatting.Indented
                                                        , new JsonSerializerSettings
                                                        {
                                                            NullValueHandling = NullValueHandling.Ignore
                                                        });
                if (!string.IsNullOrEmpty(txtPollingReceipt.Text.Trim()))
                    Session["FollowOn"] = txtPollingReceipt.Text;


                CloudReceipt.Rootobject cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(txtPollingReceipt.Text.Trim());
                if (Request.QueryString.AllKeys.Count() == 0)
                {
                    if (cloudRece != null && (cloudRece.receipt.TxnName.ToLower().Trim() == "purchase" || cloudRece.receipt.TxnName.ToLower().Trim() == "preauth"))
                    {
                        if (cloudRece.receipt.Error == "true")
                            lblFollowOn.Visible = true;
                        else
                            lblFollowOn.Visible = false;
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

        public async Task<CloudReceipt.Rootobject> poolReceipt(string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync(URL);
                return JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(result);
            }

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //if (!string.IsNullOrEmpty(responseString))
            //{
            //    cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(responseString);
            //}
            //RestContorols();

            //return cloudRece;
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

            if (drpCashbackMode.SelectedValue == "1")
            {
                chkCustomCashback.Enabled = true;
                txtDebit.Enabled = true;
                txtVisaCashBackLimit.Enabled = true;
                txtMCCashBackLimit.Enabled = true;
                txtPre1CashbackAmount.Enabled = true;
                txtPre2CashbackAmount.Enabled = true;
                txtPre3CashbackAmount.Enabled = true;
            }
            else if (drpCashbackMode.SelectedValue == "0")
            {
                chkCustomCashback.Checked = false;
                chkCustomCashback.Enabled = false;
                txtDebit.Enabled = false;
                txtDebit.Text = "";
                txtVisaCashBackLimit.Enabled = false;
                txtVisaCashBackLimit.Text = "";
                txtMCCashBackLimit.Enabled = false;
                txtMCCashBackLimit.Text = "";
                txtPre1CashbackAmount.Enabled = false;
                txtPre1CashbackAmount.Text = "";
                txtPre2CashbackAmount.Enabled = false;
                txtPre2CashbackAmount.Text = "";
                txtPre3CashbackAmount.Enabled = false;
                txtPre3CashbackAmount.Text = "";
            }
            //if (drpCashbackMode.SelectedItem.Value == "CD")
            //{
            //    txtDebit.Enabled = false;
            //    txtVisaCashBackLimit.Enabled = false;
            //    txtMCCashBackLimit.Enabled = false;
            //}
            //if (drpCashbackMode.SelectedItem.Value == "CE")
            //{
            //    txtDebit.Enabled = true;
            //    txtVisaCashBackLimit.Enabled = false;
            //    txtMCCashBackLimit.Enabled = false;
            //}
            //if (drpCashbackMode.SelectedItem.Value == "DE")
            //{
            //    txtDebit.Enabled = false;
            //    txtVisaCashBackLimit.Enabled = true;
            //    txtMCCashBackLimit.Enabled = true;
            //}
            //if (drpCashbackMode.SelectedItem.Value == "HE")
            //{
            //    txtDebit.Enabled = true;
            //    txtVisaCashBackLimit.Enabled = true;
            //    txtMCCashBackLimit.Enabled = true;
            //}
        }

        protected void drpSetTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSetTip.SelectedItem.Value != "TF" || drpSetTip.SelectedIndex != 0)
            {
                txtPercentPre1.Enabled = true;
                txtPercentPre2.Enabled = true;
                txtPercentPre3.Enabled = true;
                txtTipPerThreshold.Enabled = true;
            }
            else
            {

                txtPercentPre1.Enabled = false;
                txtPercentPre2.Enabled = false;
                txtPercentPre3.Enabled = false;
                txtTipPerThreshold.Enabled = false;
                ResetTipAmounts();
            }
        }

    }
}