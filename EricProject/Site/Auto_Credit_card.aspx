<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Auto_Credit_card.aspx.cs" Inherits="EricProject.Site.Auto_Credit_card" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PaymentDetails</title>
      <script type="text/javascript" language="javascript" >
          if (location.protocol !== "https:")
              location.protocol = "https:";
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
                document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "It looks like your card has expired";
                return false;
            }
            else if ("20" + year == yyyy) {
                if (month < n) {
                    document.getElementById("<%=lblmessage.ClientID %>").innerHTML = "It looks like your card has expired";
                    return false;
                }
                else
                    return true;
            }
            else
                return true;
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
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails"
                    class="sel"><span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
     <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner" style="margin-left: 14px;">
                <div class="bottom">
                    <div class="mid"   style="min-height: 350px;">
         <div class="shedulebox" id="Creditdiv">
                           
                                 <div class="innerTabelbg">
                                        <div class="toppartSml">
                                                 <div class="botpartSml"  style="min-height: 350px;">
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
                                        <asp:TextBox ID="txtCardnumber" runat="server" onkeypress="return isNumberKey(event)" Width="16%" CssClass="inptredeem" MaxLength="16"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Expiry Date</td>
                                                <td>
                                                    <%-- <asp:TextBox ID="txtExpiryDate" runat="server" Width="6%" CssClass="inptredeem" MaxLength="4">

                                            </asp:TextBox>&nbsp; &nbsp;<span style="margin-top: 5px; display: inline-block;">(MMYY)</span>--%>
                                                    <asp:DropDownList ID="ddlMonth"  runat="server">
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
                                                    <asp:DropDownList ID="ddlyear" runat ="server"></asp:DropDownList>
                                                </td>
                                    </tr>
                                <tr>
                                  <td></td>
                                    <td style="text-align: left ">
                                        <asp:Label runat="server" ID="lblmessage" ForeColor="Red" ></asp:Label>
                                    </td>
                                </tr>
                            
                                <tr>
                                           <td  style="text-align: left">
                                        <asp:Button ID="btnback" runat="server" Text="Back"  class="formbtnBig" OnClick="btnback_Click" /></td>
                                    
                                    <td  style="text-align: right;">
                                        <asp:Button ID="btnRedeem" runat="server" Text="Continue" OnClientClick="return CheckDate();" OnClick="btnRedeem_Click" class="formbtnBig" /></td>
                                </tr>
                            </table>
                            <div class="clr"></div>
                   </div>   </div>  </div>
                        </div> </div>   </div>  </div>
                        </div></div>
</asp:Content>
