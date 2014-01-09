<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="CampaignOverview.aspx.cs" Inherits="EricProject.Site.CampaignOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Campaign Overview</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            border-collapse: collapse;
        }
        .CampaignOverview .line
        {
            border-bottom: 1px solid #d8d8d9;
            width: 100%;
            margin-bottom: 15px;
        }
        .CampaignOverview .hd
        {
            padding-bottom: 4px;
            font-weight: bold;
            color: #085baf;
        }
        .CampaignOverview p
        {
            padding-bottom: 5px;
            color: #333333;
            line-height: 16px;
        }
        .mailForm .label
        {
            width: 70px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"
                    class="sel"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
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
                    <div class="mid" style="min-height: 350px;">
                        <!--Start midIncont -->
                        <div class="CampaignOverview">
                            <table class="style1">
                                <tr>
                                    <td align="center" rowspan="2" style="width: 19%">
                                        <asp:Image ID="imgCampaign" runat="server" Style="max-width: 160px; max-height: 90px;" />
                                    </td>
                                    <td style="width: 27%">
                                        <div class="hd">
                                            Campaign Name</div>
                                        <p>
                                            <asp:Label ID="lblCampaignName" runat="server" Text="3D Cart Custom Cap 2 Campaign"></asp:Label></p>
                                    </td>
                                    <td style="width: 27%">
                                        <div class="hd">
                                            Customer Rebate</div>
                                        <p>
                                            <asp:Label ID="lblCustomerRebate" runat="server" Text="$2 off"></asp:Label></p>
                                    </td>
                                    <td style="width: 27%">
                                        <div class="hd">
                                            Referrer Reward</div>
                                        <p>
                                            <asp:Label ID="lblReferrerReward" runat="server" Text="2% off"></asp:Label></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="hd">
                                            Min Purchase Amount</div>
                                        <p>
                                            <asp:Label ID="lblMinPurchaseAmount" runat="server" Text="$20"></asp:Label></p>
                                    </td>
                                    <td>
                                        <div class="hd">
                                            SKU</div>
                                        <p>
                                            <asp:Label ID="lblSKU" runat="server" Text="Store Wide"></asp:Label></p>
                                    </td>
                                    <td>
                                        <div class="hd">
                                            Expiration</div>
                                        <p>
                                            <asp:Label ID="lblExpiration" runat="server" Text="Wed 25 Mar, 2013"></asp:Label></p>
                                    </td>
                                </tr>
                                <tr><td colspan="4" style="text-align: right">
                                <%--<a href="javascript:void(0)" style="color: #0084b4;"><strong>Edit Campaign</strong></a>--%>
                                <asp:LinkButton ID="lnkEditCampaign" runat="server" style="color: #0084b4;" 
                                        Font-Bold="True" onclick="lnkEditCampaign_Click">Edit Campaign</asp:LinkButton>
                                </td></tr>
                            </table>
                            <div class="line">
                                &nbsp;</div>
                            <!--Start dashLeft -->
                            <div class="dashLeft">
                                <h2 class="bluhedBorder">
                                    Facebook</h2>
                                <!--Start graphBox -->
                                <div class="graphBox">
                                    <div class="socialSec" style="padding-bottom: 0;">
                                        <div class="line" style="margin-bottom: 0; padding-top: 10px;">
                                            <div class="image">
                                             
                                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/commenter_img.jpg" alt=""/></div>
                                            <div class="txt" style="width: 367px">
                                                <div class="title">
                                                  John Smith</div>
                                                <span class="date">2013 9:22am.</span> <%--Your customer personal message to there friends--%>
                                                <asp:Label ID="lblFbMessage" runat="server"></asp:Label>
                                                <div class="commenteddata">
                                                    <div class="img">
                                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/commenter_img2.jpg" alt="" style="border: none"/></div>
                                                    <div class="detailtxt" style="width: 275px;">
                                                        <div class="title">
                                                           Special Offer for Friends</div>
                                                        <p>
                                                            Get 20% off on $150.00 your order</p>
                                                    </div>
                                                    <div class="clr">
                                                    </div>
                                                </div>
                                                <div class="action">
                                                    <ul>
                                                        <li>Like</li>
                                                        <li>Comment</li>
                                                        <li>Share</li>
                                                        <li>
                                                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/like_icon.jpg" alt=""/>
                                                            66</li>
                                                        <li>
                                                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/globe_icon.jpg" alt=""/></li>
                                                        <li class="last">Sponsored</li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="">
                                <h2 class="bluhedBorder">
                                    <span class="fl">Twitter </span><span class="grnlink fr"></span>
                                </h2>
                                <!--Start postBox -->
                                <div class="">
                                    <div class="tweetbox" id="Div1" style="margin: 0;">
                                        <div class="lastline">
                                            <div class="twitterImg" style="padding-left: 10px;">
                                                
                                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/twitter1.jpg" alt=""/></div>
                                            <div class="twitteTxt">
                                                <div class="hd">
                                                  alessa @paramoresays</div>
                                                <p>
                                                    <%--Just buy at flat rate moving and get a coupon to share<br>
                                                    with my friends--%>
                                                    <asp:Label ID="lblTwitter" runat="server"></asp:Label>
                                                    <strong>#ParamoreBBCRadio1</strong></p>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End postBox -->
                            </div>
                                <!--End graphBox -->
                            </div>
                            <!--End dashLeft -->
                            <!--Start dashRight -->
                            <div class="dashRight">
                                <h2 class="bluhedBorder">
                                    Email</h2>
                                <!--Start graphBox -->
                                <div class="graphBox">
                                    <div class="socialSec" style="padding-bottom: 0;">
                                        <div class="mailForm">
                                            <fieldset>
                                                <div>
                                                    <div class="label">
                                                        From</div>
                                                    <div class="fld">
                                                        <input type="text" disabled="disabled" class="inpt" value="Customer Name"
                                                          /></div>
                                                    <div class="clr">
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="label">
                                                        Subject</div>
                                                    <div class="fld">
                                                        <input type="text" disabled="disabled" id="txtEmailSubject" runat="server" class="inpt" value="Thought you might be interested" 
                                                           /></div>
                                                    <div class="clr">
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <div class="grybarHd">
                                                Referral website</div>
                                            <div class="gradientBox">
                                                <div class="upper">
                                                    <div class="lower">
                                                        <div class="text">
                                                            <span class="grnfont">Get <strong>20%</strong></span>
                                                                abcuser@yoursite.com <span class="grnfont">want to share this deal with you</span><br>
                                                            <br/>
                                                            <%--Your customer writes a personal message here--%>
                                                            <asp:Label ID="lblEmailMessage" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mailtxt">
                                                Your friend shared this with you using referral website on behalf of Testsite.</div>
                                            <div class="postBtns">                                                
                                                <input type="button" class="gryfldBtn" value="Send" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End graphBox -->
                            </div>
                            
                            <!--End dashRight -->
                            <div class="clr">
                            </div>
                        </div>
                        <!--End midIncont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
</asp:Content>
