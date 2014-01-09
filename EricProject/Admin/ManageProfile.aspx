<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageProfile.aspx.cs" Inherits="Admin_ManageProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Profile</title>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/manage-profile.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Responsive header -->
    <!-- Title area -->
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>
                    Manage Admins Users Profile</h5>
                <span>Manage Admins Users Profile</span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Page statistics and control buttons area -->
    <%-- <div class="statsRow">
        <div class="wrapper">
            <div class="controlB">
                <ul>
                    <li><a href="#" title="Manage SubAdmin">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/order-149.png"
                            alt="" /><span>Manage Admin Users</span></a></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="line">
    </div>--%>
    <!-- Main content wrapper -->
    <div class="wrapper">
        <fieldset>
            <div class="widget">
                <div class="title">
                    &nbsp;
                </div>
                <div class="formRow">
                    <label>
                        First Name:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtFirstName" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Last Name:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="LastName" id="txtLastName" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        EmailID:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="Password" id="txtEmail" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Password:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="minValid" id="txtPassword" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Confirm Password:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="minValid" id="txtConfirmPassword" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select Role:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="selectReq" id="ddlselectRole">
                                <option value="">Please Select Role</option>
                                <option value="1">SuperAdmin</option>
                                <option value="2">SubAdmin</option>
                                <option value="3">SalesPerson</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Address:<%--<span class="req">*</span>--%></label>
                    <div class="formRight">
                        <input type="text" name="Address" id="txtAddress" /></div>
                    <div class="clear">
                    </div>
                </div>
                   <div class="formRow">
                    <label>
                        Address2:<%--<span class="req">*</span>--%></label>
                    <div class="formRight">
                        <input type="text" name="Address" id="txtAddress2" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        City:<%--<span class="req">*</span>--%></label>
                    <div class="formRight">
                        <input type="text" name="City" id="txtCity" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        State:<%--<span class="req">*</span>--%></label>
                    <div class="formRight">
                        <input type="text" name="numsValid" id="txtState" maxlength="2"/></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Country:<span class="req">*</span></label>
                    <div class="formRight">
                        <div>
                            <select name="ddlCountry" id="ddlCountry">
                                <option value="">Please Select Country</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Zip:<span class="req"></span></label>
                    <div class="formRight">
                        <input type="text" value="10" name="Zip" id="txtZip" maxlength="5" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        PhoneNo:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="PhoneNO" id="txtphoneno" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Fax:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="Fax" id="txtfax" onkeypress="return checkIt(event);" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="button" value="Edit" class="redB" onclick="return AddNewAdminDetails();" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
        <div class="widget" style="display: none;">
            <!-- Dynamic table -->
            <table id="GrdManageAdmin" cellpadding="0" cellspacing="0" border="0" class="display ">
                <thead>
                    <tr>
                        <th>
                            Username
                        </th>
                        <th>
                            Email
                        </th>
                        <th>
                            Role
                        </th>
                        <th>
                            Edit
                        </th>
                        <th>
                            Delete
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        FunctionOnLoad();
    </script>
</asp:Content>
