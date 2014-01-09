<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="MerchantProfile.aspx.cs" Inherits="Site_MerchantProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Profile</title>
   <script type="text/javascript" >
        function changeintegration()
        {
            if (document.getElementById('<%=isintegrated.ClientID%>').value == "1" && (document.getElementById('<%=MerchantWebsiteURL.ClientID%>').value != document.getElementById('<%=txtWebsiteURL.ClientID%>').value || document.getElementById('<%=MerchantPlatForm.ClientID%>').value != document.getElementById('<%=ddlEcomPlatform.ClientID%>').value)) 
            {
               
                if (confirm("Changing the URL or platform will require you to integrate the site again.")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            if (document.getElementById('<%=txtCompanyName.ClientID%>').value.length < 3) {
                alert("Company Name Should be of minium three characters");
                return false;
            }
            if (document.getElementById('<%=txtWebsiteURL.ClientID %>').value.indexOf(".") == -1) {
                alert("URL Must contain . in it");
                    return false;
                }
        }
        function onsucess() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ -->

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
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel">
                    <span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="midInnergrybg">
                            <h2>
                                Manage Profile <span>Manage Your Profile, Referral Plug-Ins and Credit Details</span></h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <span style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d;
                                font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 330px; color: Green;"
                                id="SpanSuccess" runat="server" visible="false">&nbsp;&nbsp;&nbsp;Your Profile has
                                been updated successfully.&nbsp;&nbsp;&nbsp;</span>
                            <!--Start formBox -->
                            <div class="formBox" style="position:relative ">
                                <%--<form action="#">--%>
                                <h3>
                                    Personal Info</h3>  
                                 <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ChangePassword" style="position:absolute;top:0px;left :350px;font-weight: normal;text-decoration: none;font-size: 12px;color: #7ebb01;  ">Change Password</a>
                                <div class="formLine">
                                    <div class="formLabel">
                                        First Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtFirstName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Last Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtLastName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Email Address</div>
                                    <div class="formField" style="line-height: 32px;">
                                        <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="ProfileEmail"></asp:Label>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="spacer">
                                    &nbsp;</div>
                                <h3>
                                    Company Info</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Company Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtCompanyName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Address</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtAddress" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        City</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtCity" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        State</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtState" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" maxlength="2" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Country</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <%--  <select class="formSlct" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';"
                                                id="ddlCountry" runat="server">
                                                <option>India</option>
                                            </select>--%>
                                            <asp:DropDownList ID="ddlCountry" runat="server" class="formSlct">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Zip</div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtZip" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);"
                                                maxlength="5" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Phone</div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtPhone" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Fax</div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtFax" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Pending Credit Duration</div>
                                    <%--<div class="formField">--%>
                                    <div style="float: left; width: 124px">
                                        <div class="formFldsml">
                                            <asp:DropDownList ID="ddlPendingDate" runat="server" Style="border: 2px solid #FFFFFF;
                                                color: #707070; font-size: 14px; padding: 5px 2px 5px 3px; width: 124px;">
                                                <asp:ListItem Value="5">5 Days</asp:ListItem>
                                                <asp:ListItem Value="10">10 Days</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="15">15 Days</asp:ListItem>
                                                <asp:ListItem Value="20">20 Days</asp:ListItem>
                                                <asp:ListItem Value="25">25 Days</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="spacer">
                                    &nbsp;</div>
                                <h3>
                                    Web Site</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        eCommerce Platform</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <%-- <select class="formSlct" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';"
                                                id="ddlEcomPlatform" runat="server">
                                                <option>Volusion</option>
                                            </select>--%>
                                            <asp:DropDownList ID="ddlEcomPlatform" runat="server" class="formSlct"  >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Wesite Url <span class="http"></span></div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtWebsiteURL" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                     <div class="formLine">
                                    <div class="formLabel">
                                         <%=ConfigurationManager.AppSettings["site_name"]%> ID</div>
                                    <div class="formField" style="line-height: 32px;">
                                        <asp:Label ID="lblsocialreferralid" runat="server" Text="Label" CssClass="ProfileEmail"></asp:Label>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <%--</form>--%>
                            </div>
                            <!--End formBox -->
                            <div class="midbottgrybg">
                                <div class="formbtns">
                                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" class="formbtnGrn" OnClientClick="return changeintegration();"
                                        OnClick="btnSaveDetails_Click" />
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
    <asp:HiddenField runat="server" ID="isintegrated" />
     <asp:HiddenField runat="server" ID="MerchantWebsiteURL" />
     <asp:HiddenField runat="server" ID="MerchantPlatForm" />
    <!--  \ content container / -->
    <%--<div align="center" id="lblMessage" style="position: fixed;  width: 100%;
        top: 0px; height: 1px; z-index: 999999">
       
    </div>--%>
</asp:Content>
