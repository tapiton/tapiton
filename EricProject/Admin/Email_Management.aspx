<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Email_Management.aspx.cs" Inherits="Admin_Email_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Email Management</title>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/manage-email.js"> </script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple"
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Responsive header -->
    <!-- Title area -->
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>Email Management</h5>
                <span>Manage Email</span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Main content wrapper -->
    <div class="wrapper">

        <div class="widget" id="DivEmailGrid">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>Manage Email</h6>
            </div>
            <!-- Dynamic table -->
            <table id="GrdManageEmail" cellpadding="0" cellspacing="0" border="0" class="display dTable">
                <thead>
                    <tr>
                        <th>Email Name
                        </th>
                        <th>Suject
                        </th>
                        <th>Edit
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <fieldset>
            <div class="widget"  id="DivEmailField">
                <div class="title">
                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                        alt="" class="titleIcon" /><h6>Manage Email</h6>
                </div>
                <div class="formRow">
                    <label>
                        Email Name:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" id="txtName" readonly="readonly" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Subject:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" id="txtSubject" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Replace Text:<span class="req">*</span></label>
                    <div class="formRight">
                        <span id="SpanReplaceText" style="font-weight:bold;"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Body:<span class="req">*</span></label>
                    <div class="formRight">
                        <%--<input type="text"  id="txtBody" />--%>
                        <textarea id="txtBody" rows="15" cols="50" style="width:710px;"></textarea>
                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="formSubmit">
                    <input type="button" value="Update" class="redB" onclick="return AddNewEmailDetails();" />
                </div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
    </div>
    <input type="hidden" id="hiddenEmailID" value="0" />
    <script type="text/javascript">
        FunctionOnLoad();
    </script>
</asp:Content>
