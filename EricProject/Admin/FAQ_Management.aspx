<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="FAQ_Management.aspx.cs" Inherits="FAQ_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>FAQ Management</title>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/faq-management.js"></script>
      <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple"
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>
                    FAQ Management</h5>
                <%-- <span>Add a New Admin</span>--%>
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
                <ul>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                            alt="" />
                        <%--<input type="button" id="btnAddNewFAQ" value="Add new Faq" onclick="VisibleFAQDiv();" />--%>
                        <span id="btnAddNewFAQ" onclick="VisibleFAQDiv(this.id);">Add new Faq</span> </a>
                    </li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/hire-me.png"
                            alt="" /><span id="ViewCustomerFAQ" onclick="VisibleFAQDiv(this.id);">View Customer
                                FAQ</span></a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/hire-me.png"
                            alt="" /><span id="ViewMerchantFAQ" onclick="VisibleFAQDiv(this.id);">View Merchant
                                FAQ</span></a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/order-192.png"
                            alt="" /><span id="ManageFAQCategory" onclick="VisibleFAQDiv(this.id);">Manage FAQ Categories</span></a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Main content wrapper -->
    <div class="wrapper">
        <div class="widget" id="AddFAQ" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                    alt="" class="titleIcon" /><h6>
                        Add FAQ</h6>
            </div>
            <div class="formRow">
                <label>
                    Add FAQ For:<span class="req">*</span></label>
                <div class="formRight">
                    <div>
                        <select name="AddFAQfor" id="ddlAddFAQfor" onchange="BindFAQCategoryForType();">
                            <option value="0">--Select--</option>
                            <option value="1">Customer</option>
                            <option value="2">Merchant</option>
                        </select>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Select FAQ Category(Choose FAQ Type First):<span class="req">*</span></label>
                <div class="formRight">
                    <div>
                        <select name="FAQCategory" id="ddlFAQCategory">
                            <option value="0">--Select--</option>
                            <%--     <option value="opt2">Referrals</option>
                       <option value="opt3">Other</option>--%>
                        </select>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Frequently Asked Question:<span class="req">*</span></label>
                <div class="formRight">
                    <textarea id="txtQuestion" cols="100" rows="8" style="width: 100%" name="textarea"></textarea></div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Answer:<span class="req">*</span></label>
                <div class="formRight">
                    <textarea   id="txtAnswers" cols="100" rows="15" style="width: 100%;height:150%;" name="textarea"></textarea></div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Order:<span class="req">*</span></label>
                <div class="formRight">
                    <input type="text" id="txtOrderFAQ" onkeypress="return checkIt(event);" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    Status:<span class="req">*</span></label>
                <div class="formRight">
                    <div class="floatL">
                        <input type="radio" id="rbActive" class="radioInpt" name="radioReq" data-prompt-position="topRight:102"
                            value="1" onclick="hiddenFAQStatus(this.value)" checked/><label class="rdtxt" for="radioReq">Active</label></div>
                    <div class="floatL">
                        <input type="radio" id="rbInActive" class="radioInpt" name="radioReq" data-prompt-position="topRight:102"
                            value="2" onclick="hiddenFAQStatus(this.value)" /><label class="rdtxt" for="radioReq">Inactive</label></div>
                    <div class="floatL">
                        <input type="radio" id="rbSearchOnly" class="radioInpt" name="radioReq" data-prompt-position="topRight:102"
                            value="3" onclick="hiddenFAQStatus(this.value)" /><label class="rdtxt" for="radioReq">Search
                                Only</label></div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="formSubmit">
                <input type="button" value="submit" class="redB" id="btnAddNewFAQ" onclick="return AddNewFAQ();" /></div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="wrapper">
        <div class="widget" id="MerchantFAQ" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Merchant - Frequently Asked Questions</h6>
            </div>
            <table id="GrdMerchant" cellpadding="0" cellspacing="0" width="100%" class="display dTable">
                <thead>
                    <tr>
                        <th style="width: 20%">
                            FAQ Categroy
                        </th>
                        <th style="width: 35%">
                            FAQ
                        </th>
                        <th style="width: 35%">
                            Answers
                        </th>
                        <th style="width: 5%">
                            Edit
                        </th>
                        <th style="width: 5%">
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <div class="wrapper">
        <div class="widget" id="CustomerFAQ" style="display: none;">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Customer - Frequently Asked Questions</h6>
            </div>
            <table id="GrdCustomer" cellpadding="0" cellspacing="0" width="100%" class="display dTable">
                <thead>
                    <tr>
                        <th style="width: 20%">
                            FAQ Categroy
                        </th>
                        <th style="width: 35%">
                            FAQ
                        </th>
                        <th style="width: 35%">
                            Answers
                        </th>
                        <th style="width: 5%">
                            Edit
                        </th>
                        <th style="width: 5%">
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <div class="wrapper">
        <fieldset>
            <div id="AddEditFAQCategory" class="widget" style="display: none;">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                        alt="" class="titleIcon" /><h6>
                            Add/Edit FAQ Catgory</h6>
                </div>
                <div class="formRow">
                    <label>
                        Category Type:<span class="req">*</span></label>
                    <div class="formRight">
                        <select name="CategoryType" id="ddlCategoryType">
                            <option value="0">--Select--</option>
                            <option value="1">Customer</option>
                            <option value="2">Merchant</option>
                        </select></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Category Title:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtCategoryTitle" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRight">
                    <div class="formRow">
                        <label>
                            Category Description:<span class="req">*</span></label>
                        <div class="formRight">
                            <textarea rows="8" cols="" name="textarea" id="txtCategoryDescription"></textarea></div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="formRow">
                        <label>
                            Order:<span class="req">*</span></label>
                        <div class="formRight">
                            <input type="text" name="req" id="txtOrder" onkeypress="return checkIt(event);" />
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="submit" class="redB" id="btnAddNewFAQCategory" onclick="return AddNewFAQCategory();" /></div>
                <!-- Table with sortable columns -->
                <div class="widget">
                    <div class="title">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                            alt="" class="titleIcon" /><h6>
                                Manage FAQ Category</h6>
                    </div>
                    <!-- Dynamic table -->
                    <table id="GrdFAQCategory" cellpadding="0" cellspacing="0" border="0" class="display dTable">
                        <thead>
                            <tr>
                                <th style="width: 45%">
                                    Category Type
                                </th>
                                <th style="width: 45%">
                                    FAQ Categroy
                                </th>
                                <th style="width: 5%">
                                    Edit
                                </th>
                                <th style="width: 5%">
                                    Delete
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </fieldset>
    </div>
    <input type="hidden" id="hiddenFAQCategoryID" value="0" />
    <input type="hidden" id="hiddenFAQCustomerID" value="0" />
    <input type="hidden" id="hiddenFAQCategoryType" value="0" />
    <input type="hidden" id="hiddenFAQCategoryType" value="0" />
    <input type="hidden" id="hiddenFAQStatus1" value="1" />
    <script type="text/javascript">
        FunctionOnLoad();
    </script>
</asp:Content>
