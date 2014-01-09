<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Refund.aspx.cs" Inherits="EricProject.Site.Refund" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Refund</title>
    <script type="text/javascript" language="javascript">
        if (location.protocol !== "https:")
            location.protocol = "https:";
        function ajaxloader() {           
            if (document.getElementById("<%=txtAmount.ClientID%>").value.trim() == '') {
                document.getElementById('<%=Amountless.ClientID%>').innerHTML = "Please provide an amount greater than 0";
                return false;
            } //if (!document.getElementById("<%=txtAmount.ClientID%>").value.match("[0-9]+(\.[0-9][0-9]?)?"))
            if (!isNaN(document.getElementById("<%=txtAmount.ClientID%>").value) && document.getElementById("<%=txtAmount.ClientID%>").value.indexOf('.')<0)
            {
                document.getElementById("imgloader").src = "<%=ConfigurationManager.AppSettings["pageURL"]%>images/ajax-loader.gif";
                document.getElementById("<%=overlay.ClientID%>").style.display = "block";
                document.getElementById("<%=progressdiv.ClientID%>").style.display = "block";
                return true;
            }
            else {
                document.getElementById('<%=Amountless.ClientID%>').innerHTML = "Please provide an amount in integer only";
                return false;

            }
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <div id="contentCntr">
        <div class="contentcenter">
        

            <!--  / midInner container \ -->
            <div class="midInner" style="margin-left: 14px;">
                <div class="bottom">
                    <div class="mid"   style="min-height: 350px;">
     <div class="innerTabelbg">
                                <div class="toppartSml">
                                    <div class="botpartSml"  style="min-height: 350px;">
                                        <div class="tabelbluhed">Refund Details</div>
                                        <table style="width: 100%" cellpadding="0" cellspacing="10">
                                            <tr>
                                                <td style="width: 150px;">Amount (in Credits)</td>
                                                <td>
                                                    <asp:TextBox ID="txtAmount" runat="server" Width="8%" MaxLength="8" CssClass="inptredeem"></asp:TextBox></td>
                                                 </tr>
                                            <tr><td colspan="2" style="text-align: left">
                                                     <label runat="server" id="Amountless" style="color:red; text-align:right" ></label>
                                                 </td>  </tr>
                                            <tr><td></td></tr>
                                            <tr>
                                                  <td style="text-align: right">
                                                    <asp:Button ID="btnBack" runat="server" Text="Back" class="formbtnBig"  OnClick="btnBack_Click" />
                                           </td>
                                                       <td style="text-align: left">
                                                    <asp:Button ID="btnRefund" runat="server" Text="Request Refund" class="formbtnBig" OnClientClick="return ajaxloader();" OnClick="btnRefund_Click" /></td>
                                           </tr>
                                        </table>
                                 
                                    </div>
                                </div>
                            </div></div>   </div>  </div></div>  </div>
      <div id="overlay" runat="server" style="position: fixed; width: 100%; height: 100%; background-color: black; opacity:0.5;filter:alpha(opacity=50);top: 0px; left: 0px; text-align: center; z-index: 1; display:none">
    </div>
    <div id="progressdiv" runat="server" style="position: fixed; top: 200px; width: 100%; z-index: 2;display:none;" align="center">
        <div style="width: 300px; height: 200px;">
            <center>
                <img id="imgloader" width="100px" height="100px" alt="" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/ajax-loader.gif" /><br />
               <span style="color:white; font-weight:bold;font-size:medium;"> Processing Your Transaction</span>
            </center>
        </div>
    </div>
</asp:Content>
