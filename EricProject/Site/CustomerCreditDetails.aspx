<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteCustomer.Master" AutoEventWireup="true" CodeBehind="CustomerCreditDetails.aspx.cs" Inherits="EricProject.Site.CustomerCreditDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CustomerCreditDetails</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/CustomerCreditDetails.css"
        type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"
        type="text/javascript"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"
        type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"
        type="text/javascript"></script>

    <!--For color picker -->
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorpicker.css"
        type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/colorpicker.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/eye.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/utils.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/layout.js"></script>
    <!--For color picker (End)-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <asp:HiddenField ID="hfPageUrl" runat="server" />
    <div class="topbluStrip">
        <div class="inner">
            <div class="fl">
                <ul class="nav">
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Dashboard"><span>Dashboard</span></a></li>
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CreditDetails" class="sel"><span>Credit Details</span></a></li>
                </ul>
                <div class="clr"></div>
            </div>
            <div class="toprgtTxt" style="margin-top: 14px">
                <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CustomerReferral" style="color: white;">Get 5,000 Credits,Refer a Merchant</a>
            </div>
        </div>
    </div>
    <!--  End topbluStrip -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="blanktpbox">
                            <div class="fl">
                                <h2 class="bluhed">My Earnings</h2>
                            </div>
                            <div class="sortbytabs">
                                <ul class="sortby">
                                    <li class="first">Filter By:</li>
                                    <li>
                                        <asp:LinkButton ID="lnkAll" runat="server" OnClick="lnkAll_Click"><span>ALL</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkMyReferrals" runat="server"
                                            OnClick="lnkMyReferrals_Click"><span>REFERRALS</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkMyRebates" runat="server"
                                            OnClick="lnkMyRebates_Click"><span>REBATES</span></asp:LinkButton></li>
                                    <li class="last">
                                        <asp:LinkButton ID="lnkRedeeemed" runat="server" OnClick="lnkRedeeemed_Click"><span>Redeeemed</span></asp:LinkButton></li>

                                </ul>
                                <div class="clr"></div>
                            </div>
                            <div class="clr"></div>
                        </div>
                        <!--Start innaerTabel -->
                        <div class="innerTabelbg">
                            <div class="toppartSml">
                                <div class="botpart">
                                    <div class="innerTabel" id="alltable" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Ebd innaerTabe 1-->
                        <a href="#" name="tp" style="height: 1px; width: 1px">&nbsp;</a>
                        <!--Start innaerTabe2 -->
                        <div class="innerTabelbg">
                            <div class="toppartSml">
                                <div class="botpartSml">
                                    <div class="inSpace">
                                        <div class="blusmlHd">
                                            <div class="fl">Redeem Schedule</div>
                                         <%--   <div class="fr"><a href="#">View History</a></div>--%>
                                            <div class="clr"></div>
                                        </div>
                                        <div class="shedulebox">
                                            <ul class="blubg">
                                                <% if (UnredeemedCredits >= 500)
                                                   { %>
                                                <li style="cursor: pointer;" onclick="RedeemCredits(500,5)"><span><span class="rt">500<label>Credits</label></span></span>
                                                    <label class="grn">$5.00</label></li>
                                                <%}
                                                   else
                                                   { %>
                                                <li style="cursor: pointer;" onclick="alert('You must have atleast 500 credits to redeem.')"><span><span class="rt">500<label>Credits</label></span></span>
                                                    <label class="grn">$5.00</label></li>
                                                <%} %>
                                                <% if (UnredeemedCredits >= 1000)
                                                   { %>
                                                <li style="cursor: pointer;" onclick="RedeemCredits(1000,10)"><span><span class="rt">1,000<label>Credits</label></span></span>
                                                    <label class="grn">$10.00</label></li>
                                                <%}
                                                   else
                                                   { %>
                                                <li style="cursor: pointer;" onclick="alert('You must have atleast 1,000 credits to redeem.')"><span><span class="rt">1,000<label>Credits</label></span></span>
                                                    <label class="grn">$10.00</label></li>
                                                <%} %>
                                                <% if (UnredeemedCredits >= 5000)
                                                   { %>
                                                <li style="cursor: pointer;" onclick="RedeemCredits(5000,50)"><span><span class="rt">5,000<label>Credits</label></span></span>
                                                    <label class="grn">$50.00</label></li>
                                                <%}
                                                   else
                                                   { %>
                                                <li style="cursor: pointer;" onclick="alert('You must have atleast 5,000 credits to redeem.')"><span><span class="rt">5,000<label>Credits</label></span></span>
                                                    <label class="grn">$50.00</label></li>
                                                <%} %>
                                                <% if (UnredeemedCredits >= 10000)
                                                   { %>
                                                <li style="cursor: pointer;" onclick="RedeemCredits(10000,100)"><span><span class="rt">10,000<label>Credits</label></span></span>
                                                    <label class="grn">$100.00</label></li>
                                                <%}
                                                   else
                                                   { %>
                                                <li style="cursor: pointer;" onclick="alert('You must have atleast 10,000 credits to redeem.')"><span><span class="rt">10,000<label>Credits</label></span></span>
                                                    <label class="grn">$100.00</label></li>
                                                <%} %>
                                                <% if (UnredeemedCredits >= 50000)
                                                   { %>
                                                <li style="cursor: pointer;" onclick="RedeemCredits(50000,500)"><span><span class="rt">50,000<label>Credits</label></span></span>
                                                    <label class="grn">$500.00</label></li>
                                                <%}
                                                   else
                                                   { %>
                                                <li style="cursor: pointer;" onclick="alert('You must have atleast 50,000 credits to redeem.')"><span><span class="rt">50,000<label>Credits</label></span></span>
                                                    <label class="grn">$500.00</label></li>
                                                <%} %>
                                            </ul>
                                            <div class="clr"></div>
                                        </div>
                                        <div class="shedulebox" id="redeemdiv" runat="server" visible="false">
                                            <table style="width: 100%" cellpadding="0" cellspacing="10">
                                                <tr>
                                                    <td style="width: 150px;">Paypal Username</td>
                                                    <td>
                                                        <asp:Label ID="txtPaypalUsername" runat="server" Width="50%"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Credits</td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Width="100%"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Amount</td>
                                                    <td>
                                                        <asp:Label ID="lblAmount" runat="server" Width="100%"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:Button ID="btnRedeem" runat="server" Text="Redeem Credits" class="formbtnBig" OnClientClick="ajaxloader();" OnClick="btnRedeem_Click" /></td>
                                                </tr>
                                            </table>
                                            <div class="clr"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--End innaerTabe2 -->
                        </div>
                    </div>
                </div>
                <!--  \ midInner container / -->
            </div>
        </div>
        <!--  \ content container / -->
    </div>
         <div id="overlay" runat="server" style="position: fixed; width: 100%; height: 100%; background-color: black; opacity:0.5;filter:alpha(opacity=50); top: 0px; left: 0px; text-align: center; z-index: 1; display:none">
    </div>
     <div id="progressdiv" runat="server" style="position: fixed; top: 200px; width: 100%; z-index: 2;display:none;" align="center">
        <div style="width: 300px; height: 200px;">
            <center>
                <img id="imgloader" width="100px" height="100px" alt="" /><br />
               <span style="color:white; font-weight:bold;font-size:medium;"> Processing Your Transaction</span>
            </center>
        </div>
    </div>
    <asp:HiddenField ID="hiddenCredits" runat="server" Value="" />
    <script type="text/javascript">
        function ajaxloader() {
            document.getElementById("imgloader").src = "<%=ConfigurationManager.AppSettings["pageURL"] %>images/ajax-loader.gif";
            document.getElementById("<%=overlay.ClientID%>").style.display = "block";
               document.getElementById("<%=progressdiv.ClientID%>").style.display = "block";
           }
        function RedeemCredits(c, a) {
            var values = new Array();
            values[0] = c;
            values[1] = a;
            EricProject.WebServices.Admin.SetPaypalCredits(values, onpaypalsuccess);
            var hostname = "https://sandbox.paypal.com/webapps/auth/protocol/openidconnect/v1/authorize?";
            var url = "client_id=AcBjtBB6OFzYFX-eLEpQFAD0ZUKHdcDBL9KZfCw6w3GKFP4mtqDwfMT3Ay6w&response_type=code&scope=openid profile email&redirect_uri=<%=ConfigurationManager.AppSettings["pageURL"] %>Site/CreditRedeem.aspx";
            window.open(hostname + encodeURI(url), "paypal", "width=400,height=550,location=0,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }
        function onpaypalsuccess(result) {
        }
        function RedirectCustomerReferenceId(TransactionId, CustomerId) {
            var Transaction = new Array();
            Transaction[0] = TransactionId;
            Transaction[1] = CustomerId;
            EricProject.WebServices.Admin.SetSessionCustomerTransactionId(Transaction, onSuccess);
        }
        function onSuccess(result) {
            window.location.href = document.getElementById('<%=hfPageUrl.ClientID %>').value + "Site/Customer/CustomerTransactionHistory";
        }
        function gethiddenCredits() {
            return document.getElementById('<%=hiddenCredits.ClientID%>').value;
        }
        function redirect(url) {
            window.location.href = url;
        }
        function RedirectCustomerReferenceIdRedeem(TransactionId, CustomerId) {
            var Transaction = new Array();
            Transaction[0] = TransactionId;
            Transaction[1] = CustomerId;
            EricProject.WebServices.Admin.SetSessionCustomerTransactionIdRedeem(Transaction, onSuccess1);
        }
        function onSuccess1(result) {
            window.location.href = document.getElementById('<%=hfPageUrl.ClientID %>').value + "Site/Custometer/RedeemDetails";
        }
    </script>
</asp:Content>
