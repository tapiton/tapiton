<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Credit_Card_Listing.aspx.cs" Inherits="EricProject.Site.Credit_Card_Listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Card Details</title>
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
                    <div class="mid" style="min-height: 350px;">
                        <div class="shedulebox" id="Creditdiv">
                            <div class="innerTabelbg">
                                <div class="toppartSml">
                                    <div class="botpartSml" style="min-height: 350px;">
                                        <div class="tabelbluhed">Card Details</div>
                                        <table style="width: 100%" cellpadding="0" cellspacing="10">
                                            <tr>
                                                <td style="width: 150px;">Card Holder Name</td>
                                                <td>
                                                    <asp:Label runat="server" ID="txtcardholdername"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px;">Card Number</td>
                                                <td>
                                                    <asp:Label runat="server" ID="TxtTokenNumber"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px;">Expiry Date</td>
                                                <td>
                                                    <asp:Label runat="server" ID="txtexpirydate"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px;">Card Type</td>
                                                <td>
                                                    <asp:Label runat="server" ID="txtcardtype"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <%-- <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" >Back</a>--%>
                                                    <asp:Button ID="btnBack" runat="server" Text="Back" class="submitbtn" OnClick="btnBack_Click" />

                                                </td>
                                                <td style="text-align: right">
                                                    <asp:LinkButton ID="aEditCardDetails" runat="server" OnClick="aEditCardDetails_Click">Edit Card Details</asp:LinkButton>
                                                    <%--<a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ModifyDetails" >Edit Card Details</a>--%>

                                                </td>
                                            </tr>
                                        </table>
                                        <div class="clr"></div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
