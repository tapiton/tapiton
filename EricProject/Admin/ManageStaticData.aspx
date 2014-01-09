<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageStaticData.aspx.cs" Inherits="Admin_ManageStaticData" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Content Management System</title>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/manage-static-data.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Responsive header -->
    <!-- Title area -->
    <div class="titleArea">
        <div class="wrapper">
            <div class="pageTitle">
                <h5>
                    Content Management System</h5>
                <span>Manage Content.</span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <%--<div class="line">
    </div>--%>
    <!-- Page statistics area -->
    <%--<div class="statsRow">
        <div class="wrapper">
            <div class="controlB">
                <ul>
                    <li><a href="#" title="" onclick="SetPageID(1);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/plus.png"
                            alt="" /><span>Page 1</span></a></li>
                    <li><a href="#" title="" onclick="SetPageID(2);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/database.png"
                            alt="" /><span>Page 2</span></a></li>
                    <li><a href="#" title="" onclick="SetPageID(3);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/hire-me.png"
                            alt="" /><span>Page 3</span></a></li>
                    <li><a href="#" title="" onclick="SetPageID(4);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/statistics.png"
                            alt="" /><span>Page 4</span></a></li>
                    <li><a href="#" title="" onclick="SetPageID(5);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/comment.png"
                            alt="" /><span>Page 5</span></a></li>
                    <li><a href="#" title="" onclick="SetPageID(6);">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/control/32/order-149.png"
                            alt="" /><span>Page 6</span></a></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>--%>
    <div class="line">
    </div>
    <!-- Main content wrapper -->
    <div class="wrapper">
        <!-- Note -->
        <div class="nNote nInformation hideit">
            <p>
                <h3>
                    &nbsp;&nbsp;<span id="SpanPageName"></span></h3>
            </p>
        </div>
        <!-- WYSIWYG editor -->
        <div class="widget">
            <div class="formRow">
                <label id="lblPageName">
                    <strong>Page Name:</strong> <span class="req">*</span></label>
                <label id="lblEcommerceProvider">
                    <strong>Ecommerce Provider:</strong> <span class="req">*</span></label>
                <div class="formRight">
                    <input type="text" name="PageName" id="txtPageName" />
                    <asp:DropDownList ID="ddlEcommerceProvider" CssClass="formSlctsml" runat="server" AutoPostBack="false" onchange="BindData();">
                                            </asp:DropDownList>	
                    </div>
                <div class="clear">
                </div>
            </div>
            <div class="formRow">
                <label>
                    <strong>Title:</strong> <span class="req">*</span></label>
                <div class="formRight">
                    <input type="text" name="Title" id="txtTitle" /></div>
                <div class="clear">
                </div>
            </div>
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/pencil.png"
                    alt="" class="titleIcon" /><h6>
                        Content editor</h6>
            </div>
            
            <CKEditor:CKEditorControl ID="editor1" runat="server" Width="98%" Height="180px"></CKEditor:CKEditorControl>
            <%--<input type="text" name="editor" id="editor1" />--%>
        </div>
        <div class="formSubmit">
            <input type="button" value="submit" class="redB" onclick="return AddNewEditStaticContent();" /></div>
    </div>
    <input type="hidden" id="hiddenPageID" value="1" />
    <script type="text/javascript">
        FunctionOnLoad('<%= Request.QueryString["pid"] %>');
    </script>
</asp:Content>
