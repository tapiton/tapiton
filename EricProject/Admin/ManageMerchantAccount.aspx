<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageMerchantAccount.aspx.cs" Inherits="Admin_ManageMerchantAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Account Management</title>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/manage-merchant-account.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#a1").click(function () {
                $("#s1").toggleClass("showpop");
            });
        });
        $(document).ready(function () {
            $("#a2").click(function () {
                $("#s2").toggleClass("showpop");
            });
        });
        $(document).ready(function () {
            $("#a3").click(function () {
                $("#s3").toggleClass("showpop");
            });
        });
    </script>
    <style type="text/css">
        .showpop
        {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>
                    Merchant Account Management</h5>
                <span>The merchant management system provide an access to view and control of the merchant
                    accounts on the website.</span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <div class="statsRow">
        <div class="wrapper statsItems">
            <!-- Stats item -->
            <div class="sItem ticketsStats">
                <h2>
                    <a title="" class="value" id="a1">
                        <div id="DivTotalActiveMerchant">
                        </div>
                        <span>Active Merchants</span></a></h2>
                <div class="statsDetailed" id="s1">
                    <div class="statsContent">
                        <div class="sElements">
                            <div class="sDisplay" style="line-height: 15px;">
                                <h4>
                                    <div id="DivTotalNewSignupMerchant">
                                    </div>
                                </h4>
                                <span>New Signups</span></div>
                            <div class="sDisplay" style="line-height: 15px;">
                                <h4>
                                    <div id="DivTotalDeActivatedMerchant">
                                    </div>
                                </h4>
                                <span>Deactivated</span></div>
                            <div class="sDisplay" style="line-height: 15px;">
                                <h4>
                                    <div id="DivTotalMerchant">
                                    </div>
                                </h4>
                                <span>Total</span></div>
                        </div>
                    </div>
                </div>
                <span class="changes"><span class="negBar" values="5,10,15,20,18,16,14,20,15,16"></span>
                    <div id="DivTotalMerchantIncreDecPercent">
                    </div>
                </span>
            </div>
            <!-- Stats item -->
            <div class="sItem visitsStats">
                <h2>
                    <a title="" class="value" id="a2">
                        <div id="DivTotalPointsSold">
                        </div>
                        <span>Points Sold</span></a></h2>
                <div class="statsDetailed" id="s2">
                    <div class="statsContent">
                        <div class="sElements">
                            <div class="sDisplay">
                                <div id="DivPointsSoldFirstMonth">
                                </div>
                            </div>
                            <div class="sDisplay">
                                <div id="DivPointsSoldSecondMonth">
                                </div>
                            </div>
                            <div class="sDisplay">
                                <div id="DivPointsSoldThirdMonth">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <span class="changes"><span class="posBar" values="5,10,15,20,25,30,35,40,45,50"></span>
                    <div id="DivTotalMerchantPointsSoldIncreDecPercent">
                    </div>
                </span>
            </div>
            <!-- Stats item -->
            <div class="sItem usersStats">
                <h2>
                    <a title="" class="value" id="a3">
                        <div id="DivTotalActiveCampaigns">
                        </div>
                        <span>Active Campaigns</span></a></h2>
                <div class="statsDetailed" id="s3">
                    <div class="statsContent">
                        <div class="sElements">
                            <div class="sDisplay">
                                <div id="DivActiveCampaignsFirstMonth">
                                </div>
                            </div>
                            <div class="sDisplay">
                                <div id="DivActiveCampaignsSecondMonth">
                                </div>
                            </div>
                            <div class="sDisplay">
                                <div id="DivActiveCampaignsThirdMonth">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <span class="changes"><span class="zeroBar" values="5,10,15,20,25,30,35,40,45,50"></span>
                    <div id="DivTotalMerchantActivityCampaignsIncreDecPercent">
                    </div>
                </span>
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <div class="line mt30">
    </div>
    <!-- Page statistics and control buttons area -->
    <div class="statsRow">
        <div class="wrapper">
            <div class="controlB">
                <ul>
                    <li><a href="javascript:void();" title="" id="btnMerchantSignups" onclick="VisibleMerchantDetailsDiv(this.id);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/plus.png"
                            alt="" />
                        <span>Merchant Signups</span> </a></li>
                    <li><a href="javascript:void();" title="" id="btnAddMerchant" onclick="VisibleMerchantDetailsDiv(this.id);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/hire-me.png"
                            alt="" />
                        <span>Manage Merchant</span> </a></li>
                    <li><a href="javascript:void();" title="" id="btnSearchMerchant" onclick="VisibleMerchantDetailsDiv(this.id);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/database.png"
                            alt="" />
                        <span>Search Merchant</span> </a></li>
                    <li><a href="javascript:void();" title="" id="btnCheckRefunds" onclick="VisibleMerchantDetailsDiv(this.id);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/order-149.png"
                            alt="" />
                        <span>Check Refunds</span> </a></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <div class="wrapper" id="MerchantAlreadyExist" style="display: none;">
        <div class="nNote nSuccess hideit">
            <p>
                <strong>Already Exists: </strong>Merchant Email Id already exits.</p>
        </div>
    </div>
    <div class="wrapper" id="NoRecordFound" style="display: none;">
        <div class="nNote nSuccess hideit">
            <p>
                <strong>Search: </strong>No Record Found.</p>
        </div>
    </div>
    <div class="wrapper" id="SuccessNewPassword" style="display: none;">
        <div class="nNote nSuccess hideit">
            <p>
                <strong>SUCCESS: </strong>New password has been emailed to the merchant at the registered
                email address</p>
        </div>
    </div>
    <div class="wrapper" id="SuccessMerchantDetails" style="display: none;">
        <div class="nNote nSuccess hideit">
            <p>
                <strong>SUCCESS: </strong>
               <label id="lbl_msg" name="lbl_msg"></label> </p>
        </div>
    </div>
    <div class="wrapper" id="SuccessAccountDeleted" style="display: none;">
        <div class="nNote nSuccess hideit">
            <p>
                <strong>SUCCESS: </strong>Merchant account has been deleted</p>
        </div>
    </div>
    <div class="wrapper">
        <fieldset id="SectionSearch">
            <div class="widget">
                <div class="formRow">
                    <label style="padding-top: 15px;">
                        Search Merchants:</label>
                    <div class="formRight" style="width: 100%;">
                        <input style="width: 555px;" type="text" id="txtSearch" value="Use Email Address/User name to search for the merchant"
                            onblur="if (this.value == '') {this.value = 'Use Email Address/User name to search for the merchant';}"
                            onfocus="if(this.value == 'Use Email Address/User name to search for the merchant') {this.value = '';}"
                            onkeypress="return searchKeyPress(event);" />
                        <a href="javascript:void();" title="Search" class="wButton bluewB ml15 m10" onclick="return Search();">
                            <span>Search</span></a>
                    </div>
                    <a href="javascript:void();" onclick="ViewAll();" id="aViewAll">View All</a>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </fieldset>
        <div class="widget" id="SectionMerchantOverview">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Merchant Overview</h6>
            </div>
            <table id="GrdManageMerchant" cellpadding="0" cellspacing="0" width="100%" class="display dTable">
                <thead>
                    <tr>
                        <td>
                            Name
                        </td>
                        <td>
                            Email
                        </td>
                        <td>
                            Website
                        </td>
                        <td>
                            Active Campaigns
                        </td>
                        <td>
                            Points sold <span id="SpanCurrentMonth"></span>
                        </td>
                        <td>
                            Avg Credit purchase
                        </td>
                        <td>
                            Status
                        </td>
                        <td>
                            Manage
                        </td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <%--    <div class="line mt30" id="SpaceSectionMerchantDetails">
    </div>--%>
    <div class="wrapper" id="SectionMerchantDetails">
        <fieldset>
            <div class="widget">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/list.png"
                        alt="" class="titleIcon" /><h6>
                            Merchant Details : <span style="color: #000;" id="MerchantName"></span>
                        </h6>
                    <div style="float: right;">
                        <span id="SpanMerchantDetailsOpertation"></span>
                    </div>
                </div>
                <div class="formRow">
                    <h6>
                        Personal Info</h6>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        First Name</label>
                    <div class="formRight">
                        <input type="text" id="txtFirstName" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Last Name</label>
                    <div class="formRight">
                        <input type="text" id="txtLastName" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        E-mail Address</label>
                    <div class="formRight">
                        <input type="text" id="txtEmail" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Password</label>
                    <div class="formRight">
                        <input type="text" id="txtPassword" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <h6>
                        Company Info</h6>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Company Name</label>
                    <div class="formRight">
                        <input type="text" id="txtCompanyName" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Address</label>
                    <div class="formRight">
                        <input type="text" id="txtAddress" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        City</label>
                    <div class="formRight">
                        <input type="text" id="txtCity" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        State</label>
                    <div class="formRight">
                        <input type="text" id="txtState" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select Country:</label>
                    <div class="formRight">
                        <select name="ddlCountry" id="ddlCountry">
                            <option value="0">Please Select Country</option>
                        </select>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Zip</label>
                    <div class="formRight">
                        <input type="text" id="txtZip" maxlength="5" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Phone</label>
                    <div class="formRight">
                        <input type="text" id="txtPhone" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Fax</label>
                    <div class="formRight">
                        <input type="text" id="txtFax" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <h6>
                        Website</h6>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select E-commerce Platform</label>
                    <div class="formRight">
                        <select name="ddlEcomPlatform" id="ddlEcomPlatform">
                            <option value="0">Please Select Platform</option>
                        </select>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Website Url<span style="padding-left: 4px;">(<b>http://</b>)</span></label>
                    <div class="formRight">
                        <input type="text" id="WesiteUrl" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="Update" id="Btn_Update_Merchant" class="redB" onclick="return UpdateMerchantDetails();" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
    </div>
    <%--  <div class="line mt30" id="SpaceSectionMerchantCampaigndetails">
    </div>--%>
    <div class="wrapper" id="SectionMerchantCampaigndetails">
        <div class="widget">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Merchant Campaign details: <span style="color: #000;" id="MerchantNameForCampaign">&nbsp;</span></h6>
            </div>
            <table id="GrdMerchantCampaigns" cellpadding="0" cellspacing="0" width="100%" class="display dTable">
                <thead>
                    <tr>
                        <td style="max-width: 75%; width: 25%;">
                            Campaign
                        </td>
                        <td>
                            Customer Rebate
                        </td>
                        <td>
                            Referrer Reward
                        </td>
                        <td>
                            Minimum Amount
                        </td>
                        <td>
                            Expiration (Days)
                        </td>
                        <td>
                            Credits rewarded
                        </td>
                        <td>
                            Sales
                        </td>
                        <td>
                            Referals
                        </td>
                        <td>
                            Clicks
                        </td>
                        <td>
                            Status
                        </td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <%-- <div class="line mt30">
    </div>--%>
    <input type="hidden" id="hiddenMerchantID" value="0" />
    <script type="text/javascript">
        FunctionOnLoad();
    </script>
</asp:Content>
