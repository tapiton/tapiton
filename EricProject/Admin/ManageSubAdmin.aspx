<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageSubAdmin.aspx.cs" Inherits="Admin_ManageSubAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Sub Admin</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Responsive header -->
    <div class="resp">
        <div class="respHead">
            <a href="#" title="Add New SubAdmin">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/loginLogo.png"
                    alt="" /></a>
        </div>
        <div class="cLine">
        </div>
        <div class="smalldd">
            <span class="goTo">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/home.png"
                    alt="" />Dashboard</span>
            <ul class="smallDropdown">
                <li><a href="index.html" title="">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/home.png"
                        alt="" />Dashboard</a></li>
                <li><a href="charts.html" title="">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/stats.png"
                        alt="" />Statistics and charts</a></li>
                <li><a href="#" title="" class="exp">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/pencil.png"
                        alt="" />Forms stuff<strong>4</strong></a>
                    <ul>
                        <li><a href="forms.html" title="">Form elements</a></li>
                        <li><a href="form_validation.html" title="">Validation</a></li>
                        <li><a href="form_editor.html" title="">WYSIWYG and file uploader</a></li>
                        <li class="last"><a href="form_wizards.html" title="">Wizards</a></li>
                    </ul>
                </li>
                <li><a href="ui_elements.html" title="">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/users.png"
                        alt="" />Interface elements</a></li>
                <li><a href="tables.html" title="" class="exp">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/frames.png"
                        alt="" />Tables<strong>3</strong></a>
                    <ul>
                        <li><a href="table_static.html" title="">Static tables</a></li>
                        <li><a href="table_dynamic.html" title="">Dynamic table</a></li>
                        <li class="last"><a href="table_sortable_resizable.html" title="">Sortable &amp; resizable
                            tables</a></li>
                    </ul>
                </li>
                <li><a href="#" title="" class="exp">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/fullscreen.png"
                        alt="" />Widgets and grid<strong>2</strong></a>
                    <ul>
                        <li><a href="widgets.html" title="">Widgets</a></li>
                        <li class="last"><a href="grid.html" title="">Grid</a></li>
                    </ul>
                </li>
                <li><a href="#" title="" class="exp">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/alert.png"
                        alt="" />Error pages<strong>6</strong></a>
                    <ul class="sub">
                        <li><a href="403.html" title="">403 page</a></li>
                        <li><a href="404.html" title="">404 page</a></li>
                        <li><a href="405.html" title="">405 page</a></li>
                        <li><a href="500.html" title="">500 page</a></li>
                        <li><a href="503.html" title="">503 page</a></li>
                        <li class="last"><a href="offline.html" title="">Website is offline</a></li>
                    </ul>
                </li>
                <li><a href="file_manager.html" title="">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/files.png"
                        alt="" />File manager</a></li>
                <li><a href="#" title="" class="exp">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/light/create.png"
                        alt="" />Other pages<strong>3</strong></a>
                    <ul>
                        <li><a href="typography.html" title="">Typography</a></li>
                        <li><a href="calendar.html" title="">Calendar</a></li>
                        <li class="last"><a href="gallery.html" title="">Gallery</a></li>
                    </ul>
                </li>
            </ul>
        </div>
        <div class="cLine">
        </div>
    </div>
    <!-- Title area -->
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>
                    Manage Admin Users</h5>
                <span>Add, Edit and Delete Admin Users</span>
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
            <div class="controlB">
                <ul>
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Admin/AddNewSubAdmin.aspx"
                        title="Add New SubAdmin">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/hire-me.png"
                            alt="" /><span>Add new user</span></a></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Main content wrapper -->
    <div class="wrapper">
        <div class="widget">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Manage Admin Users</h6>
            </div>
            <table cellpadding="0" cellspacing="0" width="100%" class="sTable">
                <thead>
                    <tr>
                        <td class="sortCol">
                            <div>
                                User name<span></span></div>
                        </td>
                        <td class="sortCol">
                            <div>
                                Email-ID<span></span></div>
                        </td>
                        <td class="sortCol">
                            <div>
                                Role<span></span></div>
                        </td>
                        <td class="sortCol">
                            <div>
                                Column name<span></span></div>
                        </td>
                        <td class="sortCol">
                            <div>
                                Column name<span></span></div>
                        </td>
                        <td>
                            <div>
                                Edit<span></span></div>
                        </td>
                        <td>
                            <div>
                                Delete<span></span></div>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            Row 1
                        </td>
                        <td>
                            Row 3
                        </td>
                        <td>
                            Row 4
                        </td>
                        <td>
                            Row 5
                        </td>
                        <td>
                            Row 2
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                                alt="">
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/uploader/deleteFile.png"
                                alt="">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Row 1
                        </td>
                        <td>
                            Row 3
                        </td>
                        <td>
                            Row 4
                        </td>
                        <td>
                            Row 5
                        </td>
                        <td>
                            Row 2
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                                alt="">
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/uploader/deleteFile.png"
                                alt="">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Row 1
                        </td>
                        <td>
                            Row 3
                        </td>
                        <td>
                            Row 4
                        </td>
                        <td>
                            Row 5
                        </td>
                        <td>
                            Row 2
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                                alt="">
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/uploader/deleteFile.png"
                                alt="">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Row 1
                        </td>
                        <td>
                            Row 3
                        </td>
                        <td>
                            Row 4
                        </td>
                        <td>
                            Row 5
                        </td>
                        <td>
                            Row 2
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                                alt="">
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/uploader/deleteFile.png"
                                alt="">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Row 1
                        </td>
                        <td>
                            Row 3
                        </td>
                        <td>
                            Row 4
                        </td>
                        <td>
                            Row 5
                        </td>
                        <td>
                            Row 2
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/16/pencil.png"
                                alt="">
                        </td>
                        <td width="30" style="text-align: center;">
                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/uploader/deleteFile.png"
                                alt="">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <fieldset>
            <div class="widget">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/alert.png"
                        alt="" class="titleIcon" /><h6>
                            Edit User Details</h6>
                </div>
                <div class="formRow">
                    <label>
                        First Name:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" class="validate[required]" name="req" id="req" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Last Name:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" class="validate[required]" name="req" id="req" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Email Address:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" value="" class="validate[required,custom[email]]" name="emailValid"
                            id="emailValid" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Password:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="password" class="validate[required]" name="password1" id="password1" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Confirm Password:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="password" class="validate[required,equals[password]]" name="password2"
                            id="password2" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Select Role:<span class="req">*</span></label>
                    <div class="formRight">
                        <div class="floatL">
                            <select name="selectReq" id="selectReq" class="validate[required]">
                                <option value="">Super Admin</option>
                                <option value="opt2">Sub-admin</option>
                                <option value="opt3">Sales person</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Address:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" class="validate[required]" name="req" id="req" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        City:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" class="validate[required]" name="req" id="req" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        State:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" class="validate[required]" name="req" id="req" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Country:<span class="req">*</span></label>
                    <div class="formRight">
                        <div class="floatL">
                            <select name="selectReq" id="selectReq" class="validate[required]">
                                <option value="">United States of America</option>
                                <option value="opt2">India</option>
                                <option value="opt3">Australia</option>
                                <option value="opt4">Canada</option>
                                <option value="opt5">Mexico</option>
                                <option value="opt6">Germany</option>
                                <option value="opt7">China</option>
                                <option value="opt8">Switzerland</option>
                            </select>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Phone Number:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" value="10.1" class="validate[required,custom[onlyNumberSp]]" name="numsValid"
                            id="numsValid" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Fax:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" value="10.1" class="validate[required,custom[onlyNumberSp]]" name="numsValid"
                            id="numsValid" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formSubmit">
                    <input type="submit" value="Update" class="redB" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
    </div>
    <div class="clear">
    </div>
</asp:Content>
