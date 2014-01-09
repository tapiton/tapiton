<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="TestEcommerce.aspx.cs" Inherits="EricProject.Admin.TestEcommerce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/demos.css" rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#datepicker").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo">
        <p>
            Date:
            <input type="text" id="datepicker"></p>
    </div>
    <!-- End demo -->
</asp:Content>
