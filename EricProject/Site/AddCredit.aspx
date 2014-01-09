<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="AddCredit.aspx.cs" Inherits="Site_AddCredit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Add Credit</title>
 
    <script type="text/javascript">
        if (location.protocol !== "https:")
            location.protocol = "https:";
        function confirm(id, Credits, Amount) {

            // alert('You have some pending credits which will be automatically deducted from your account after purchase.');
            Credit(id, Credits, Amount)
            document.getElementById("Divnegativecredits").style.display = "block";
        }
        function Credit(id, Credits, Amount) {
            document.getElementById("<%=Creditdiv.ClientID%>").style.display = "block";
            var txtCardholderName = document.getElementById("<%=txtCardholderName.ClientID%>");
            var lblCredits = document.getElementById("<%=lblCredits.ClientID%>");
            var lblAmount = document.getElementById("<%=lblAmount.ClientID%>");
            var CreditPlanID = document.getElementById("<%=CreditPlanID.ClientID%>");
            var DollarAmount = document.getElementById("<%=DollarAmount.ClientID%>");
            var CreditsHidden = document.getElementById("<%=CreditsHidden.ClientID%>");
            var CardHolderFirstName = document.getElementById("<%=CardHolderFirstName.ClientID%>");
            var cardHolderLastName = document.getElementById("<%=cardHolderLastName.ClientID%>");
            CreditPlanID.value = id;
            lblCredits.innerHTML = Credits.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace('.00', '');
            CreditsHidden.value = Credits.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace('.00', '');
            lblAmount.innerHTML = "$" + Amount + ".00";
            DollarAmount.value = Amount + ".00";
            if (CardHolderFirstName.value != '') {
                txtCardholderName.value = CardHolderFirstName.value + ' ' + cardHolderLastName.value;
            }
            //window.location.href = "#tp";
            txtCardholderName.focus();
        }
        function ChangeMerchant(status) {
            var hiddenradio = document.getElementById("<%=hiddenradio.ClientID%>");
            hiddenradio.value = status;
        }
        function ajaxloader() {
            document.getElementById("imgloader").src = "/images/ajax-loader.gif";
            document.getElementById("<%=overlay.ClientID%>").style.display = "block";
            document.getElementById("<%=progressdiv.ClientID%>").style.display = "block";
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "Only Numeric allowed";
                return false;
            }

            return true;
        }
        function CheckDate() {
            var d = new Date();
            var m = d.getMonth() + 1;
            var n = m < 10 ? '0' + m : m
            var yyyy = d.getFullYear();
            var month = document.getElementById("<%=ddlMonth.ClientID %>").options[document.getElementById("<%=ddlMonth.ClientID%>").selectedIndex].text;
           var year = document.getElementById("<%=ddlyear.ClientID %>").options[document.getElementById("<%=ddlyear.ClientID%>").selectedIndex].text;
           var date = month + "20" + year;

           var CardHolderName = document.getElementById('<%=txtCardholderName.ClientID%>').value;
           var CardNumber = document.getElementById('<%=txtCardnumber.ClientID%>').value;

           if (CardHolderName.trim() == '') {
               document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "Card Holder Name can not be blank";
               return false;
           }

           if (CardNumber.trim() == '') {
               document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "Card Number can not be blank";
               return false;
           }
            if (CardNumber.trim().length < 14) {
                document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "Minimum Length of card number is 14";
                 return false;
             }
           if ("20" + year < yyyy) {
               document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "Expiry Date Should be greater than today's Date";
                return false;
            }
            else if ("20" + year == yyyy) {
                if (month < n) {
                    document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "Expiry Date Should be greater than today's Date";
                    return false;
                }
                else
                    ajaxloader();
            }
            else
                ajaxloader();
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--<!--  Start topbluStrip -->
   <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard"
                    ><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign">
                    <span>Campaigns</span></a></li>
                <li><a href="#"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ManageCredits" class="sel">
                    <span>Credits</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  End topbluStrip -->
    <!--  / banner container \ -->

    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <h2 class="bluhed">
                            <span class="fl">Purchase Credits</span> <span class="grnlink fr">&nbsp;</span>
                        </h2>
                        <!--Start midInnercont -->
                        <div class="midInnercont" style="height:230px;">
                            <asp:Literal ID="litPlan" runat="server"></asp:Literal>
                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->--%>
    <!--  Start topbluStrip -->
    <%--    <div class="topbluStrip">
      <div class="inner">
	  	<div class="searchFaqBox">
			<h2>Add Credits</h2>												
		</div>
	  </div>
    </div>--%>
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails"
                    class="sel"><span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  End topbluStrip -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner" style="margin-left: 14px;">
                <div class="bottom">
                    <div class="mid">
                        <div class="redBar" runat="server" id="redbar" visible="false" style="margin-bottom: 10px;">
                            <div class="lt">
                                <div class="rt">
                                    <img src="/images/wrong_icon.jpg" alt="" />
                                    Transaction Failed. Please check your credit card details.
                                </div>
                            </div>
                        </div>

                        <div class="spacer"></div>
                        <!--Start innaerTabel -->
                        <div class="innerTabelbg">
                            <div class="toppartSml">
                                <div class="botpartSml">

                                    <div class="innerTabel">

                                        <div class="tabelbluhed">Credit Cost</div>
                                        <div id="innerTabelDiv" runat="server">
                                            <%--<table cellpadding="0" cellspacing="0" border="0" width="100%" class="creditrow">
										<tr>
											<td width="26%">09 <span class="blufont">Credits</span></td>
											<td width="62%"><span class="grnfont">$10.00</span></td>
											<td width="12%">
											<div class="grnbutton"><a href="#"><span>Buy Now</span></a></div>
											</td>
										</tr>
										<tr>
											<td>24 <span class="blufont">Credits</span></td>
											<td><span class="grnfont">$25.00</span></td>
											<td>
											<div class="grnbutton"><a href="#"><span>Buy Now</span></a></div>
											</td>
										</tr>
										<tr class="lastrow">
											<td>49 <span class="blufont">Credits</span></td>
											<td><span class="grnfont">$50.00</span></td>
											<td>
											<div class="grnbutton"><a href="#"><span>Buy Now</span></a></div>
											</td>
										</tr>																				
									</table>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Ebd innaerTabe 1-->
                        <div class="spacer"></div>
                        <div class="redBar" id="Divnegativecredits" style="margin-bottom: 10px; display: none;">
                            <div class="lt">
                                <div class="rt">
                                    <img src="/images/info_icon.jpg" alt="" />
                                    You have some pending credits which will be automatically deducted from your account after purchase.
                                </div>
                            </div>
                        </div>
                        <div class="shedulebox" id="Creditdiv" runat="server" style="display: none;">

                            <div class="innerTabelbg">
                                <div class="toppartSml">
                                    <div class="botpartSml">
                                        <div class="tabelbluhed">Payment Details</div>
                                        <table style="width: 100%" cellpadding="0" cellspacing="10">
                                            <tr>
                                                <td style="width: 150px;">Card Holder Name</td>
                                                <td>
                                                    <asp:TextBox ID="txtCardholderName" runat="server" Width="50%" CssClass="inptredeem"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px;">Card Number</td>
                                                <td>
                                                    <asp:TextBox ID="txtCardnumber" runat="server" onkeypress="return isNumberKey(event)" Width="15%" CssClass="inptredeem" MaxLength="16"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px;">Expiry Date</td>
                                                <td>
                                                    <%-- <asp:TextBox ID="txtExpiryDate" runat="server" Width="6%" CssClass="inptredeem" MaxLength="4">

                                            </asp:TextBox>&nbsp; &nbsp;<span style="margin-top: 5px; display: inline-block;">(MMYY)</span>--%>
                                                    <asp:DropDownList ID="ddlMonth" runat="server">
                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlyear" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 27px;">Credits</td>
                                                <td>
                                                    <asp:Label ID="lblCredits" runat="server" Width="100%"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 27px;">Amount</td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Width="100%"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="detailLine" runat="server" id="autoreplenish" visible="false">
                                                        <div class="autohd" style="display: inline-block; width: 155px;">Auto-Replenish</div>
                                                        <input type="radio" name="rbtauto" class="vAlign" id="rbtautoon" checked="checked" onclick="ChangeMerchant(1)" />
                                                        <span class="radiotxt">On</span>
                                                        <input type="radio" name="rbtauto" class="vAlign" id="rbtautooff" onclick="ChangeMerchant(0)" />
                                                        <span class="radiotxt">Off</span>
                                                    </div>
                                                </td>
                                            </tr>
                                          
                                            <tr>
                                                <td> </td>
                                                <td  style="text-align: right">
                                               <span style="margin-right:460px;">    <asp:Label runat="server" ID="lblmessage" ForeColor="Red"></asp:Label></span> <asp:Button ID="btnRedeem" runat="server" Text="Buy Credits" class="formbtnBig" OnClientClick="return CheckDate();" OnClick="btnRedeem_Click" /></td>
                                            </tr>
                                        </table>
                                        <div class="clr"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a href="#" name="tp" style="height: 1px; width: 1px">&nbsp;</a>
                        <div class="tablebotTxt">All Credits can be refunded any time at $1 for every 100 Credits</div>

                        <div class="botspaceInner">
                            <div class="midbottgrybg">
                                <div class="bottxt">You would like to <strong>Request Refund?</strong> <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Refund">Click here</a></div>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="CreditPlanID" />
                        <asp:HiddenField runat="server" ID="DollarAmount" />
                        <asp:HiddenField runat="server" ID="CreditsHidden" />
                        <asp:HiddenField runat="server" ID="CardHolderFirstName" />
                        <asp:HiddenField runat="server" ID="cardHolderLastName" />
                        <asp:HiddenField runat="server" ID="hiddenMerchantID" />
                        <asp:HiddenField runat="server" ID="hiddenpageurl" />
                        <asp:HiddenField runat="server" ID="hiddenradio" Value="1" />
                    </div>
                </div>
                <!--  \ midInner container / -->
            </div>
        </div>
        <!--  \ content container / -->
    </div>
    <div id="overlay" runat="server" style="position: fixed; width: 100%; height: 100%; background-color: black; opacity: 0.5; filter: alpha(opacity=50); top: 0px; left: 0px; text-align: center; z-index: 1">
    </div>
    <div id="progressdiv" runat="server" style="position: fixed; top: 200px; width: 100%; z-index: 2" align="center">
        <div style="width: 300px; height: 200px;">
            <center>
                <%--                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/ajax-loader.gif" width="100px" height="100px" alt="" /><br />--%>
                <img id="imgloader" width="100px" height="100px" src="/images/ajax-loader.gif" /><br />
                <span style="color: white; font-weight: bold; font-size: medium;">Processing Your Transaction</span>
            </center>
        </div>
    </div>
</asp:Content>
