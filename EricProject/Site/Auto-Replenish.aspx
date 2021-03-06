﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Auto-Replenish.aspx.cs" Inherits="EricProject.Site.Auto_Replenish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>AutoReplenish</title>
      <script type="text/javascript" language="javascript">
          if (location.protocol !== "https:")
              location.protocol = "https:";
          function ajaxloader() {
              document.getElementById("imgloader").src = "<%=ConfigurationManager.AppSettings["pageURL"] %>images/ajax-loader.gif";
              document.getElementById("<%=overlay.ClientID%>").style.display = "block";
            document.getElementById("<%=progressdiv.ClientID%>").style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
            <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard"><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel"><span>Account</span></a></li>
            </ul>
            <div class="clr"></div>
        </div>
    </div>
          <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid"   style="min-height: 350px;">
                        <%--                        <div class="midInnergrybg">
                            <h2>Account Detail <span>My Profile, Referral Plug-Ins and Credit Details</span></h2>
                        </div>--%>
                        <!--Start midInnercont -->
                        <div class="midInnercont" style="padding-top: 12px">
                           
  <div style="font-size:14px;text-align:center;"> Auto-Replenish</div><br /><br />
  <div>
Turning on auto-replenish will ensure that your campaigns remain active and your referrers are properly rewarded. 
    <br />   <br />
Auto-replenish will automatically make purchases equal to your last purchase amount. 
    <br/>   <br />
Turning on auto-replenish at this time will trigger an automatic payment because you have a negative balance. 
    <br />   <br />
Please confirm if you would like to proceed.
    <br />
               <br />   </div>    
<asp:Button runat="server" ID="btnback"  class="submitbtn"  Text="Back" OnClick="btnback_Click" />
                              &nbsp;  &nbsp;
<asp:Button runat="server" ID="btncontinue"  class="submitbtn" Text="Continue" OnClientClick="ajaxloader();" OnClick="btncontinue_Click"  />
                                
                            </div>    </div>    </div>    </div>    </div>    </div>
        <div id="overlay" runat="server" style="position: fixed; width: 100%; height: 100%; background-color: black; opacity:0.5;filter:alpha(opacity=50);top: 0px; left: 0px; text-align: center; z-index: 1; display:none">
    </div>
     <div id="progressdiv" runat="server" style="position: fixed; top: 200px; width: 100%; z-index: 2;display:none;" align="center">
        <div style="width: 300px; height: 200px;">
            <center>
                <img id="imgloader" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/ajax-loader.gif" width="100px" height="100px" alt="" /><br />
               <span style="color:white; font-weight:bold;font-size:medium;"> Processing Your Transaction</span>
            </center>
        </div>
    </div>
       
</asp:Content>
