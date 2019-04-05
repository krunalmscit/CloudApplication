using MonerisDAL.App_Code.Cloud.IPGate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MonerisDAL.App_Code.DBHelper
{
    public class IPGateCloudDbRepository
    {
        string v =   System.Configuration.ConfigurationManager.ConnectionStrings.ToString();
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString();
        public void InsertDateToDB(CloudReceipt.Rootobject response, string RequestJSON, string SyncResponse, string PollingResponse, String TestCase, string MerchantID, string APIToken)
        {
            try
            {
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO[dbo].[IPGateTransaction]([TestCase],[TxnName],[Amount],[TransType],[TransDate],[TransTime],[ReferenceNumber],[CloudTicket],[Completed],[Error],[InitRequired],[SafIndicator],[ResponseCode],[ISO],[LanguageCode],[PartialAuthAmount],[AvailableBalance],[TipAmount],[EMVCashBackAmount],[SurchargeAmount],[ForeignCurrencyAmount],[ForeignCurrencyCode],[BaseRate],[ExchangeRate],[Pan],[CardType],[CardName],[AccountType],[SwipeIndicator],[FormFactor],[CvmIndicator],[ReservedField1],[ReservedField2],[AuthCode],[InvoiceNumber],[EMVEchoDataq],[ReservedField3],[ReservedField4],[Aid],[AppLabel],[AppPreferredName],[Arqc],[TvrArqc],[Tcacc],[TvrTcacc],[Tsi],[TokenResponseCode],[Token],[LogonRequired],[EncryptedCardInfo],[EncryptedTrack2] , [EncryptedPan],[BatchNumber],[TerminalNumber],[MerchantID], [RequestJSON],[SyncResposne] ,[PollingResponse],[AvailableForComp], [SynchronizationData], [APIToken]) VALUES (@TestCase,@TxnName,@Amount,@TransType,@TransDate,@TransTime,@ReferenceNumber,@CloudTicket,@Completed,@Error,@InitRequired,@SafIndicator,@ResponseCode, @ISO,@LanguageCode,@PartialAuthAmount,@AvailableBalance,@TipAmount,@EMVCashBackAmount,@SurchargeAmount,@ForeignCurrencyAmount, @ForeignCurrencyCode,@BaseRate,@ExchangeRate,@Pan,@CardType,@CardName,@AccountType,@SwipeIndicator,@FormFactor,@CvmIndicator, @ReservedField1,@ReservedField2,@AuthCode,@InvoiceNumber,@EMVEchoDataq,@VReservedField3,@ReservedField4,@Aid,@AppLabel,@AppPreferredName, @Arqc,@TvrArqc,@Tcacc,@TvrTcacc,@Tsi,@TokenResponseCode,@Token,@LogonRequired,@EncryptedCardInfo, @EncryptedTrack2, @EncryptedPan,@BatchNumber,@TerminalNumber,@MerchantID,@ReqJSON, @SyncResponse, @PollingResponse,@AvailableForComp,@synchronizationData,@APIToken)", conn))
                        {
                            cmd.Parameters.AddWithValue("@TestCase", CheckForNull(TestCase));
                            cmd.Parameters.AddWithValue("@TxnName", CheckForNull(response.receipt.TxnName));
                            cmd.Parameters.AddWithValue("@Amount", CheckForNull(response.receipt.Amount));
                            cmd.Parameters.AddWithValue("@TransType", CheckForNull(response.receipt.TransType));
                            cmd.Parameters.AddWithValue("@TransDate", CheckForNull(response.receipt.TransDate));
                            cmd.Parameters.AddWithValue("@TransTime", CheckForNull(response.receipt.TransTime));
                            cmd.Parameters.AddWithValue("@ReferenceNumber", CheckForNull(response.receipt.ReferenceNumber));
                            cmd.Parameters.AddWithValue("@CloudTicket", CheckForNull(response.receipt.CloudTicket));
                            cmd.Parameters.AddWithValue("@Completed", CheckForNull(response.receipt.Completed));
                            cmd.Parameters.AddWithValue("@Error", CheckForNull(response.receipt.Error));
                            cmd.Parameters.AddWithValue("@InitRequired", CheckForNull(response.receipt.InitRequired));
                            cmd.Parameters.AddWithValue("@SafIndicator", CheckForNull(response.receipt.SafIndicator));
                            cmd.Parameters.AddWithValue("@ResponseCode", CheckForNull(response.receipt.ResponseCode));
                            cmd.Parameters.AddWithValue("@ISO", CheckForNull(response.receipt.ISO));
                            cmd.Parameters.AddWithValue("@LanguageCode", CheckForNull(response.receipt.LanguageCode));
                            cmd.Parameters.AddWithValue("@PartialAuthAmount", CheckForNull(response.receipt.PartialAuthAmount));
                            cmd.Parameters.AddWithValue("@AvailableBalance", CheckForNull(response.receipt.AvailableBalance));
                            cmd.Parameters.AddWithValue("@TipAmount", CheckForNull(response.receipt.TipAmount));
                            cmd.Parameters.AddWithValue("@EMVCashBackAmount", CheckForNull(response.receipt.EMVCashBackAmount));
                            cmd.Parameters.AddWithValue("@SurchargeAmount", CheckForNull(response.receipt.SurchargeAmount));
                            cmd.Parameters.AddWithValue("@ForeignCurrencyAmount", CheckForNull(response.receipt.ForeignCurrencyAmount));
                            cmd.Parameters.AddWithValue("@ForeignCurrencyCode", CheckForNull(response.receipt.ForeignCurrencyCode));
                            cmd.Parameters.AddWithValue("@BaseRate", CheckForNull(response.receipt.BaseRate));
                            cmd.Parameters.AddWithValue("@ExchangeRate", CheckForNull(response.receipt.ExchangeRate));
                            cmd.Parameters.AddWithValue("@Pan", CheckForNull(response.receipt.Pan));
                            cmd.Parameters.AddWithValue("@CardType", CheckForNull(response.receipt.CardType));
                            cmd.Parameters.AddWithValue("@CardName", CheckForNull(response.receipt.CardName));
                            cmd.Parameters.AddWithValue("@AccountType", CheckForNull(response.receipt.AccountType));
                            cmd.Parameters.AddWithValue("@SwipeIndicator", CheckForNull(response.receipt.SwipeIndicator));
                            cmd.Parameters.AddWithValue("@FormFactor", CheckForNull(response.receipt.FormFactor));
                            cmd.Parameters.AddWithValue("@CvmIndicator", CheckForNull(response.receipt.CvmIndicator));
                            cmd.Parameters.AddWithValue("@ReservedField1", CheckForNull(response.receipt.ReservedField1));
                            cmd.Parameters.AddWithValue("@ReservedField2", CheckForNull(response.receipt.ReservedField2));
                            cmd.Parameters.AddWithValue("@AuthCode", CheckForNull(response.receipt.AuthCode));
                            cmd.Parameters.AddWithValue("@InvoiceNumber", CheckForNull(response.receipt.InvoiceNumber));
                            cmd.Parameters.AddWithValue("@EMVEchoDataq", CheckForNull(response.receipt.EMVEchoData));
                            cmd.Parameters.AddWithValue("@VReservedField3", CheckForNull(response.receipt.ReservedField3));
                            cmd.Parameters.AddWithValue("@ReservedField4", CheckForNull(response.receipt.ReservedField4));
                            cmd.Parameters.AddWithValue("@Aid", CheckForNull(response.receipt.Aid));
                            cmd.Parameters.AddWithValue("@AppLabel", CheckForNull(response.receipt.AppLabel));
                            cmd.Parameters.AddWithValue("@AppPreferredName", CheckForNull(response.receipt.AppPreferredName));
                            cmd.Parameters.AddWithValue("@Arqc", CheckForNull(response.receipt.Arqc));
                            cmd.Parameters.AddWithValue("@TvrArqc", CheckForNull(response.receipt.TvrArqc));
                            cmd.Parameters.AddWithValue("@Tcacc", CheckForNull(response.receipt.Tcacc));
                            cmd.Parameters.AddWithValue("@TvrTcacc", CheckForNull(response.receipt.TvrTcacc));
                            cmd.Parameters.AddWithValue("@Tsi", CheckForNull(response.receipt.Tsi));
                            cmd.Parameters.AddWithValue("@TokenResponseCode", CheckForNull(response.receipt.TokenResponseCode));
                            cmd.Parameters.AddWithValue("@Token", CheckForNull(response.receipt.Token));
                            cmd.Parameters.AddWithValue("@LogonRequired", CheckForNull(response.receipt.LogonRequired));
                            cmd.Parameters.AddWithValue("@EncryptedCardInfo", CheckForNull(response.receipt.EncryptedCardInfo));
                            cmd.Parameters.AddWithValue("@EncryptedTrack2", CheckForNull(response.receipt.EncryptedTrack2));
                            cmd.Parameters.AddWithValue("@EncryptedPan", CheckForNull(response.receipt.EncryptedPan));
                            cmd.Parameters.AddWithValue("@MerchantID", CheckForNull(MerchantID));
                            cmd.Parameters.AddWithValue("@APIToken", CheckForNull(APIToken));
                            //string synchronizationDate = null;
                            if (response.receipt.synchronizationData != null)
                            {
                               string synchronizationDate = JsonConvert.SerializeObject(response.receipt.synchronizationData, Formatting.Indented);
                               cmd.Parameters.AddWithValue("@synchronizationData", synchronizationDate);
                            }
                            else { 
                            cmd.Parameters.AddWithValue("@synchronizationData", DBNull.Value);
                            }

                            if (!string.IsNullOrEmpty(response.receipt.ReferenceNumber))
                            {
                                string refrenceNumber = response.receipt.ReferenceNumber;
                                string host = refrenceNumber.Substring(8, 3);
                                string seq = refrenceNumber.Substring(14, 3);
                                string saf = refrenceNumber.Substring(17, 1);
                                cmd.Parameters.AddWithValue("@BatchNumber", refrenceNumber.Substring(11, 3));
                                cmd.Parameters.AddWithValue("@TerminalNumber", refrenceNumber.Substring(0, 8));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@BatchNumber", DBNull.Value);
                                cmd.Parameters.AddWithValue("@TerminalNumber", DBNull.Value);
                            }
                            cmd.Parameters.AddWithValue("@ReqJSON", CheckForNull(RequestJSON));
                            cmd.Parameters.AddWithValue("@SyncResponse", CheckForNull(SyncResponse));
                            cmd.Parameters.AddWithValue("@PollingResponse", CheckForNull(PollingResponse));
                            if (response.receipt.Completed == "true" && (response.receipt.TxnName == "Preauth" || response.receipt.TxnName == "Purchase"))
                            {
                                cmd.Parameters.AddWithValue("@AvailableForComp", "Y");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AvailableForComp", "N");
                            }
                            int rows = cmd.ExecuteNonQuery();
                            //rows number of record got inserted
                        }
                    }
                }
            }
            catch 
            { }
            finally
            {
            }
        }

        public int  UpdateTransactionByAuthCode(string authCode)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("update  IPGateTransaction  set AvailableForComp = @flag where AuthCode= @authCode ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar, 50));
            cmd.Parameters["@flag"].Value = "N";
            cmd.Parameters.Add(new SqlParameter("@authCode", SqlDbType.NVarChar, 50));
            cmd.Parameters["@authCode"].Value = authCode;
           
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException err)
            {
                if (err.Number == 2601)
                {
                    //return Constants.DuplicateRecordError;
                }
                else
                {
                    throw new ApplicationException("Data Error inserting into Activity status DB.");
                }
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public PreAuthDetails getTransactionDetilsByApprovalCodeAndTerminalID(string approvalCode, string terminalID)
        {
            PreAuthDetails preauth = GetAppPreAuhtPendingToPreauthByTermainlID(terminalID).Where(o => o.AuthCode == approvalCode).SingleOrDefault();
            if(preauth != null)
            { return preauth; }
            else { return null; }
        }

       

        public List<PreAuthDetails> GetAppPreAuhtPendingToPreauthByTermainlID(string terminalId)
        {
            List<PreAuthDetails> transactions = new List<PreAuthDetails>();
           
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("select Tid, Amount, AuthCode, TransDate, TransTime, CardType, EncryptedTrack2,Amount,Pan, AccountType, Aid, SynchronizationData, SwipeIndicator, MerchantID, APIToken  from IPGateTransaction where TxnName = 'Preauth' and AvailableForComp = 'Y' and completed ='true' and error = 'false'  and TerminalNumber = @TerminalNumber", conn))
                    {
                        sqlcmd.Parameters.AddWithValue("@TerminalNumber", CheckForNull(terminalId));
                        using (SqlDataReader dr = sqlcmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                PreAuthDetails preTransaction = new PreAuthDetails();
                                preTransaction.Tid = dr["TID"].ToString();
                                preTransaction.OriginalPreAuthAmount = dr["Amount"].ToString();
                                preTransaction.AuthCode = dr["AuthCode"].ToString();
                                preTransaction.CardType = dr["CardType"].ToString();
                                preTransaction.encryptedTrack2 = dr["EncryptedTrack2"].ToString();
                                preTransaction.OriginalPreAuthAmount = dr["Amount"].ToString();
                                preTransaction.PAN = dr["Pan"].ToString();
                                preTransaction.AccoutnType = dr["AccountType"].ToString();
                                preTransaction.Aid = dr["Aid"].ToString();
                                preTransaction.SynchronizationData = dr["SynchronizationData"].ToString();
                                preTransaction.SwipeIndicator = dr["SwipeIndicator"].ToString();
                                preTransaction.TransactionDate = dr["TransDate"].ToString();
                                preTransaction.TransactionTime = dr["TransTime"].ToString();
                                preTransaction.APIToken = dr["APIToken"].ToString();
                                preTransaction.MerchantId = dr["MerchantID"].ToString();
                                transactions.Add(preTransaction);
                            }
                        }
                    } 
                }
                return transactions.OrderBy(o=>o.Tid).ToList();
            }
            catch
            {
                return null;
            }
        }

        private object CheckForNull(string colValue)
        {
            if (string.IsNullOrEmpty(colValue))
            {
                return DBNull.Value;
            }
            else { return colValue; }
        }
    }
}