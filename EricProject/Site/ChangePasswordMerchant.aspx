<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="ChangePasswordMerchant.aspx.cs" Inherits="EricProject.Site.ChangePasswordMerchant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <title>Change Password</title>
    <style type="text/css">
        .style1
        {
            width: 40%;
        }
    </style>
    <style type="text/css">
        .errer
        {
            border: 1px solid #d89c9e;
            text-align: center;
            font-size: 12px;
            color: #c60707;
            background: #f8bfc1;
            width: 212px;
            height: auto;
            line-height: 20px;
        }
        .errerv
        {
            border: 1px solid #d89c9e;
            text-align: left;
            font-size: 12px;
            color: #c60707;
            background: #f8bfc1;
            width: 250px;
            height: auto;
            line-height: 20px;
            margin-left: 200px;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
 
    <!--  / banner container \ -->
  <%--  <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>
                        Change Password</h2>
                </div>
                <div class="SubRight">
                    &nbsp;</div>
            </div>
            <!--  \ searchFaq box / -->
            <div class="clr">
            </div>
        </div>--%>
    </div>
    <!--  \ banner container / -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"
                    ><span>Campaigns</span></a></li>
                  <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel"><span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  / content container \ -->
    <div id="contentCntr" style="min-height: 350px;">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="midInnergrybg">
                            <h2>
                                Update Password
                            </h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <span style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d;
                                font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 330px; color: Green;"
                                id="SpanSuccess" runat="server" visible="false">&nbsp;&nbsp;&nbsp;Password Updated
                                Successfully&nbsp;&nbsp;&nbsp;</span>
                            <!--Start validation messages-->
                            <div id="MsgDiv" runat="server" style="color: Green; margin-left: 200px; margin-bottom: 20px;
                                background: #f8bfc1; line-height: 25px; text-align: center; width: 250px;">
                            </div>
                            <div id="MsgValidation" runat="server" style="color: Red; margin-left: 200px; margin-bottom: 20px;
                                background: #f8bfc1; line-height: 25px; text-align: center; width: 250px;">
                            </div>
                            <div id="validationDiv">
                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="errerv" runat="server" ShowMessageBox="False"
                                    ShowSummary="True" ValidationGroup="a" />
                            </div>
                            <!--Start formBox -->
                            <div class="formBox">
                                <%--<form action="#">--%>
                                <%--<h3>
                                    Personal Info</h3>--%>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Old Password</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <asp:TextBox ID="txtOldPassword" CssClass="formInpt" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        New Password</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <asp:TextBox ID="txtNewPassword" CssClass="formInpt" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Confirm New Password</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <asp:TextBox ID="txtConfirmNewPassword" CssClass="formInpt" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <%--</form>--%>
                                <!--End formBox -->
                                <div class="midbottgrybg">
                                    <div class="formbtns">
                                        <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" class="formbtnGrn"
                                            OnClick="btnSaveDetails_Click" ValidationGroup="a" />
                                         <asp:Button ID="btncancel" runat="server" Text="Cancel" class="formbtnGry" OnClick="btncancel_Click"
                                           />
                                        <%--<input type="button" class="formbtnGry" value="Cancel" onclick="Cancel();" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter old Password"
        ControlToValidate="txtOldPassword" Display="None" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter new Password"
        ControlToValidate="txtNewPassword" Display="None" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter confirm new password"
        ControlToValidate="txtConfirmNewPassword" Display="None" SetFocusOnError="True"
        ValidationGroup="a"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch"
        ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" Display="None"
        SetFocusOnError="True" ValidationGroup="a"></asp:CompareValidator>
</asp:Content>
