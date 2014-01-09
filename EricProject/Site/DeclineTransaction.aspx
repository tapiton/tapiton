<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeclineTransaction.aspx.cs" Inherits="EricProject.Site.DeclineTransaction" MasterPageFile="~/Master/SiteMerchant.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>Decline Transaction</title>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/merchantmain.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Validate() {
            if (document.getElementById('<%=txtReason.ClientID%>').value == "") {
                alert("Please provide reason to decline this transaction.");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel">
                    <span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid" style="height: 370px;">
                        <div class="midInnergrybg">
                            <h2>Decline Transaction
                            </h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont" style="padding: 76px 12px 80px 12px;">
                            <!--Start formBox -->
                            <div class="formBox">
                                <br />
                                <div class="formLine">
                                    <div class="formLabel">
                                        Credits Rewarded
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtCredits" runat="server" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Reason
                                    </div>
                                    <div class="formField">
                                        <div class="">
                                            <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="300" Height="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        &nbsp;
                                    </div>
                                    <div class="formField">
                                        <div class="">
                                            <asp:Button ID="btnDecline" runat="server"
                                                Text="Submit" class="formbotton" OnClick="btnDecline_Click" OnClientClick="return Validate();" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <%--</form>--%>
                            </div>
                            <!--End formBox -->
                            <div class="fromFree fromFree1">
                                <div class="clr"></div>

                            </div>
                        </div>
                        <!--End midInnercont -->
                        <div class="botspaceInner">
                            <div class="midbottgrybg">
                                <asp:Button ID="btnBack" runat="server" Text="Back" class="formbotton" PostBackUrl="~/Site/Merchant/TransactionDetails" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <!--  \ midInner container / -->
        </div>

    </div>

</asp:Content>
