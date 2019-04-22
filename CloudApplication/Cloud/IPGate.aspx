<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IPGate.aspx.cs" Inherits="CloudApplication.Cloud.IPGate" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IPGate Cloud</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <style>
        label {
            color: gray;
        }

        body {
            padding-left: 20px;
        }

        th {
            font: bolder;
        }

        table.fixed {
            table-layout: fixed;
        }

            table.fixed tbody td {
                overflow: scroll;
            }

        textarea {
            width: 100%;
            min-height: 10rem;
            font-family: "Lucida Console", Monaco, monospace;
            font-size: 1.4rem;
            line-height: 1.2;
            color: white;
            background-color: dimgrey;
        }

        /*input {
            font-weight: bold;
        }*/
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <%-- <div class="col-lg-12">
            <div class="card">
                <div class="card-header">Panel Header</div>
                <div class="card-body">Panel  Bodey</div>
                <div class="card-footer">Panel Footer</div>
            </div>

        </div>--%>



        <div class="col-lg-12 ">
            <div class=" card">
                <div class="card-header">
                    Store Settings
                </div>
                <div class="card-body">
                    <div class="col-lg-12 row">
                        <div class="col-sm-4">
                            Merchant #
                        <asp:TextBox ID="txtMerchantId" runat="server" CssClass="form-control" placeHolder="Merchant Number"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            TerminalId
                        <asp:TextBox ID="txtTerminalID" runat="server" CssClass="form-control" placeHolder="Terminal ID"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            Token
                        <asp:TextBox ID="txtToken" runat="server" CssClass="form-control" placeHolder="API Token"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-lg-12" style="padding-top: 20px;">

            <div class="row">
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            Financial Transactions
                            <asp:LinkButton ID="lnkNextDayFollowonTransaction" Text="Next Day Follow on" PostBackUrl="~/Cloud/IPGateFollowOnTransactions.aspx" CssClass="btn-link text-right" runat="server"></asp:LinkButton>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Test Case</label>
                                </div>
                                <div class="col-lg-6">
                                    <label>Amount</label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtTestCase" CssClass="form-control" placeholder="Test Case" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" placeholder="Amount"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <label>Invoice</label>
                                </div>
                                <div class="col-lg-6">
                                    <label>Invoice </label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtInvoice" CssClass="form-control" runat="server" placeholder="Invoice Number"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtOriginalinvNumber" CssClass="form-control" runat="server" placeholder="Original Invoice Number (Force Post)"></asp:TextBox>
                                </div>
                                <div class="col-lg-12">
                                    <label>Echo Data</label>
                                </div>
                                <div class="col-lg-12">
                                    <asp:TextBox ID="txtEchoData" CssClass="form-control" runat="server" placeholder="Echo Data"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <label>Entry Method</label>
                                </div>
                                <div class="col-lg-6">
                                    <label>Moto</label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:RadioButtonList ID="rdEntryMethods" CssClass="radio-inline" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="N/A" Text="No Option" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="B" Text="Cash back (Only Debit)"></asp:ListItem>
                                        <asp:ListItem Value="M" Text="Manual Entry"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList ID="drpMoto" runat="server" CssClass="form-control" AutoPostBack="false">
                                        <asp:ListItem Value="false" Text="False" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="true" Text="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnPurchase" CssClass="btn btn-outline-dark" runat="server" Text="Purchase" OnClick="btnPurchase_Click" />
                            <asp:Button ID="btnPreAuth" CssClass="btn  btn-outline-dark" runat="server" Text="Pre Auth" OnClick="btnPreAuth_Click" />
                            <asp:Button ID="btnCapture" CssClass="btn  btn-outline-dark" runat="server" Text="Completion" OnClick="btnCapture_Click" />
                            <asp:Button ID="btnPurchaseCorrections" CssClass="btn  btn-outline-dark " runat="server" Text="Purchase Corrections" OnClick="btnPurchaseCorrections_Click" />
                            <asp:Button ID="btnForcePost" CssClass="btn  btn-outline-dark" runat="server" Text="Force Post" OnClick="btnForcePost_Click" />
                            <asp:Button ID="btnRefund" CssClass="btn  btn-outline-dark" runat="server" Text="Refund" OnClick="btnRefund_Click" />
                            <asp:Button ID="btnRefundCorrections" CssClass="btn  btn-outline-dark" runat="server" Text="Refund Correction" OnClick="btnRefundCorrections_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            Admin Transactions
                        </div>
                        <div class="card-body">
                            <asp:TextBox ID="txtPairingToken" CssClass="form-control" runat="server" placeholder="Token"></asp:TextBox>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnUnPair" CssClass="btn btn-outline-dark" runat="server" Text="UnPair" OnClick="btnUnPair_Click" />
                            <asp:Button ID="btnPair" CssClass="btn btn-outline-dark" runat="server" Text="Pair" OnClick="btnPair_Click" />
                            <asp:Button ID="btnOpenTotal" CssClass="btn btn-outline-dark" runat="server" OnClick="btnOpenTotal_Click" Text="Open Total" />
                            <asp:Button ID="btnBatchCLose" CssClass="btn btn-outline-dark" runat="server" OnClick="btnBatchCLose_Click" Text="Batch Close" />
                        </div>
                    </div>
                    <br />
                    <div class="card">
                        <div class="card-header">
                            Misc Transactions
                        </div>
                        <div class="card-body">
                            <div class="col-sm-12 row">
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtCahsbackLimit" CssClass="form-group form-control" runat="server" placeholder="Cashback Limit"></asp:TextBox>
                                </div>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="drpMode" CssClass="form-group form-control" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="‘CD’ = Cashback is disabled (default)" Value="CD"></asp:ListItem>
                                        <asp:ListItem Text="‘CE’ = Debit Cashback is enabled" Value="CE"></asp:ListItem>
                                        <asp:ListItem Text="‘DE’ = Credit Cashback is enabled" Value="DE"></asp:ListItem>
                                        <asp:ListItem Text="‘HE’ = Credit and Debit Cashback is enabled" Value="HE"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnEnableCashBack" CssClass="btn btn-outline-danger" runat="server" Text="Enable Cash Back" OnClick="btnEnableCashBack_Click"/>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtSurcharge" CssClass="form-control" runat="server" placeholder="Surcharge Amount"></asp:TextBox>
                                </div>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="drpSurchargeMode" runat="server" CssClass="form-control form-group" AutoPostBack="false">
                                        <asp:ListItem Text="‘SE’ = Surcharge processing is enabled for debit card transactions" Value="SE"></asp:ListItem>
                                        <asp:ListItem Text="‘SC’ = Surcharge processing is enabled for cashback transactions" Value="SC"></asp:ListItem>
                                        <asp:ListItem Text="‘SD’ = Surcharge processing is disabled (default)" Value="SD"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnSurcharge" runat="server" Text="Enable Surcharge" CssClass="btn btn-outline-danger" OnClick="btnSurcharge_Click" />
                                </div>
                            </div>

                            <div class="col-sm-12 row">
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="drpEntryMethod" CssClass="form-control form-group" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="‘C’ – swipe / insert / tap credit cards only" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="‘E’ - swipe / insert credit cards" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="‘M’ – manually-entered credit cards" Value="M"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnBalanceInquiry" CssClass="btn btn-outline-danger" runat="server" Text="Balance Inquiry" OnClick="btnBalanceInquiry_Click" />
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="drpSetTip" CssClass="form-control" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="‘TF’ - Tip entry is disabled" Value="TF"></asp:ListItem>
                                        <asp:ListItem Text="‘TB’ - In dollar amount" Value="TB"></asp:ListItem>
                                        <asp:ListItem Text="‘TC’ - In Percentage " Value="TC"></asp:ListItem>
                                        <asp:ListItem Text="‘TH’ – Both Dollar and Percentage" Value="TH"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="brnSetTIp" CssClass="btn btn-outline-danger" runat="server" Text="Set TIp" OnClick="brnSetTIp_Click" />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>


            <br />

            <div class="row">
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">Gift and Loyalty Transactions</div>
                        <div class="card-body">
                            <asp:TextBox ID="txtGLAmount" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnGPurchase" runat="server" CssClass="btn btn-outline-dark" Text="G Purchase" OnClick="btnGPurchase_Click" />
                            <asp:Button ID="btnGRefund" runat="server" CssClass="btn btn-outline-dark" Text="G Refund" OnClick="btnGRefund_Click" />
                            <asp:Button ID="btnGVoid" runat="server" CssClass="btn btn-outline-dark" Text="G Void" OnClick="btnGVoid_Click" />
                            <asp:Button ID="btnLPurchase" runat="server" CssClass="btn btn-outline-dark" Text="L Purchase" OnClick="btnLPurchase_Click" />
                            <asp:Button ID="btnLRefund" runat="server" CssClass="btn btn-outline-dark" Text="L Refund" OnClick="btnLRefund_Click"/>
                            <asp:Button ID="btnLVoid" runat="server" CssClass="btn btn-outline-dark" Text="L Void" OnClick="btnLVoid_Click" />
                        </div>
                    </div>
                </div>

                <div class=" col-lg-6">
                    <div class="card ">
                        <div class="card-header">Gift and Loyalty Admin</div>
                        <div class="card-body">
                            <div class="row col-sm-12">
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGiftTerminalId" runat="server" placeholder="Gift Loyalty Terminal ID" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="drpGiftMode" runat="server" AutoPostBack="false" CssClass="form-control">
                                        <asp:ListItem Text="‘LE’ = Moneris Gift and Loyalty functionality is enabled" Value="LE"></asp:ListItem>
                                        <asp:ListItem Text="‘LD’ = Moneris Gift and Loyalty functionality is disabled (default)" Value="LD"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnGiftInit" runat="server" OnClick="btnGiftInit_Click" Text="Gift Init" CssClass="btn btn-outline-danger " />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <b>URL :</b><asp:Label ID="lblReqUrl" runat="server"></asp:Label>
        <br />


        <table class="table fixed" style="width: 100%; max-height: 100px;">
            <thead>
                <tr>
                    <td>Request</td>
                    <td>Sync Response</td>
                    <td>Async Response From URL</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:TextBox TextMode="MultiLine" runat="server" ID="txtRequest" Enabled="false" Rows="20"> </asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox TextMode="MultiLine" runat="server" ID="txtResposne" Enabled="false" Rows="20"> </asp:TextBox>
                    </td>
                    <td>
                        <pre><asp:TextBox TextMode="MultiLine" runat="server" ID="txtAsyncResponse" Enabled="false" Rows="20"> </asp:TextBox></pre>
                    </td>
                </tr>
            </tbody>
        </table>


        <b></b>
        <asp:Label ID="lblTestCase" runat="server"></asp:Label>
        <br />


        <table>
            <tr>
                <td>
                    <asp:Button ID="btnPollingReceipt" runat="server" CssClass="btn btn-outline-success" Text="Polling Receipt" OnClick="btnPollingReceipt_Click" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
