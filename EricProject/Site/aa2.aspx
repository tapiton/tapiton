<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="aa2.aspx.cs" Inherits="Site_aa2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/datepicker.css" type="text/css" />
    <link rel="stylesheet" media="screen" type="text/css" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/layout.css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/datepicker.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/eye.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/utils.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/layout.js?ver=1.0.2"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<input type="text" id="DateFrom" />
 <input class="inputDate" id="inputDate" value="06/14/2008" runat="server"/>
</asp:Content>
