<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="SMTP_ErrorHandling.aspx.cs" Inherits="EricProject.Site.SMTP_ErrorHandling" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Error</title>
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
  <div style="font-size:16px;font-style:italic; text-align:center;">
      <asp:Label runat="server" ID="lblmessage"></asp:Label>

    <br />
         <br />   </div>   
                            </div>    </div>    </div>    </div>    </div>    </div>
       
</asp:Content>
