using MonerisDAL.App_Code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonerisAPPDAL.App_Code.DBHelper
{
    public class GatewayDbRespository
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString();
        public bool InsertToCloudTransactions(string request, string res, string receipt) {
            bool flag = false;
            CloudReceipt.Rootobject cloudRece = JsonConvert.DeserializeObject<CloudReceipt.Rootobject>(receipt);
            try
            {
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO CloudTransactions(Request,Response, Receipt, Complete,ResponseCode,TransactionType,ErrorCode,Error,Note) VALUES (@request,@response,@receipt,@complete,@responseCode,@tType,@errorCode,@error,@note)", conn))
                        {
                            cmd.Parameters.AddWithValue("@request", request);
                            cmd.Parameters.AddWithValue("@response", res);
                            cmd.Parameters.AddWithValue("@receipt", receipt);
                            if (cloudRece != null)
                            {
                                cmd.Parameters.AddWithValue("@complete", cloudRece.receipt.Completed ?? "null");
                                cmd.Parameters.AddWithValue("@responseCode", cloudRece.receipt.ResponseCode ?? "null");
                                cmd.Parameters.AddWithValue("@tType", cloudRece.receipt.TransType ?? "null");
                                cmd.Parameters.AddWithValue("@errorCode", cloudRece.receipt.ErrorCode ?? "null");
                                cmd.Parameters.AddWithValue("@error", cloudRece.receipt.Error ?? "null");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@complete", "null");
                                cmd.Parameters.AddWithValue("@responseCode", "null");
                                cmd.Parameters.AddWithValue("@tType", "null");
                                cmd.Parameters.AddWithValue("@errorCode", "null");
                                cmd.Parameters.AddWithValue("@error", "null");

                            }
                            //      cmd.Parameters.AddWithValue("@note", txtTranNote.Text ?? "null");
                            cmd.Parameters.AddWithValue("@note", "" ?? "null");
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                flag = true;
                            }
                            //rows number of record got inserted
                        }
                    }
                }
            }
            catch
            {
                flag = false;
                //lblDbSave.Visible = true;
                //lblDbSave.Text = rr.Message.ToString();
            }
            return flag;
        }
    }
}
