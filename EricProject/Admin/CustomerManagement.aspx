<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="CustomerManagement.aspx.cs" Inherits="Admin_CustomerManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Management</title>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/themes/base/jquery.ui.all.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-1.7.2.js"
        type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/demos.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.ui.core.js"
        type="text/javascript"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.ui.datepicker.js"
        type="text/javascript"></script>
        <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/customer-management.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#DateFrom").datepicker();
            $("#DateTo").datepicker();
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Title area -->
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>
                    Customer Management System</h5>
                <span>The customer management system is a tool for the marketing team to record and
                    review thier customer contacts and communication</span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Page statistics and control buttons area -->
    <div class="statsRow">
        <div class="wrapper">
            <div class="horControlB">
                <ul style="text-align: left;">
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/order-192.png"
                            alt="" />
                        <span id="btnActivityOverviewbyCustomer" onclick="VisibleCustomerInfoDiv(this.id);">
                            Activity Overview by Customer</span> </a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/hire-me.png"
                            alt="" />
                        <span id="btnAddNewCustomer" onclick="VisibleCustomerInfoDiv(this.id);">Add New Customer</span>
                    </a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/hire-me.png"
                            alt="" />
                        <span id="btnAddNewClient" onclick="VisibleCustomerInfoDiv(this.id);">Add New Client</span>
                    </a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                            alt="" />
                        <span id="btnUpdateActivityLog" onclick="VisibleCustomerInfoDiv(this.id);">Update Activity
                            Log</span> </a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                            alt="" />
                        <span id="btnManageE-commercePlatforms" onclick="VisibleCustomerInfoDiv(this.id);">Manage
                            E-commerce Platforms</span> </a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                            alt="" />
                        <span id="btnManageClientProfile" onclick="VisibleCustomerInfoDiv(this.id);">Manage
                            Client Profile</span> </a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Main content wrapper -->
    <div class="wrapper">
        <fieldset id="AddNewCustomerCompany" style="display: none;">
            <div class="widget">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                        alt="" class="titleIcon" /><h6>
                            Add New Customer/Company</h6>
                </div>
                <div class="formRow">
                    <label>
                        Name Of Company:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtCompanyName" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Website:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtCompanyWebsite" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select E-commerce Platform:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlSelectEcom">
                                <option value="">Select E-commerce Platform</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        If other please specify:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtOtherPlatform" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Company Email Address:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" id="txtCompanyEmailAddress" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Company Address:<span class="req"></span></label>
                    <div class="formRight">
                        <textarea rows="8" cols="" name="textarea" id="txtCompanyAddress"></textarea></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Company Address1:<span class="req"></span></label>
                    <div class="formRight">
                        <textarea rows="8" cols="" name="textarea" id="txtCompanyAddress1"></textarea></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        City:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" id="txtCompanyCity" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        State:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" id="txtCompanyState" maxlength="2" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Zip:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" id="txtCompanyZip" maxlength="5" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Company Phone Number:</label>
                    <div class="formRight">
                        <input type="text" value="" name="PhoneNo" id="txtCompanyPhoneNumber" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Company Fax:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" name="txtCompanyFax" id="txtCompanyFax" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="submit" id="btnAddNewCompanyDetails" class="redB" onclick="return AddNewCompanyDetails();" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
        <div class="widget" id="CustomerCompanyDetails" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Manage Customers</h6>
            </div>
            <!-- Dynamic table -->
            <table id="GrdManageCustomer" cellpadding="0" cellspacing="0" border="0" class="display dTable">
                <thead>
                    <tr>
                        <th>
                            Name of Company
                        </th>
                        <th>
                            Website
                        </th>
                        <th>
                            E-commerce Platform
                        </th>
                        <th>
                            Address
                        </th>
                        <th>
                            Phone Number
                        </th>
                        <th>
                            Fax
                        </th>
                        <th>
                            Edit
                        </th>
                        <th>
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <fieldset id="AddNewContact" style="display: none;">
            <div class="widget">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                        alt="" class="titleIcon" /><h6>
                            Add New Contact</h6>
                </div>
                <div class="formRow">
                    <label>
                        Select Company:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlCompanyNameContact">
                                <option value="">Select Company</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select Sales Person:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlSalesPersonContact">
                                <option value="">--Select--</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Name Of Contact:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="txtNameOfContact" id="txtNameOfContact" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Job Title:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" name="txtJobTitle" id="txtJobTitle" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Contact Phone Number:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" name="txtContactPhone" id="txtContactPhone" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Contact Email Address:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" name="txtContactEmail" id="txtContactEmail" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Contact Fax:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="" name="txtContactFax" id="txtContactFax" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="submit" class="redB" id="btnAddNewCompanyContactDetails"
                        onclick="return AddNewCompanyContactDetails();" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
        <div class="widget" id="ContactDetails" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Contact Details</h6>
            </div>
            <!-- Dynamic table -->
            <table id="GrdManageCompanyContact" cellpadding="0" cellspacing="0" border="0" class="display dTable">
                <thead>
                    <tr>
                        <th style="width: 90%">
                            Contact Name
                        </th>
                        <th style="width: 90%">
                            Job Title
                        </th>
                        <th style="width: 90%">
                            Company
                        </th>
                        <th style="width: 90%">
                            Phone Number
                        </th>
                        <th style="width: 90%">
                            Email Address
                        </th>
                        <th style="width: 90%">
                            Fax
                        </th>
                        <th style="width: 90%">
                            Last Contact Date
                        </th>
                        <th style="width: 10%">
                            Edit
                        </th>
                        <th style="width: 10%">
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="widget" id="ActivityOverviewbyCustomerCompany" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Activity Overview by Customer/Company</h6>
            </div>
            <div class="formRow">
                <label>
                    Select Company:<span class="req">*</span></label>
                <div class="formRight">
                    <div>
                        <select name="selectReq" id="ddlSelectCompanyActivityFilter">
                            <option value="">Select Company</option>
                        </select>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Select Sales person:<span class="req">*</span></label>
                <div class="formRight">
                    <div>
                        <select name="selectReq" id="ddlSalesPersonActivityFilter">
                            <option value="">Select Sales person</option>
                        </select>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Select Date Range:</label>
                <div class="formRight">
                    <span>From: </span><span class="req">*</span>
                    <input type="text" class="datepicker" id="DateFrom" /><span style="padding-left: 20px;">To:<span
                        class="req">*</span> </span>
                    <input type="text" class="datepicker" id="DateTo" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formSubmit">
                <input type="button" value="Filter" class="redB" onclick="return FilterActiviy();" /></div>
            <div class="clear">
            </div>
        </div>
        <div class="widget" style="display: none;" id="ActivityOverview">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Activity Overview</h6>
            </div>
            <table id="GrdManageActivity" cellpadding="0" cellspacing="0" width="100%" class="display dTable">
                <thead>
                    <tr>
                        <th>
                            Company Name
                        </th>
                        <th>
                            Contact
                        </th>
                        <th>
                            Contact Date
                        </th>
                        <th>
                            Contact Type
                        </th>
                        <th>
                            Contacted by
                        </th>
                        <th>
                            Score
                        </th>
                        <th>
                            Notes
                        </th>
                        <th>
                            Edit
                        </th>
                        <th>
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <fieldset id="UpdateActivityLog" style="display: none;">
            <div class="widget">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                        alt="" class="titleIcon" /><h6>
                            Update Activity Log
                        </h6>
                </div>
                <div class="formRow">
                    <label>
                        Select Company:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlCompanyNameActivityUpdate" onchange="BindContactUpdateActivity();">
                                <option value="">--Select--</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select Contact ( Select company first ) :<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlContactActivityUpdate">
                                <option value="">--Select--</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Type of contact:<span class="req">*</span></label>
                    <div class="formRight">
                        <div class="floatL">
                            <input type="radio" id="rbInPerson" class="radioInpt" name="radioReq" data-prompt-position="topRight:102"
                                value="1" onclick="TypeOfContact(this.value)" /><label class="rdtxt" for="radioReq">In-person</label></div>
                        <div class="floatL">
                            <input type="radio" id="rbPhone" class="radioInpt" name="radioReq" data-prompt-position="topRight:102"
                                value="2" onclick="TypeOfContact(this.value)" /><label class="rdtxt" for="radioReq">Phone</label></div>
                        <div class="floatL">
                            <input type="radio" id="rbEmail" class="radioInpt" name="radioReq" data-prompt-position="topRight:102"
                                value="3" onclick="TypeOfContact(this.value)" /><label class="rdtxt" for="radioReq">Email</label></div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Score:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlScoreActivity">
                                <option value="0">Select Score</option>
                                <option value="1">1 - No interest</option>
                                <option value="2">2 - Intrigued/somewhat interested</option>
                                <option value="3">3 - Very interested/requested additional info</option>
                                <option value="4">4 - Exteremely interested/using a competitor</option>
                                <option value="5">5 - registered merchantContact name</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Notes:<span class="req">*</span></label>
                    <div class="formRight">
                        <textarea rows="8" cols="" name="textarea" id="txtNotesActivity"></textarea></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="submit" class="redB" id="btnAddNewActivity" onclick="return AddNewActivity();" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
        <div class="widget" style="display: none;" id="ManageE-commercePlatforms">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Manage E-commerce Platforms</h6>
            </div>
            <table id="GrdECommerce" cellpadding="0" cellspacing="0" width="100%" class="display dTable">
                <thead>
                    <tr>
                        <th>
                            E-commerce PlatForm
                        </th>
                        <th>
                            Edit
                        </th>
                        <th>
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <fieldset id="AddEditE-commerceplatform" style="display: none;">
            <div class="widget">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                        alt="" class="titleIcon" /><h6>
                            Add/Edit E-commerce platform</h6>
                </div>
                <div class="formRow">
                    <label>
                        Name Of E-Commerce platform:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtECommerce" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="submit" class="redB" id="btnECommerceADD" onclick="return ECommerceADD();" /></div>
                <!-- Table with sortable columns -->
            </div>
        </fieldset>
    </div>
    <div class="wrapper">
        <!-- Widgets -->
        <div class="formRow" id="DivCompanyProfileSelectCompany" style="display: none;">
            <label>
                Select Company:<span class="req">*</span></label>
            <div class="formRight">
                <div>
                    <select name="selectReq" id="ddlCompanyProfile" onchange="ddlCompanyProfileChange();">
                        <option value="0">Select Company</option>
                    </select>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <!-- Partners list widget -->
         
		 <div class="widgets">
        <!-- 2 columns widgets -->
         <div class="oneTwo" id="DivCompanyProfileContact" style="display: none;">   
        <div class="widget" >
        
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/timer.png"
                        alt="" class="titleIcon" /><h6>
                            Contact's at <span id="SpanCompanyName2"></span>'s store</b></h6>
                    <div style="float: right; margin: 0px 0px 0 0;">
                        <a href="javascript:script();" title="" class="button greyishB" style="margin: 4px;"
                            onclick="btnCompanyProfileContact();">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/create.png"
                                alt="" class="icon" /><span>New</span></a></div>
                </div>
                <table id="grdCompanyProfileContact" cellpadding="0" cellspacing="0" border="0" class="display dTable">
                    <thead>
                        <tr>
                            <th>
                                Name
                            </th>
                            <th>
                                Email
                            </th>
                            <th>
                                Phone
                            </th>
                            <th>
                                Job Title
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="oneTwo" id="DivCompanyProfile" style="display: none;">
        <div class="widget">
           
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/users.png"
                        alt="" class="titleIcon" /><h6>
                            Client's Profile</h6>
                </div>
                <ul class="partners">
                    <li>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <%--<td width="15%">
                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/buildin_img.jpg"
                                        alt="" />
                                </td>--%>
                                    <td style="vertical-align: top;">
                                        <span><strong style="float: left; width: 50%; border-bottom: 1px solid"><span id="SpanCompanyName">
                                        </span>'s Store</strong><i style="float: right; width: 50%; border-bottom: 1px solid;
                                            text-align: right; font-weight: bold;">1- Very interested</i> </span>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="color: #626262; font-weight: bold; padding-top: 10px;">
                                                    <span id="SpanCompanyName1"></span>'s company<br />
                                                    <span id="SpanCompanyStreetAddress"></span><span id="SpanCompanyCity"></span><span
                                                        id="SpanCompanyState"></span><span id="SpanCompanyZip"></span>
                                                </td>
                                                <td style="width: 60%;">
                                                    <span id="SpanCompanyMainPhone"></span><span id="SpanCompanyMainFax"></span>Webpage:
                                                    <span id="SpanCompanyWebsite"></span><span id="SpanCompanyEditAccountDetails"></span>
                                                    <%-- <a href="#">Edit account details<a>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"> </div>

		
        <!-- Dynamic table -->
        <div class="widget" id="DivCompanyProfileActivity" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/timer.png"
                    alt="" class="titleIcon" /><h6>
                        Activity</h6>
                <div style="float: right; margin: 0px 0px 0 0;">
                    <a href="javascript:void();" title="" class="button greyishB" style="margin: 4px;"
                        onclick="btnCompanyProfileActivity();">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/create.png"
                            alt="" class="icon" /><span>New</span></a></div>
            </div>
            <table id="GrdCompanyProfileActivity" cellpadding="0" cellspacing="0" border="0"
                width="100%" class="sTable taskWidget">
                <%--<thead>
                    <tr>
                        <th id="ThDate">
                            Name
                        </th>
                        <th id="ThOriginator">
                            Email
                        </th>
                        <th id="ThActivityType">
                            Phone
                        </th>
                        <th id="ThContact">
                            Job Title
                        </th>
                    </tr>
                </thead>--%>
            </table>
        </div>
    </div>
	
    <input type="hidden" id="hiddenCompanyID" value="0" />
    <input type="hidden" id="hiddenCompanyContactID" value="0" />
    <input type="hidden" id="hiddenEcomPlatID" value="0" />
    <input type="hidden" id="hiddenActivityId" value="0" />
    <input type="hidden" id="hiddenCompanyIdForContact" value="0" />
    <input type="hidden" id="hiddenTypeOfContact" value="1" />
    <input type="hidden" id="hiddenEditContactID" value="" />
    <script type="text/javascript">
        FunctionOnLoadCompany();
        FunctionOnLoadCompanyContact();
        FunctionOnLoadEcomPlatform();
        FunctionOnLoadActivity();
        FunctionOnLoadFilter();
        FunctionOnLoadCompanyProfile();
    </script>
</asp:Content>
