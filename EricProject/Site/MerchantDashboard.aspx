<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="MerchantDashboard.aspx.cs" Inherits="Site_MerchantDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
    <title>Dashboard</title>
    <style type="text/css">
        .Margin {
            vertical-align: middle;
            text-align: left;
            padding-top: 20px;
            margin-left: 5px;
        }
    </style>
    <script type="text/javascript">
        function Validate() {
            if (document.getElementById('ContentPlaceHolder2_txtWebsiteURL').value == '') {
                alert("Enter website url");
                return false;
            }
        }
        function ValidateCompany() {
            if (document.getElementById('ContentPlaceHolder2_txtcompanyname').value == '') {
                alert("Enter Company Name");
                return false;
            }
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">   
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard"
                    class="sel"><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails">
                    <span>Account</span></a></li>
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
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <h2 class="bluhed">
                            <span class="fl">Reports</span> <span class="grnlink fr"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">View Details</a></span>
                        </h2>
                        <!--Start reportbg -->
                        <div class="reportbg">
                            <div class="upper">
                                <div class="lower">
                                    <ul>
                                        <li><span><span class="rt">
                                            <%=TotalReferrals%></span></span>
                                            <label>
                                                Total Purchases</label></li>
                                        <li><span><span class="rt">
                                            <%=TotalReach%></span></span>
                                            <label>
                                                Total Reach</label></li>
                                        <li><span><span class="rt">
                                            <%=TotalClicks%></span></span>
                                            <label>
                                                Total Clicks</label></li>
                                        <li><span><span class="rt"><%=TotalSales%></span></span>
                                            <label>
                                               Referred Sales</label></li>
                                    </ul>
                                    <div class="clr">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--End reportbg -->
                        <!--Start dashLeft -->
                        <div class="dashLeft" id="DivAnalytics" runat="server">
                            <h2 class="bluhedBorder">Analytics</h2>
                            <!--Start graphBox -->
                            <div class="graphBox">
                                <iframe src="<%=ConfigurationManager.AppSettings["pageURL"] %>Charts/Merchant/Dashboard.aspx"
                                    id="iframe1" width="450px" height="316px" frameborder="0" scrolling="no"></iframe>
                            </div>
                            <!--End graphBox -->
                        </div>
                        <div class="dashLeft" id="DivMerchantLoginConditionsAnalytics" runat="server">
                            <h2 class="bluhedBorder">Steps to get started</h2>
                            <!--Start graphBox -->
                            <div class="graphBox" style="min-height: 300px; background-color: #fbfbfb;">
                                <div class="dashboardFlds" id="DivWebsiteIUrl" runat="server">
                                    <div class="label">
                                        Website URL :
                                    </div>
                                    <div class="fld">
                                        <asp:TextBox CssClass="inpt" ID="txtWebsiteURL" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" class="formbotton" OnClick="btnSave_Click"
                                            OnClientClick="return Validate()" />
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                  <div class="dashboardFlds" id="divcompany" runat="server">
                                    <div class="label">
                                        Company Name :
                                    </div>
                                    <div class="fld">
                                        <asp:TextBox CssClass="inpt" ID="txtcompanyname" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnsavecompany" runat="server" Text="Save" class="formbotton"                                          
                                               OnClientClick="return ValidateCompany()" OnClick="btnsavecompany_Click" />
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <asp:Literal ID="litMerchantLoginConditionsAnalytics" runat="server"></asp:Literal>
                            </div>
                            <!--End graphBox -->
                        </div>
                        <!--End dashLeft -->
                        <!--Start dashRight -->
                        <div class="dashRight">
                            <h2 class="bluhedBorder">
                                <span class="fl">Latest Posts by Customers </span><span class="grnlink fr"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Posts">
                                    <%=TotalCustomer %> Customer Posts</a></span>
                            </h2>
                            <!--Start postBox -->
                            <div class="postBox">
                                <asp:Literal ID="LatestTop3PostByCustomer" runat="server"></asp:Literal>
                            </div>
                            <!--End postBox -->
                        </div>
                        <!--End dashRight -->
                        <div class="clr">
                        </div>
                        <div class="spacer">
                            &nbsp;
                        </div>
                        <h2 class="bluhedBorder">
                            <span class="fl">Campaigns</span> <span class="grnlink fr"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">Manage Campaigns</a></span>
                        </h2>
                        <span style="width: 200px; color: #222222; font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 0px; color: Green;"
                            id="SpanCampaignNotAvailable" runat="server"
                            visible="false">Campaigns are not available.&nbsp;&nbsp;&nbsp;</span>
                        <div id="DivNoCampaign" runat="server">
                            <!--Start campaignbox left -->
                            <div class="campaignbox fl">
                                <asp:Literal ID="litCampaignLeft" runat="server"></asp:Literal>
                            </div>
                            <!--End campaignbox left -->
                            <!--Start campaignbox right -->
                            <div class="campaignbox fr">
                                <asp:Literal ID="litCampaignRight" runat="server"></asp:Literal>
                            </div>
                            <!--End campaignbox right -->
                            <div class="clr">
                            </div>
                        </div>
                        <!--Start botspaceInner -->
                        <div class="botspaceInner">
                            <div class="midbottgrybg">
                                <%--<a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign/New"
                                    class="greenbtn" style="color: #FFFFFF;">Create Campaign</a> --%>
                                <asp:LinkButton ID="lnkCreateCampaign" CssClass="greenbtn" Style="color: #FFFFFF;"
                                    runat="server" OnClick="lnkCreateCampaigncopyClick">Create Campaign</asp:LinkButton>
                               
                                <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics" class="grybtn" style="color: #FFFFFF;">
                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/form_icon.png"
                                        alt="" />
                                    View Reports</a> <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Profile"
                                        class="grybtn" style="color: #FFFFFF;">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/account_icon.png"
                                            alt="" />
                                        Manage Account</a>
                            </div>
                        </div>
                        <!--End botspaceInner -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <%-- <div class="loginPopup" id="DivMerchantLoginConditions" style="position: absolute;
    top: 200px; left: 630px;" runat="server"> <div class="formBanner formBanner1"> <div
    class="bottom"> <div class="mid" style="height: 150px;"> <div class="virtical" id="tab1"
    runat="server"> <div class="formHd"> <span>&nbsp;&nbsp;&nbsp;</span>Welcome <div
    class="deletebg"> <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
    alt="" onclick="ClosePopup();" /> </div> <div class="clr"> </div> </div> <div class="formField"
    style="margin-left: 20px;"> <br /> <asp:Literal ID="litMerchantLoginConditions"
    runat="server"></asp:Literal> <div class="clr"> </div> </div> </div> <div class="clr">
    </div> </div> </div> </div> </div>--%>
    <!-- \ content container / -->
    <script type="text/javascript">
        function ClosePopup() {
            document.getElementById("ContentPlaceHolder2_DivMerchantLoginConditions").style.display
    = "none";
        }
       
    </script>
</asp:Content>
