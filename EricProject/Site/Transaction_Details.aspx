<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Transaction_Details.aspx.cs" Inherits="EricProject.Site.Transaction_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <title>Transaction Details</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css" type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/colorbox.css" type="text/css" />
    <%--<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/fonts.css" type="text/css" />
    --%><script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>

    <%--<script type="text/javascript">
    var j = jQuery.noConflict();
    j(document).ready(function () {
        j(".popup1").colorbox({ width: "410px", height: "400px", background: "none", iframe: true });
        j(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //Example of preserving a JavaScript event for inline calls.
        j("#click").click(function () {
            j('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
            return false;
        });
    });
</script>--%>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/ddaccordion.js"></script>

    <%--<script type="text/javascript">
$(document).ready(function() {
	
	$(".log_out").click(function(e) {          
		e.preventDefault();
		$("div#log_act").toggle();
		$(".log_out").toggleClass("menu-open");
	});
	
	$("div#log_act").mouseup(function() {
		return false
	});
	$(document).mouseup(function(e) {
		if($(e.target).parent("a.log_out").length==0) {
			$(".log_out").removeClass("menu-open");
			$("div#log_act").hide();
		}
	});			
});
</script>--%>
    <!--For color picker -->
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorpicker.css" type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/colorpicker.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/eye.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/utils.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/layout.js"></script>
    <!--For color picker (End)-->
    <%--#cboxClose { top:10px; right:-6px;}--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:HiddenField ID="HiddenTransactionStatus" runat="server" />
    <!--  \ banner container / -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel"><span>Account</span></a></li>
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
                    <div class="mid">

                        <!--Start innaerTabel -->
                        <div class="innerTabelbg" id="DivNoData" runat="server">
                            <div class="toppartSml">
                                <div class="botgraypart">

                                    <div class="innerTabel">

                                        <div class="grysmlHd">
                                            Order Details -  
                                            <asp:Literal ID="transaction_Date" runat="server"></asp:Literal>
                                        </div>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="borderTabel">
                                            <tr class="blutop">
                                                <td width="26%">ITEM</td>
                                                <td width="16%">QUANTITY</td>
                                                <td width="14%">SKU</td>
                                                <td width="15%">UNIT PRICE</td>
                                                <td width="15%">PRICE</td>
                                                <%--<td width="14%">REBATE</td>--%>
                                            </tr>
                                            <asp:Literal ID="litTransactionHistory" runat="server"></asp:Literal>
                                            <tr class="blank">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <%--       <td>&nbsp;</td>--%>
                                                <td>Order Subtotal:</td>
                                                <asp:Literal ID="OrderSubtotal" runat="server"></asp:Literal>
                                            </tr>
                                            <tr class="blank">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <%--       <td>&nbsp;</td>--%>
                                                <td>Tax:</td>
                                                <asp:Literal ID="Tax" runat="server"></asp:Literal>
                                            </tr>
                                            <tr class="blank">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <%--       <td>&nbsp;</td>--%>
                                                <td>Shipping Charge: </td>
                                                <asp:Literal ID="ShippingCharege" runat="server"></asp:Literal>
                                            </tr>
                                            <tr class="lastrow">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <%--       <td>&nbsp;</td>--%>
                                                <td>Total:</td>
                                                <asp:Literal ID="Total" runat="server"></asp:Literal>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Ebd innaerTabe 1-->
                        <div class="spacer">&nbsp;</div>
                        <!--Start innaerTabe2 -->
                        <div class="innerTabelbg">
                            <div class="toppartSml">
                                <div class="botpartfree1">
                                    <div class="innerTabel">
                                        <div class="grysmlHd" style="margin-left: 6px;">Referral Details</div>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="borderTabel" style="margin-bottom: -5px; margin-top: -5px;">
                                            <tr class="blutop">
                                                <td width="25%">Campaign Name</td>
                                                <td width="15%">Type</td>
                                                <td width="15%">User</td>
                                                <td width="16%">Eligible Purchase</td>
                                                <td width="15%">Reward Offered</td>
                                                <td width="15%">Credits Rewarded</td>
                                            </tr>

                                            <asp:Literal ID="customerdetails" runat="server"></asp:Literal>
                                            <asp:Literal ID="ReferralDetails" runat="server"></asp:Literal>

                                            <tr class="blank">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>Transaction Fee: </td>
                                                <td class="last">
                                                    <div class="tabelbottom1">
                                                        <asp:Literal ID="TransactionFee" runat="server"></asp:Literal><br />
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr class="blank">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>Current Status: </td>
                                                <td class="last">
                                                    <div class="tabelbottom1">
                                                        <asp:Literal ID="Status" runat="server"></asp:Literal><br />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="fullwidth1" align="right">Total Credits Rewarded:</td>
                                                <td class="fullwidth1">
                                                    <asp:Literal ID="TotalCredits" runat="server"></asp:Literal></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="spacer">&nbsp;</div>
                        <div id="DivDeclinedReason" runat="server" style="margin-left: 8px; width: 940px;">
                            <b>Reason</b> : 
                            <asp:Literal ID="litDeclinedReason" runat="server"></asp:Literal>
                        </div>
                        <div class="fromFree fromFree1">
                            <!--<a href="deciline.html" class="popup1"><img src="images/decline.jpg" alt="" /></a>-->
                            <%--<a href="#" class="popup1 fr"><span>Decline</span></a>--%>
                            <asp:Button ID="btnBack" runat="server" Text="Back" class="formbotton" PostBackUrl="~/Site/Merchant/ManageCredits" />
                            <%--<a href="#" style="float: right;"><span>Decline</span></a>--%>
                            <asp:LinkButton ID="lbtnDecline" runat="server" Style="float: right;" PostBackUrl="~/Site/Merchant/DeclineTransaction"><span>Decline</span></asp:LinkButton>
                            <div class="clr"></div>
                        </div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->

    <!--  \ main container / -->
    <!--  / footer container \ -->
    <script type="text/javascript">
        window.onload = function () {
            if (document.getElementById('<%=HiddenTransactionStatus.ClientID%>').value == "Pending Payment" || document.getElementById('<%=HiddenTransactionStatus.ClientID%>').value == "Pending Vesting") {
                document.getElementById('<%=lbtnDecline.ClientID%>').style.display = "block";
            }
            else {
                document.getElementById('<%=lbtnDecline.ClientID%>').style.display = "none";
            }
        }
    </script>
</asp:Content>
