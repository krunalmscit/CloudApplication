<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gateway.aspx.cs" Inherits="CloudApplication.Cloud.Gateway" Async="true" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Gateway Cloud</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <style>
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
            min-height: 20rem;
            font-family: "Lucida Console", Monaco, monospace;
            font-size: 1rem;
            line-height: 1.2;
            color:white;
            background-color:#363a42;
        }

        input {
            font-weight: bold;
        }

        /*img {
            display: none;
        }*/
        .removeImage {
            display: none;
        }

        .hidden {
            display: none;
        }

        .unhidden {
            display: block;
        }
    </style>
    <script src="https://code.jquery.com/jquery-3.2.1.js" integrity="sha256-DZAnKJ/6XZ9si04Hgrsxu/8s717jcIzLy3oi35EouyE=" crossorigin="anonymous"></script>
</head>
<body>
    <script type="text/javascript">

        $(document).ready(function () {
            //  function EditClick() {
            //    console.log('Button Clicked');
            //}

            $('#imgEdit').click(function () {
                console.log('Edit clicked');
                $(this).attr('hidden', 'hidden');
                $('#imgAccept').removeAttr('hidden');
                
                $('#txtRequest').removeAttr ('disabled')
            });
            $('#imgAccept').click(function () {
                console.log('Acccept Changes');
                $(this).attr('hidden', 'hidden');
                $('#imgEdit').removeAttr('hidden');
                 $('#txtRequest').attr ('disabled', 'disabled')
            });

        });

    </script>
    <form id="form1" runat="server">


        <div class="row col-lg-12" style="padding-top: 10px">
            <div class="col-lg-4">
                Environment
                       <asp:RadioButtonList ID="rdEnv" runat="server" CssClass=" form-check form-check-inline" RepeatDirection="Horizontal" CellSpacing="10">
                           <asp:ListItem Text="QA1 / QA2" Value="0"></asp:ListItem>
                           <asp:ListItem Text="Internal QA" Value="1" Selected="True"></asp:ListItem>
                       </asp:RadioButtonList>
            </div>
        </div>

        <div class="row col-lg-12" style="padding-top: 10px">

            <div class="col-lg-4">
                Store ID 
                <asp:TextBox ID="txtStoreID" runat="server" CssClass="form-control" placeholder="Store Id"></asp:TextBox>

            </div>
            <div class="col-sm-4">
                API Token
                <asp:TextBox ID="txtAPIToken" runat="server" CssClass="form-control" placeholder="API Token"></asp:TextBox>
            </div>
            <div class="col-sm-4">
                Terminal ID                     
                <asp:TextBox ID="txtTerminalId" runat="server" placeholder="Terminal #" CssClass="form-control form-group"></asp:TextBox>
                <asp:Button ID="btnInit" runat="server" OnClick="btnInit_Click" Text="Init" CssClass="btn btn-secondary " />
                <asp:Button ID="btnCardRead" runat="server" OnClick="btnCardRead_Click" Text="Enc Card Read" CssClass="btn btn-secondary" />
                <asp:Button ID="btnBatchClose" runat="server" OnClick="btnBatchClose_Click" Text="Batch Close" CssClass="btn btn-secondary " />
                <asp:Button ID="btnOpenTotal" runat="server" OnClick="btnOpenTotal_Click" Text="Open Total" CssClass="btn btn-secondary" />

            </div>
        </div>



        <br />
        <div class="row col-lg-12">
            <div class="col-lg-6">
                <h3>Financial Transactions</h3>
                <hr />

                <div class="row">
                    <div class="col-sm-2">OrderID</div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control" placeholder="Order Id"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class="col-sm-2">Amount</div>
                    <div class="col-sm-8">

                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">$</span>
                            </div>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="Amount"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class="col-sm-2">TxnNumber</div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtTxnNumber" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class="col-sm-2">Entry Method</div>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="drpEntryMethod" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Default" Value="A" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="C (swiped/inserted/tapped credit cards)" Value="C"></asp:ListItem>
                            <asp:ListItem Text="D (swiped/inserted/tapped debit cards)" Value="D"></asp:ListItem>
                            <asp:ListItem Text="E (swiped/inserted credit and debit cards)" Value="E"></asp:ListItem>
                            <asp:ListItem Text="M (manually-entered credit cards)" Value="M"></asp:ListItem>
                            <asp:ListItem Text="B swipe / insert debit and credit cards – cashback only" Value="B"></asp:ListItem>
                            <asp:ListItem Text="T tapped credit or debit cards" Value="T"></asp:ListItem>
                            <asp:ListItem Text="U swipe only for Assisted UnionPay cards" Value="U"></asp:ListItem>
                            <asp:ListItem Text="W manually-entered Assisted UnionPay cards for Mail Order/Telephone Order Purchase transactions" Value="W"></asp:ListItem>
                            <asp:ListItem Text="Z swipe / insert / tap with cashback" Value="Z"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row" style="padding-top: 10px;">
                    <div class="col-sm-2">Original Auth #</div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtOriginalInvNumber" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                        <%--<p class="text-info small">*** use this for force post</p>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">MOTO</div>
                    <div class="col-sm-8">
                        <asp:CheckBox ID="chkMoto" runat="server" AutoPostBack="false" />
                    </div>
                </div>


                <div class="row" style="padding-top: 10px;">
                    <div class="col-sm-2">Echo Data</div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtEchoData" runat="server"></asp:TextBox>
                    </div>
                </div>
                <hr />

                <div class="row">
                    <div class="col-sm-12">
                        <asp:Button ID="btnPurchase" runat="server" OnClick="btnPurchase_Click" Text="Purchase" CssClass="btn btn-dark" />
                        <asp:Button ID="btnPreauth" runat="server" OnClick="btnPreauth_Click" Text="Pre Auth" CssClass="btn btn-dark" />
                        <asp:Button ID="btnCompletion" runat="server" OnClick="btnCompletion_Click" Text="Completion" CssClass="btn btn-dark" />
                        <asp:Button ID="btnRefund" runat="server" OnClick="btnRefund_Click" Text="Refund" CssClass="btn btn-dark" />
                        <asp:Button ID="btnVoid" runat="server" OnClick="btnVoid_Click" Text="Void" CssClass="btn btn-dark" />
                        <asp:Button ID="btnIndRefund" runat="server" OnClick="btnIndRefund_Click" Text="Ind Refund" CssClass="btn btn-dark" />
                        <asp:Button ID="btnFpost" runat="server" OnClick="btnFpost_Click" Text="Force Post" CssClass="btn btn-dark" />
                        <%--<asp:Button ID="btnBalanceInquiery" runat="server" OnClick="btnBalanceInquiery_Click" Text="Balance inquiry" CssClass="btn btn-dark" />--%>
                    </div>
                </div>

                <hr />
                <h3>Gift Card Transactions</h3>
                <hr />
                <div class="row">
                    <div class=" col-sm-12">
                        <div class="row">
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtLowPrefix" runat="server" placeholder="Low Prefix" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                <asp:TextBox ID="txtHighPrefix" runat="server" placeholder="High Prefix" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnaddBinRnage" runat="server" OnClick="btnaddBinRnage_Click" Text="Add Range" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class=" col-sm-12">
                        <div class="row">
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtDelLowRange" runat="server" placeholder="Low Prefix" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                <asp:TextBox ID="txtDelHighRange" runat="server" placeholder="High Prefix" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnDeleteRange" runat="server" OnClick="btnDeleteRange_Click" Text="Delete Range" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class=" col-sm-12">
                        <div class="row">
                            <div class="col-sm-8">
                                It uses Terminal # from Terminal ID Field
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnGetBinRange" runat="server" OnClick="btnGetBinRange_Click" Text="Get Range" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class=" col-sm-12">
                        <div class="row">
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtEntryOption" runat="server" placeholder="Entry Options" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                <asp:TextBox ID="txtTrack" runat="server" placeholder="Track #" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnGiftCardRead" runat="server" OnClick="btnGiftCardRead_Click" Text="Card Read" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-lg-6">
                <h3>Admin Transactions</h3>

                <hr />

                <div class="row col-sm-12">
                    <div class="col-sm-2">
                        Token
                    </div>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtPairingToken" runat="server" placeholder="Pairing Token" CssClass="form-control"></asp:TextBox>

                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnPairing" runat="server" OnClick="btnPairing_Click" Text="Pair" CssClass="btn btn-secondary" />
                        <asp:Button ID="btnunpair" runat="server" OnClick="btnunpair_Click" Text="Unpair" CssClass="btn btn-secondary" />
                    </div>
                </div>

                <div class="row col-sm-12" style="padding-top: 10px;">
                    <div class="col-sm-2">
                        Cashback Mode
                    </div>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="drpCashbackMode" runat="server" CssClass="form-control form-group" AutoPostBack="true" OnSelectedIndexChanged="drpCashbackMode_SelectedIndexChanged">
                            <asp:ListItem Text="Cashback is disabled (default)" Value="CD"></asp:ListItem>
                            <asp:ListItem Text="Debit Cashback is enabled" Value="CE"></asp:ListItem>
                            <asp:ListItem Text="Credit Cashback is enabled" Value="DE"></asp:ListItem>
                            <asp:ListItem Text="Credit and Debit Cashback is enabled" Value="HE"></asp:ListItem>
                        </asp:DropDownList>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">$</span>
                            </div>
                            <asp:TextBox ID="txtDebit" runat="server" Enabled="false" placeholder="Debit Cashback Limit" CssClass="form-control form-group"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">.00</span>
                            </div>
                        </div>

                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">$</span>
                            </div>
                            <asp:TextBox ID="txtVisaCashBackLimit" runat="server" Enabled="false" placeholder="VISA cashback Limit" CssClass="form-control form-group"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">.00</span>
                            </div>
                        </div>

                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">$</span>
                            </div>
                            <asp:TextBox ID="txtMCCashBackLimit" runat="server" Enabled="false" placeholder="MC cashback Limit" CssClass="form-control form-group"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">.00</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnCashback" runat="server" OnClick="btnCashback_Click" Text="Cashback" CssClass="btn btn-secondary" />
                    </div>
                </div>
                <div class="row col-sm-12">
                    <div class="col-sm-2">
                        Surcharge Mode
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="drpSurcharge" runat="server" CssClass="form-control form-group" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpSurcharge_SelectedIndexChanged">
                            <asp:ListItem Text="Disable Surcharge" Value="SD"></asp:ListItem>
                            <asp:ListItem Text="Enable surcharge on Cashback" Value="SC"></asp:ListItem>
                            <asp:ListItem Text="Enable surcharge on debit" Value="SE"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">$</span>
                            </div>
                            <asp:TextBox ID="txtSurcharge" runat="server" Enabled="false" placeholder="Surcharge Fee (0.00)" CssClass="form-control form-group"></asp:TextBox>
                            <%--  <div class="input-group-append">
                                <span class="input-group-text">.00</span>
                            </div>--%>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnSurcharge" runat="server" OnClick="btnSurcharge_Click" Text="Surcharge" CssClass="btn btn-secondary" />
                    </div>
                </div>


                <div class="row col-sm-12">
                    <div class="col-sm-2">
                        Tip Type
                    </div>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="drpSetTip" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Default" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="TF (No Tip)" Value="TF"></asp:ListItem>
                            <asp:ListItem Text="TB ($ on Credit and Debit)" Value="TB"></asp:ListItem>
                            <asp:ListItem Text="TC (% on Credit and Debit)" Value="TC"></asp:ListItem>
                            <asp:ListItem Text="TH ($ and % option on Credit and Debit)" Value="TH"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnSetTip" runat="server" OnClick="btnSetTip_Click" Text="SetTip" CssClass="btn btn-secondary" />
                    </div>
                </div>

                <div class="row col-sm-12" style="padding-top: 10px;">
                    <div class="col-sm-2">
                        MPOS
                    </div>
                    <div class="col-sm-10">
                        <asp:CheckBox ID="chkmPOS" runat="server" AutoPostBack="false" CssClass="form-check-inline" />
                        <asp:Button ID="btnMPOS" runat="server" Text="E/D MPOS" OnClick="btnMPOS_Click" CssClass="btn btn-secondary " />
                    </div>
                </div>
                <div class="row col-sm-12" style="padding-top: 10px;">
                    <div class="col-sm-2">
                        PAN Mask
                    </div>
                    <div class="col-sm-10">
                        <asp:CheckBox ID="chkPanMask" runat="server" AutoPostBack="false" CssClass="form-check-inline" />
                        <asp:Button ID="btnPanMask" runat="server" Text="E/D Mask PAN" OnClick="btnPanMask_Click" CssClass="btn btn-secondary " />
                    </div>
                </div>
                <div class="row col-sm-12" style="padding-top: 10px;">
                    <div class="col-sm-2">
                        Barcode Scan
                    </div>
                    <div class="col-sm-8">
                        <div class="card" style="width: 35rem;">
                            <div class="card-body">

                                <%--<h5 class="card-title">Card title</h5>--%>
                                <h6 class="card-subtitle mb-2 text-muted">Scanning Details</h6>
                                <div class="col-sm-12 row">
                                    <div class="row col-sm-6">
                                        <asp:TextBox ID="txtdisplayTimeOut" runat="server" placeholder="Display Timeout in seconds" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="row col-sm-6">
                                        <asp:TextBox ID="txtScanType" runat="server" placeholder="‘C’ = Scanning starts automatically" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12 row">
                                    <div class="row col-sm-6">
                                        <asp:TextBox ID="txtPrompt1" runat="server" placeholder="Prompt 1" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="row col-sm-6">
                                        <asp:TextBox ID="txtPrompt2" runat="server" placeholder="Prompt 2" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12 row">
                                    <div class="row col-sm-6">
                                        <asp:TextBox ID="txtPrompt3" runat="server" placeholder="Prompt 3" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="row col-sm-6">
                                        <asp:TextBox ID="txtPrompt4" runat="server" placeholder="Prompt 4" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>


                                <%--<a href="#" class="card-link">Card link</a>
                                <a href="#" class="card-link">Another link</a>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnBarCodeScan" runat="server" Text="Barcode Scan" OnClick="btnBarCodeScan_Click" CssClass="btn btn-secondary " />
                    </div>
                </div>

                <hr />


                <%--<div class="row col-sm-12" style="padding-top: 10px">
                    <p class="text-info small">*** When use Set tip please use Tip Type drop from Financial Transaction</p>
                </div>--%>
            </div>
        </div>

        <div class="col-lg-12">
            <asp:Label ID="lblRequestUrl" runat="server"></asp:Label>
        </div>

        <div class="row col-lg-12">
            <table class="table fixed" style="width: 100%; max-height: 500px;">
                <thead>
                    <tr>
                        <td>
                            <b>Request</b>
                           
                           <%-- <img src="../Images/box_edit-512.png" height="20" width="20" id="imgEdit"  />
                            <img src="../Images/Icon_33-512.png" height="20" width="20" id="imgAccept" hidden="hidden" />--%>
                        </td>
                        <td><b>Response</b></td>
                        <td><b>Receipt</b></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtRequest" Enabled="false" Rows="20"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtRespose" Enabled="false" Rows="20"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtPollingReceipt" Enabled="false" Rows="20"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


        <div class="row col-lg-12">
            <asp:Button ID="btnPool" runat="server" OnClick="btnPool_Click" Text="Polling Receipt" CssClass="btn btn-danger" />
            <%--<asp:Button runat="server" ID="btnSaveDB" OnClick="btnSaveDB_Click" Text=" Save To DB" CssClass="btn btn-primary" />--%>
            <%--<asp:Label runat="server" ID="lblDbSave"></asp:Label>--%>
        </div>


        <div class="row col-lg-12">
            <%--<asp:TextBox ID="txtTranNote" runat="server" CssClass="form-control" placeholder="Comment to save in DB"></asp:TextBox>--%>
        </div>
        <div>
            <asp:Label ID="lblFollowOn" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
