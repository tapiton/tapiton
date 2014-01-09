<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Renew_subscription.aspx.cs" Inherits="EricProject.Site.Renew_subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Renew Subscription</title>
    <script type="text/javascript" language="javascript">
        if (location.protocol !== "https:")
            location.protocol = "https:";
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
                    <div class="mid">
                        <%--                        <div class="midInnergrybg">
                            <h2>Account Detail <span>My Profile, Referral Plug-Ins and Credit Details</span></h2>
                        </div>--%>
                        <!--Start midInnercont -->
                        <div class="midInnercont" style="padding-top: 12px">

                            <div style="font-size: 14px; text-align: center;">Renew Subscription</div>
                            <br />
                            <br />
                            <div>
                                Renew Subscription will deduct $9.99 from your Account<br />
                                You want to continue this.<br />
                                <br />
                                <asp:Button runat="server" class="submitbtn" ID="btncontinue" Text="Continue" OnClick="btncontinue_Click" />
                                <br />
                                <br />
                                <asp:Label ID="lblSubscriptionFailed" runat="server" Text="Renew subscription payment has been failed." ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="overlay" runat="server" style="position: fixed; width: 100%; height: 100%; background-color: black; opacity:0.5;filter:alpha(opacity=50); top: 0px; left: 0px; text-align: center; z-index: 1; display: none">
        </div>
        <div id="progressdiv" runat="server" style="position: fixed; top: 200px; width: 100%; z-index: 2; display: none;" align="center">
            <div style="width: 300px; height: 200px;">
                <center>
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/ajax-loader.gif" width="100px" height="100px" alt="" /><br />
                    <span style="color: white; font-weight: bold; font-size: medium;">Subscribing...</span>
                </center>
            </div>
        </div>
</asp:Content>
