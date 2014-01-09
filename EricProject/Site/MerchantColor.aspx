<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="MerchantColor.aspx.cs" Inherits="EricProject.Site.MerchantColor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!--For color picker -->
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/colorpicker.css" type="text/css" />
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>js/colorpicker.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>js/eye.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>js/utils.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>js/layout.js"></script>
<!--For color picker (End)-->	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
